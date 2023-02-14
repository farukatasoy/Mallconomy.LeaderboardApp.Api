using System;
namespace Mallconomy.LeaderboardApp.Models
{
    public class LeaderboardListModel
    {
        public string? Id { get; set; }

        public string UserId { get; set; } = null!;

        public int Rank { get; set; }

        public int TotalPoints { get; set; }
    }
}