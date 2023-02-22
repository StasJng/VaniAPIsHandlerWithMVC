using System.Text.RegularExpressions;
using MP.Account.Core.Repo;
using MP.Lib.Contrains;
using MP.Lib.Core;
using MP.Lib.Core.MongoDb;
using MP.Lib.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MP.Lib.Repo
{
    public class RequestRepo : IRepository<Request>
    {

        public static RequestRepo Instant
        {
            get { return Lazy.Create<RequestRepo>(); }
        }

        public void Remove(ObjectId id)
        {
            MainDb.Instant.Remove<Request>(id);
        }

        public void Remove(string id)
        {
            Remove(ObjectId.Parse(id));
        }

        public void Remove(string[] id)
        {
            foreach (string _s in id)
            {
                Remove(_s);
            }
        }

        public Request Info(ObjectId id)
        {
            return MainDb.Instant.GetById<Request>(id);
        }

        public Request Info(string id)
        {

            var _query = Query.And(Query<Request>.EQ(x => x.Id, ObjectId.Parse(id)),
                                   Query<Request>.EQ(x => x.Status, (int) GlobalStatus.Pending));

            return MainDb.Instant.FindOne<Request>(_query);
        }

        public void Save(Request obj)
        {
            MainDb.Instant.Save(obj);
        }

        public void Save(ObjectId id, GlobalStatus status)
        {
            var _query = Query<Request>.EQ(x => x.Id,id);
            var _update = Update<Request>.Set(x => x.Status, (int) status);
            MainDb.Instant.Update<Request>(_query,_update);
        }

       
    }
}
