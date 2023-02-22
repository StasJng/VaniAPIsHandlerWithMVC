using MongoDB.Bson;

namespace MP.Account.Core.Repo
{
    public interface IRepository<T>
    {
        void Remove(ObjectId id);
        void Remove(string id);
        void Remove(string[] id);
        T Info(ObjectId id);
        T Info(string id);
        void Save(T obj);
    }
}
