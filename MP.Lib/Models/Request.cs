using System;
using MP.Lib.Contrains;
using MP.Lib.Core.MongoDb;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MP.Lib.Models
{

    [CollectionName("requests")]
    public class Request : IObject
    {
        public Request()
        {
            Id = ObjectId.GenerateNewId();
            Created = DateTime.Now;
            Status = (int)GlobalStatus.Pending;
        }
        [BsonElement("mail")]
        public string Email { get; set; }

        [BsonElement("uid")]
        public string UserId { get; set; }

        [BsonElement("crt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; }

        [BsonElement("stt")]
        public int Status { get; set; }
    }
}
