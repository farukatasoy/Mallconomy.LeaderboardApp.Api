using System;
namespace Mallconomy.LeaderboardApp.Data.Configurations
{
    public class MallconomyDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string LeaderboardCollectionName { get; set; } = null!;
    }
}