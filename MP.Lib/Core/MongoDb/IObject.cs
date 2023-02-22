using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MP.Lib.Core.MongoDb
{
    public abstract class IObject
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
