using System;
using Mallconomy.LeaderboardApp.Data.Entities;
using Mallconomy.LeaderboardApp.Models;

namespace Mallconomy.LeaderboardApp.Data.Interfaces
{
    public interface ILeaderboardService
    {
        Task<List<Scores>> CreateScoreListAsync();
        List<LeaderboardCreateModel> CreateLeaderboard(List<Scores> scoreList, DateTime date);

        Task<Leaderboard> CheckLeaderboardPeriodAsync(DateTime date);
        Task<Leaderboard> GetLeaderboardByDateAndIdAsync(DateTime date, string id);
        Task<List<Leaderboard>> GetLeaderboardByDateAsync(DateTime date);
        Task<List<Leaderboard>> GetUserPrizesAsync(string id);
    }
}