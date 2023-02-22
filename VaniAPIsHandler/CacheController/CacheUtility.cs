using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MP.Lib.LibUtility;
using ServiceStack.Redis;

namespace VaniAPIsHandler.CacheController
{
    public class CacheUtility
    {
        ////scuz
        ///// <summary>
        ///// Set all cache key by Module. This function using save all cache key by module. MP Backend list all cache key to manager
        ///// </summary>
        ///// <param name="module"></param>
        ///// <param name="cachekey"></param>
        ///// <returns></returns>
        //public static bool UpdateModuleCacheKey(string module, string cachekey)
        //{
        //    List<string> CacheKeys = new List<string>();
        //    bool ret = false;
        //    if (!string.IsNullOrEmpty(module) && !string.IsNullOrEmpty(cachekey))
        //    {
        //        RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //        if (redisClient.Get(module) == null)
        //        {
        //            CacheKeys.Add(cachekey);
        //            redisClient.Set(module, CacheKeys);
        //            ret = true;
        //        }
        //        else {
        //            CacheKeys = redisClient.Get<List<string>>(module);
        //            IEnumerable<string> matchingvalues = CacheKeys.Where(stringToCheck => stringToCheck.Contains(cachekey));
        //            if (matchingvalues == null || matchingvalues.LongCount() == 0) {
        //                CacheKeys.Add(cachekey);
        //                redisClient.Set(module, CacheKeys);
        //                ret = true;
        //            }
        //        }
                
        //    }
        //    return ret;
        //}
        ///// <summary>
        ///// Get All Cache key by Modules
        ///// </summary>
        ///// <param name="module"></param>
        ///// <returns></returns>
        //public static List<string> GetAllCacheByKey(string module,out string filesize) {
        //    long size = 0;
        //    filesize = string.Empty;
        //    List<string> CacheKeys = new List<string>();
        //    RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //    CacheKeys = redisClient.Get<List<string>>(module);
        //    //size += redisClient.Get(module).Length;
        //    if (CacheKeys != null && CacheKeys.LongCount() > 0)
        //    {
        //        //foreach (string i in CacheKeys) {
        //        //    if (redisClient.Get(i) != null)
        //        //    {
        //        //        size += redisClient.Get(i).Length;
        //        //    }
        //        //}
        //        //filesize = SizeSuffix(size);
        //    }
        //    return CacheKeys;
        //}
        //public static string GetCacheSizeByKey(string key)
        //{
        //    long size = 0;
        //    string cacheSize = string.Empty;
        //    RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //    if (!string.IsNullOrEmpty(key))
        //    {

        //        if (redisClient.Get(key) != null)
        //            {
        //                size += redisClient.Get(key).Length;
        //            }
        //            cacheSize = SizeSuffix(size);
        //    }
        //    return cacheSize;
        //}
        ///// <summary>
        ///// Remove all by List Key Cache from client
        ///// </summary>
        ///// <param name="allkeys"></param>
        //public static void RemoveAllCacheByListKey(List<string> allkeys) {
        //    RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //    redisClient.RemoveAll(allkeys);
        //}

        ///// <summary>
        ///// Remove all Cache from client
        ///// </summary>
        ///// <param name="allkeys"></param>
        //public static void RemoveAllCache()
        //{
        //    RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //    redisClient.RemoveAll(redisClient.GetAllKeys());
        //}
        ///// <summary>
        ///// Auto set cache for 5 minute
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="module"></param>
        ///// <param name="value"></param>
        ///// <param name="funtionname"></param>
        ///// <param name="args"></param>
        ///// 
        //public static void SetCacheByKeyShortTime<T>(string module, T value, string functionname, params object[] args)
        //{
        //    if (IsNull<T>(value)) return;
        //    if (!string.IsNullOrEmpty(module) && value != null && !string.IsNullOrEmpty(functionname) && AppEnv.GetSetting("isdebugcachemode")=="0")
        //    {
        //        RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //        string cachekey = module + ":" + functionname + ":" + string.Join(":", args);
                
        //        redisClient.Set(cachekey, value, DateTime.Now.AddMinutes((int)CacheDefinition.TimeIntervalCaching.shortcache));
        //        UpdateModuleCacheKey(module, cachekey);
        //    }
        //}
        //public static void SetCacheByKeyLongTime<T>(string module, T value, string functionname, params object[] args)
        //{
        //    if (IsNull<T>(value)) return;
        //    if (!string.IsNullOrEmpty(module) && value != null && !string.IsNullOrEmpty(functionname) && AppEnv.GetSetting("isdebugcachemode") == "0")
        //    {
        //        RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //        string cachekey = module + ":" + functionname + ":" + string.Join(":", args);

        //        redisClient.Set(cachekey, value, DateTime.Now.AddMinutes((int)CacheDefinition.TimeIntervalCaching.longcache));
        //        UpdateModuleCacheKey(module, cachekey);
        //    }
        //}
        ///// <summary>
        ///// Auto set cache for 60 minute
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="module"></param>
        ///// <param name="value"></param>
        ///// <param name="funtionname"></param>
        ///// <param name="args"></param>
        //public static void SetCacheByKeyDefault<T>(string module, T value, string functionname, params object[] args)
        //{
        //    if (IsNull<T>(value)) return;
        //    if (!string.IsNullOrEmpty(module) && value != null && !string.IsNullOrEmpty(functionname)&&AppEnv.GetSetting("isdebugcachemode")=="0")
        //    {
        //        RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //        string cachekey = module + ":" + functionname + ":" + string.Join(":", args);
        //        redisClient.Set(cachekey, value, DateTime.Now.AddMinutes((int)CacheDefinition.TimeIntervalCaching.normalcache));
        //        UpdateModuleCacheKey(module, cachekey);
        //    }
        //}
        ///// <summary>
        ///// Set cache permanent
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="module"></param>
        ///// <param name="value"></param>
        ///// <param name="funtionname"></param>
        ///// <param name="args"></param>
        //public static void SetCacheByKeypermanent<T>(string module, T value, string functionname, params object[] args)
        //{
        //    if (IsNull<T>(value)) return;
        //    if (!string.IsNullOrEmpty(module) && value != null && !string.IsNullOrEmpty(functionname) && AppEnv.GetSetting("isdebugcachemode") == "0")
        //    {
        //        RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //        string cachekey = module + ":" + functionname + ":" + string.Join(":", args);
        //        redisClient.Set(cachekey, value, DateTime.Now.AddMinutes((int)CacheDefinition.TimeIntervalCaching.normalcache));
        //        UpdateModuleCacheKey(module, cachekey);
        //    }
        //}
        //public static bool IsNull<T>(T subject)
        //{
        //    return ReferenceEquals(subject, null);
        //}
        ////
        //public static T GetCacheByKey<T>(string module,string functionname, params object[] args){
        //    if(!string.IsNullOrEmpty(module) && !string.IsNullOrEmpty(functionname)&&AppEnv.GetSetting("isdebugcachemode")=="0"){
        //        RedisClient redisClient = new RedisClient(CacheDefinition.CacheServer);
        //        string cachekey = module + ":" + functionname + ":" + string.Join(":", args);

        //        return redisClient.Get<T>(cachekey);
        //    }
        //    return default(T);
        //}

        //public static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        //public static string SizeSuffix(Int64 value)
        //{
        //    if (value < 0) { return "-" + SizeSuffix(-value); }
        //    if (value == 0) { return "0.0 bytes"; }

        //    int mag = (int)Math.Log(value, 1024);
        //    decimal adjustedSize = (decimal)value / (1L << (mag * 10));

        //    return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        //}
    }

}