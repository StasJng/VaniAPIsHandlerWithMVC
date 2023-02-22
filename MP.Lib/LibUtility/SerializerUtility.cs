using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
namespace MP.Lib.LibUtility
{
    public static class SerializerUtility
    {
        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("objectToSerialize must not be null");
            }
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(ms, obj);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        public static T ToObject<T>(this string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    return (T)serializer.ReadObject(ms);
                }
            }
            else
            {
                return default(T);
            }
        }
    }
}