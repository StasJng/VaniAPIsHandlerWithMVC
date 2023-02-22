using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MP.Lib.Core.MongoDb
{
    public class IntIdGenerator : IIdGenerator
    {
        public object GenerateId(object container, object document)
        {
            var _idSequenceCollection = ((MongoCollection)container).Database.GetCollection("unique_ids");

            var _query = Query.EQ("_id", ((MongoCollection)container).Name);

            return _idSequenceCollection
                .FindAndModify(_query, null, Update.Inc("seq", 1), true, true)
                .ModifiedDocument["seq"]
                .AsInt32;
        }

        public bool IsEmpty(object id)
        {
            return (int)id == 0;
        }
    }
}
