using System;
namespace Mallconomy.LeaderboardApp.Models
{
    public class LeaderboardCreateModel
    {
        public DateTime Date { get; set; }

        public string UserId { get; set; } = null!;

        public int Rank { get; set; }

        public int TotalPoints { get; set; }

        public string Prize { get; set; } = null!;
    }
}