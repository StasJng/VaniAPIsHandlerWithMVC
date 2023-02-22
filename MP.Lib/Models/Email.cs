using System;
using MP.Lib.Contrains;
using MP.Lib.Core.MongoDb;
using MongoDB.Bson.Serialization.Attributes;

namespace MP.Lib.Models
{
    [CollectionName("mailers")]
    public class Email : IObject
    {


        public Email()
        {
            Created = DateTime.Now;
            Timestamp = DateTime.Now.Ticks;
            Priority = (int)EmailPriority.Normal;
        }

        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("from")]
        public string From { get; set; }

        [BsonElement("to")]
        public string To { get; set; }

        [BsonElement("body")]
        public string Body { get; set; }

        [BsonElement("stt")]
        public int Status { get; set; }

        [BsonElement("priority")]
        public int Priority { get; set; }

        [BsonElement("crt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; }

        [BsonElement("tt")]
        public double Timestamp { get; set; }

        [BsonElement("retry")]
        public int Retry { get; set; }


    }
}
