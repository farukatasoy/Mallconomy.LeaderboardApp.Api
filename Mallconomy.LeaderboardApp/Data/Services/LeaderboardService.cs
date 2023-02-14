using Mallconomy.LeaderboardApp.Data.Configurations;
using Mallconomy.LeaderboardApp.Data.Entities;
using Mallconomy.LeaderboardApp.Data.Interfaces;
using Mallconomy.LeaderboardApp.Models;
using Microsoft.Extensions.Options;
using AutoMapper;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Mallconomy.LeaderboardApp.Data.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IMongoCollection<Leaderboard> _leaderboardCollection;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public LeaderboardService(IOptions<MallconomyDatabaseSettings> mallconomyDatabaseSettings, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            var mongoClient = new MongoClient(mallconomyDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mallconomyDatabaseSettings.Value.DatabaseName);

            _leaderboardCollection = mongoDatabase.GetCollection<Leaderboard>(
                mallconomyDatabaseSettings.Value.LeaderboardCollectionName);
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        public async Task<List<Scores>> CreateScoreListAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://cdn.mallconomy.com/testcase/points.json");
            var jsonData = await response.Content.ReadAsStringAsync();
            var scoreList = JsonConvert.DeserializeObject<List<Scores>>(jsonData);
            if (scoreList != null)
                return scoreList;
            else
                return new();
        }

        public List<LeaderboardCreateModel> CreateLeaderboard(List<Scores> scoreList, DateTime date)
        {
            //Leaderboard'in olusturulmasi
            var approvedList = scoreList.Where(I => I.Approved == true).ToList();

            var users = approvedList.DistinctBy(p => p.User_Id.OId).ToList();

            List<LeaderboardCreateModel> leaderboard = new();

            foreach (var user in users)
                leaderboard.Add(new LeaderboardCreateModel { UserId = user.User_Id.OId, Date = date });

            foreach (var user in leaderboard)
            {
                var found = approvedList.Where(I => I.User_Id.OId == user.UserId);
                foreach (var item in found)
                    user.TotalPoints += item.Point;
            }

            //Siralamanin belirlenmesi
            leaderboard = leaderboard.OrderByDescending(x => x.TotalPoints).ToList();

            //Ilk uc icin odullerin dagitilmasi ve derecelendirilmesi
            leaderboard[0].Rank = 1;
            leaderboard[0].Prize = "First Prize";
            leaderboard[1].Rank = 2;
            leaderboard[1].Prize = "Second Prize";
            leaderboard[2].Rank = 3;
            leaderboard[2].Prize = "Third Prize";

            //Ilk yuz icin odullerin dagitilmasi ve derecelendirilmesi
            for (int i = 3; i < 100; i++)
            {
                if (leaderboard[i].UserId != null)
                {
                    leaderboard[i].Rank = i + 1;
                    leaderboard[i].Prize = "25$";
                }
                else
                    break;
            }

            //Ilk bin icin odullerin dagitilmasi ve derecelendirilmesi
            var first1000PrizeAmount = leaderboard.Count < 1000 ? (double)12500 / (leaderboard.Count() - 100) : (double)12500 / 900;

            for (int i = 100; i < 1000; i++)
            {
                if (leaderboard[i].UserId != null)
                {
                    leaderboard[i].Rank = i + 1;
                    leaderboard[i].Prize = $"{first1000PrizeAmount.ToString("#00.00").Substring(0, 5)}$";
                }
                else
                    break;
            }

            //Odul alamayanlarin derecelendirilmesi
            for (int i = 1000; i < leaderboard.Count; i++)
                leaderboard[i].Rank = i + 1;

            return leaderboard;
        }

        public async Task<Leaderboard> CheckLeaderboardPeriodAsync(DateTime date) =>
            await _leaderboardCollection.Find(x => x.Date.Month == date.Month && x.Date.Year == date.Year).FirstOrDefaultAsync();

        public async Task<Leaderboard> GetLeaderboardByDateAndIdAsync(DateTime date, string id) =>
            await _leaderboardCollection.Find(x => x.Date.Month == date.Month && x.Date.Year == date.Year && x.UserId == id).FirstOrDefaultAsync();

        public async Task<List<Leaderboard>> GetLeaderboardByDateAsync(DateTime date) =>
            await _leaderboardCollection.Find(x => x.Date.Month == date.Month && x.Date.Year == date.Year).ToListAsync();

        public async Task<List<Leaderboard>> GetUserPrizesAsync(string id) =>
            await _leaderboardCollection.Find(x => x.UserId == id).ToListAsync();
    }
}