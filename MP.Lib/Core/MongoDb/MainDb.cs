using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MP.Lib.Core.MongoDb
{
    public class MainDb : IDisposable
    {


        public MongoUrl Url { get; set; }

        public ReadPreference Read { get; set; }

        public MongoDatabase Database
        {
            get
            {
                var _client = new MongoClient(Url);
                var _db = _client.GetServer().GetDatabase(Url.DatabaseName);
                _db.RequestStart(Read);
                return _db;

            }
        }

        public static string ConnectionName
        {
            get { return "accdb"; }
        }



        private static bool IsEncrypt { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["mongodb_encrypt"]); } }


        private static string ConnectionString(string _connectionName)
        {

            var _cs = ConfigurationManager.ConnectionStrings[_connectionName].ConnectionString;

            return IsEncrypt ? Encryptor.Decrypt(_cs) : _cs;

        }



        public MainDb(bool _slaveOk) : this(ConnectionName, _slaveOk) { }

        public MainDb(string _connectionName) : this(_connectionName, false) { }

        public MainDb() : this(ConnectionName, false) { }

        public MainDb(string _connectionName, bool _slaveOk) : this(MongoUrl.Create(ConnectionString(_connectionName)), _slaveOk) { }


        public MainDb(MongoUrl url, bool _slaveOk)
        {

            /*Database = MongoDatabase.Create(url);*/

            Url = url;
            Read = _slaveOk ? ReadPreference.SecondaryPreferred : ReadPreference.Primary;

            /* var _client = new MongoClient(url);
             Database = _client.GetServer().GetDatabase(_client.Settings.);
             Database.RequestStart(_slaveOk ? ReadPreference.SecondaryPreferred : ReadPreference.Primary);*/

        }


        public MainDb(MongoUrl url, ReadPreference read)
        {
            Url = url;
            Read = read;
            //ReadPreference.Primary
            /*Database = MongoDatabase.Create(url);*/
            /*  var _client = new MongoClient(url);
              Database = _client.GetServer().GetDatabase(url.DatabaseName);
              Database.RequestStart(read);*/
        }

        /* private static readonly Object _syncRoot = new Object();

         private static volatile MainDb _mainDb;*/

        private static readonly Lazy<Dictionary<string, MainDb>> _instants = new Lazy<Dictionary<string, MainDb>>(() => new Dictionary<string, MainDb>());

        public static Dictionary<string, MainDb> Instants
        {
            get { return _instants.Value; }
        }

        private readonly static object _staticLock = new object();

        public static MainDb Instant
        {
            get { return Create(); }
        }


        public static MainDb Create(string connectionName)
        {
            lock (_staticLock)
            {
                MainDb _db;
                if (!Instants.TryGetValue(connectionName, out _db))
                {
                    _db = new MainDb(connectionName);
                    Instants.Add(connectionName, _db);
                }

                /*_db.Database.Server.Instance.Connect();*/

                return _db;
            }

        }

        public static void Remove()
        {

        }

        public static MainDb Create()
        {
            return Create(ConnectionName);
        }

        public static List<IMongoQuery> Map(params IMongoQuery[] queries)
        {
            return queries.Where(query => query != Query.Null).ToList();
        }

        public MongoCollection<T> GetCollection<T>()
        {
            return Database.GetCollection<T>(MongoExtensions.GetCollectionName<T>());
        }

        public MongoCollection GetCollection(string name)
        {
            return Database.GetCollection(name);
        }


        public IQueryable<T> Get<T>() where T : new()
        {

            return GetCollection<T>().AsQueryable<T>();

        }


        #region Save


        /// <summary>
        /// Adds the new entities in the collection.
        /// </summary>
        /// <param name="_items">The entities of type T.</param>
        public void Add<T>(IEnumerable<T> _items) where T : IObject
        {

            GetCollection<T>().InsertBatch(_items);

        }

        /// <summary>
        /// Adds the new entities in the collection.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="_items">The entities of type T.</param>
        public void Add(string collectionName, IEnumerable<BsonDocument> _items)
        {

            GetCollection(collectionName).InsertBatch(_items);

        }

        public void Add(string collectionName, BsonDocument _item)
        {

            GetCollection(collectionName).Insert(_item);

        }


        /// <summary>
        /// Add or Update the a entity in the collection.
        /// </summary>
        public void Save<T>(T _item)
        {
            GetCollection<T>().Save(_item);

            // GetCollection<T>().Save(_item,MongoInsertOptions)

        }

        public void Save<T>(IEnumerable<T> _items)
        {
            foreach (T _item in _items)
            {
                Save(_item);
            }
        }



        public void Update<T>(IMongoQuery where, IMongoUpdate update, UpdateFlags flags) where T : IObject
        {
            GetCollection<T>().Update(where, update, flags);
        }

        public void Update<T>(IMongoQuery where, IMongoUpdate update) where T : IObject
        {
            Update<T>(where, update, UpdateFlags.Multi);
        }

        public void Update<T>(IMongoUpdate update) where T : IObject
        {
            Update<T>(Query.Null, update);
        }


        #endregion

        #region Find

        public T GetById<T>(ObjectId id)
        {
            return GetCollection<T>().FindOneById(id);
        }

        public T GetById<T>(string id)
        {
            if (typeof(T).IsSubclassOf(typeof(IObject)))
            {
                return GetCollection<T>().FindOneById(ObjectId.Parse(id));
            }

            return GetCollection<T>().FindOneById(id);
        }


        public CommandResult FullTextSearch<T>(string search, IMongoQuery query)
        {
            var _cmd = new CommandDocument
                {
                    { "text",  MongoExtensions.GetCollectionName<T>() },
                    { "search", search },
                    {"filter",query.ToBsonDocument()}
                };


            return Database.RunCommand(_cmd);

        }

        /* public IEnumerable<T> FullTextSearch<T>(string search)
         {
             var _cmd = new CommandDocument
                 {
                     { "text",  GetCollectionName<T>() },
                     { "search", search }
                 };
           
            
             /*return Database.RunCommand(_cmd);#1#
             var _doc = Database.RunCommand(_cmd).Response.GetElement("results").Value.ToString();

             return BsonSerializer.Deserialize<IEnumerable<T>>(_doc);

         }*/




        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> criteria)
        {
            return GetCollection<T>().AsQueryable<T>().Where(criteria);
        }

        public IEnumerable<T> Find<T>(IMongoQuery query)
        {

            return GetCollection<T>().Find(query);
        }

        public IEnumerable<T> Find<T>(IMongoQuery query, int pageIndex, int pageSize)
        {
            return GetCollection<T>().Find(query).SetSkip((pageIndex - 1) * pageSize).SetLimit(pageSize);
        }

        public IEnumerable<T> Find<T>(IMongoFields fields, IMongoQuery query, int pageIndex, int pageSize)
        {
            return GetCollection<T>().Find(query).SetFields(fields).SetSkip((pageIndex - 1) * pageSize).SetLimit(pageSize);

        }

        public IEnumerable<T> Find<T>(string[] fields, IMongoQuery query, int pageIndex, int pageSize)
        {
            return GetCollection<T>().Find(query).SetFields(fields).SetSkip((pageIndex - 1) * pageSize).SetLimit(pageSize);
        }


        public IEnumerable<T> Find<T>(IMongoQuery query, int pageIndex, int pageSize, out  long totalRows)
        {
            var _data = GetCollection<T>();
            totalRows = _data.Find(query).LongCount();
            return _data.Find(query).SetSkip((pageIndex - 1) * pageSize).SetLimit(pageSize);
        }

        public IEnumerable<T> Find<T>(IMongoSortBy sortBy, int pageIndex, int pageSize)
        {
            return GetCollection<T>().FindAll().SetSortOrder(sortBy).SetSkip((pageIndex - 1) * pageSize).SetLimit(pageSize);
        }

        public IEnumerable<T> Find<T>(IMongoFields fields, IMongoQuery query, IMongoSortBy sortBy)
        {
            return GetCollection<T>().Find(query).SetFields(fields).SetSortOrder(sortBy);
        }

        public IEnumerable<T> Find<T>(IMongoQuery query, IMongoSortBy sortBy)
        {
            return GetCollection<T>().Find(query).SetSortOrder(sortBy);
        }

        public IEnumerable<T> Find<T>(IMongoQuery query, IMongoSortBy sortBy, int pageIndex, int pageSize)
        {
            return GetCollection<T>().Find(query).SetSortOrder(sortBy).SetSkip((pageIndex - 1) * pageSize).SetLimit(pageSize);
        }


        public IEnumerable<T> Find<T>(IMongoQuery query, IMongoSortBy sortBy, int pageIndex, int pageSize, out long totalRows)
        {

            var _collection = GetCollection<T>();
            totalRows = _collection.Find(query).Count();
            return _collection.Find(query).SetSortOrder(sortBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);//data.SetSkip((pageIndex - 1) * pageSize).SetLimit(pageSize);
        }



        public IEnumerable<T> All<T>(IMongoSortBy sortBy)
        {
            return GetCollection<T>().FindAll().SetSortOrder(sortBy);


        }

        public IEnumerable<T> All<T>(IMongoSortBy sortBy, int pageIndex, int pageSize, out long totalRows)
        {
            var data = GetCollection<T>();
            totalRows = data.Count();
            return data.FindAll().SetSortOrder(sortBy).SetSkip((pageIndex - 1) * pageSize).SetLimit(pageSize);

        }



        public T FindOne<T>(IMongoQuery query)
        {
            return GetCollection<T>().FindOne(query);
        }

        /// <summary>
        /// Returns a single T by the given criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>A single T matching the criteria.</returns>
        public T FindOne<T>(Expression<Func<T, bool>> criteria)
        {
            return GetCollection<T>().AsQueryable<T>().Where(criteria).FirstOrDefault();
        }



        /// <summary>
        /// Returns the list of T where it matches the criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>IQueryable of T.</returns>
        public IQueryable<T> All<T>(Expression<Func<T, bool>> criteria)
        {
            return GetCollection<T>().AsQueryable<T>().Where(criteria);
        }

        /// <summary>
        /// Returns All the records of T.
        /// </summary>
        /// <returns>IQueryable of T.</returns>
        public IQueryable<T> All<T>()
        {
            return GetCollection<T>().AsQueryable<T>();
        }

        #endregion


        #region EnsureIndex

        public void EnsureIndex<T>(IMongoIndexKeys keys)
        {
            GetCollection<T>().EnsureIndex(keys);
        }

        public void EnsureIndex<T>(IMongoIndexKeys keys, IMongoIndexOptions options)
        {
            GetCollection<T>().EnsureIndex(keys, options);
        }

        public void EnsureIndex<T>(params string[] keys)
        {
            GetCollection<T>().EnsureIndex(keys);
        }

        #endregion


        #region Top

        public IQueryable<T> Top<T>(int top, Expression<Func<T, bool>> criteria)
        {
            return GetCollection<T>().AsQueryable<T>().Where(criteria).Take(top);
        }


        public IEnumerable<T> Top<T>(int top, IMongoQuery query, IMongoSortBy sortBy)
        {
            return GetCollection<T>().Find(query).SetSortOrder(sortBy).SetLimit(top);
        }

        public IEnumerable<T> Top<T>(int top, IMongoFields fields, IMongoQuery query, IMongoSortBy sortBy)
        {
            return GetCollection<T>().Find(query).SetFields(fields).SetSortOrder(sortBy).SetLimit(top);
        }

        public IEnumerable<T> Top<T>(int top, IMongoQuery query)
        {
            return GetCollection<T>().Find(query).SetLimit(top);
        }

        public IEnumerable<T> Top<T>(int top, IMongoFields fields, IMongoQuery query)
        {
            return GetCollection<T>().Find(query).SetFields(fields).SetLimit(top);
        }

        #endregion


        #region Delete


        public void Remove<T>(IMongoQuery query)
        {
            GetCollection<T>().Remove(query);
        }

        public void Delete<T>(IMongoQuery query)
        {
            Remove<T>(query);
        }

        /// <summary>
        /// Deletes an entity from the repository by its id.
        /// </summary>
        /// <param name="id">The string representation of the entity's id.</param>
        public void Delete<T>(string id)
        {
            Delete<T>(typeof(T).IsSubclassOf(typeof(IObject))
                                          ? Query.EQ("_id", new ObjectId(id))
                                          : Query.EQ("_id", id));

        }

        public void Delete<T>(ObjectId id)
        {
            Delete<T>(Query.EQ("_id", id));
        }

        public void Remove<T>(string id)
        {
            Delete<T>(id);
        }

        public void Remove<T>(ObjectId id)
        {
            Delete<T>(id);
        }

        public void Delete<T>(string[] id)
        {
            IMongoQuery _query;

            if (typeof(T).IsSubclassOf(typeof(IObject)))
            {
                var _ids = new ObjectId[id.Length];

                for (int i = 0; i < id.Length; i++)
                {
                    _ids[i] = ObjectId.Parse(id[i]);
                }

                _query = Query.In("_id", new BsonArray(_ids));

            }
            else
            {
                _query = Query.In("_id", new BsonArray(id));
            }

            Remove<T>(_query);

        }

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete<T>(T entity) where T : IObject
        {
            Delete<T>(entity.Id);
        }

        /// <summary>
        /// Deletes the entities matching the criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        public void Delete<T>(Expression<Func<T, bool>> criteria) where T : IObject
        {
            var list = GetCollection<T>().AsQueryable<T>().Where(criteria);
            foreach (T entity in list)
            {
                Delete<T>(entity.Id.ToString());
            }
        }

        /// <summary>
        /// Deletes all entities in the repository.
        /// </summary>
        public void DeleteAll<T>()
        {
            GetCollection<T>().RemoveAll();
        }
        #endregion

        #region Count

        public long Count<T>(Expression<Func<T, bool>> criteria)
        {
            return GetCollection<T>().AsQueryable<T>().LongCount(criteria);
        }

        public long Count<T>(IMongoQuery query)
        {
            return GetCollection<T>().Count(query);
        }

        public long Count<T>()
        {
            return GetCollection<T>().Count();
        }


        #endregion

        #region Exists
        /// <summary>
        /// Checks if the entity exists for given criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>true when an entity matching the criteria exists, false otherwise.</returns>
        public bool Exists<T>(Expression<Func<T, bool>> criteria)
        {
            return GetCollection<T>().AsQueryable<T>().Any(criteria);
        }

        /// <summary>
        /// Checks if the entity exists for given criteria.
        /// </summary>
        /// <param name="query">The IMongoQuery.</param>
        /// <returns>true when an entity matching the criteria exists, false otherwise.</returns>
        public bool Exists<T>(IMongoQuery query)
        {
            return GetCollection<T>().Find(query).Any();
        }


        public bool Exists<T>(string id)
        {
            return GetCollection<T>().Find(Query.EQ("_id", ObjectId.Parse(id))).Any();
        }



        #endregion

        #region Drop Collection
        /// <summary>
        /// Remove a collection type T in the database.
        /// </summary>
        public void Drop<T>()
        {
            Database.DropCollection(MongoExtensions.GetCollectionName<T>());
        }

        public void Drop(string collectionName)
        {
            Database.DropCollection(collectionName);
        }

        #endregion

        #region Distinct
        public IEnumerable<BsonValue> Distinct<T>(string field, IMongoQuery query)
        {
            return GetCollection<T>().Distinct(field, query);
        }
        #endregion

        #region MapReduce


        public MapReduceResult MapReduce<T>(IMongoQuery query, string map, string reduce, string finalize)
        {
            var _options = MapReduceOptions.SetFinalize(finalize).SetOutput(MapReduceOutput.Inline);
            return MapReduce<T>(query, map, reduce, _options);
        }

        public MapReduceResult MapReduce<T>(IMongoQuery query, string map, string reduce, IMongoMapReduceOptions options)
        {

            return GetCollection<T>().MapReduce(query, map, reduce, options);
        }

        public MapReduceResult MapReduce<T>(IMongoQuery query, string map, string reduce)
        {
            return MapReduce<T>(query, map, reduce, MapReduceOptions.SetOutput(MapReduceOutput.Inline));
        }

        public MapReduceResult MapReduce<T>(string map, string reduce)
        {
            return MapReduce<T>(Query.Null, map, reduce);
        }



        #endregion



        /*public Aggregate<T>(params BsonDocument[] op)
        {
            GetCollection<T>()
        }*/

        #region Dispose
        private bool _disposed;

        public void Dispose()
        {

            Database.RequestDone();

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool _disposing)
        {
            if (!_disposed)
            {
                if (_disposing)
                {
                    //cleanup managed resources


                    GC.SuppressFinalize(this);
                }

                //cleanup unmanaged resources

                _disposed = true;
            }
        }

        #endregion
    }
}
