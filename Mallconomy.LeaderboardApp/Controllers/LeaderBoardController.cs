using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mallconomy.LeaderboardApp.Data.Entities;
using Mallconomy.LeaderboardApp.Data.Interfaces;
using Mallconomy.LeaderboardApp.Data.Services;
using Mallconomy.LeaderboardApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mallconomy.LeaderboardApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly IService<Leaderboard> _service;
    private readonly ILeaderboardService _leaderboardService;
    private readonly IMapper _mapper;

    public LeaderboardController(ILeaderboardService leaderboardService, IMapper mapper, IService<Leaderboard> service)
    {
        _leaderboardService = leaderboardService;
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(DateTime date)
    {
        var result = await _leaderboardService.CheckLeaderboardPeriodAsync(date);
        if (result != null)
            return BadRequest("The system can create a leaderboard only once per month.");

        var scoreList = await _leaderboardService.CreateScoreListAsync();

        if (scoreList == null)
            return NotFound("Data source not found.");

        var leaderboard = _mapper.Map<List<Leaderboard>>(_leaderboardService.CreateLeaderboard(scoreList, date.AddDays(1)));

        foreach (var document in leaderboard)
            await _service.CreateAsync(document);

        return Ok($"The leaderboard has been created for {date.Year}-{date.Month}");
    }

    [HttpGet("{date}")]
    public async Task<ActionResult<List<LeaderboardListModel>>> Get(DateTime date)
    {
        var result = _mapper.Map<List<LeaderboardListModel>>(await _leaderboardService.GetLeaderboardByDateAsync(date));

        if (result is null)
            return NotFound();

        return result;
    }

    [HttpGet("{date},{userId:length(24)}")]
    public async Task<ActionResult<LeaderboardListModel>> Get(DateTime date, string userId)
    {
        var result = _mapper.Map<LeaderboardListModel>(await _leaderboardService.GetLeaderboardByDateAndIdAsync(date, userId));

        if (result is null)
            return NotFound();

        return result;
    }

    [HttpGet("{userId:length(24)}")]
    public async Task<ActionResult<List<UserPrizesListModel>>> Get(string userId)
    {
        var result = _mapper.Map<List<UserPrizesListModel>>(await _leaderboardService.GetUserPrizesAsync(userId));

        if (result is null)
            return NotFound();

        return result;
    }
}