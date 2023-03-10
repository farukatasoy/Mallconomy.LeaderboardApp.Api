using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mallconomy.LeaderboardApp.Data.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}