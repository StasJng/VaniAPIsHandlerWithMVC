using System;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace MP.Lib.Core.MongoDb
{
  public  static class MongoExtensions
    {

      public static dynamic ToDynamic(this BsonDocument doc)
      {
          var _json = doc.ToJson();
          dynamic _obj = JToken.Parse(_json);
          return _obj;
      }

      public static string GetCollectionName<T>()
      {
          var _att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
          string _collectionName = _att != null ? ((CollectionName)_att).Name : typeof(T).Name;

          if (string.IsNullOrEmpty(_collectionName))
          {
              throw new ArgumentException("Collection name cannot be empty for this entity");
          }
          return _collectionName;
      }
    }
}
