using System;
namespace Mallconomy.LeaderboardApp.Models
{
    public class UserPrizesListModel
    {
        public string? Id { get; set; }

        public DateTime Date { get; set; }

        public int Rank { get; set; }

        public int TotalPoints { get; set; }

        public string Prize { get; set; } = null!;
    }
}