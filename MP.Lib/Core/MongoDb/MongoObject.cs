using MongoDB.Bson.Serialization.Attributes;

namespace MP.Lib.Core.MongoDb
{
    public abstract class MongoObject
    {
        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }
        public abstract void Save();
        public abstract void Remove();

    }
}
