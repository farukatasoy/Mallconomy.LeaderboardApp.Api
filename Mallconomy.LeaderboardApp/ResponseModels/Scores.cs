using System.Security.Cryptography;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Mallconomy.LeaderboardApp.Models
{
    public class Scores
    {
        public Id _Id { get; set; } = null!;
        public bool Approved { get; set; }
        public Id User_Id { get; set; } = null!;
        public int Point { get; set; }

    }

    public class Id
    {
        [JsonProperty("$oid")]
        public string OId { get; set; } = null!;
    }
}