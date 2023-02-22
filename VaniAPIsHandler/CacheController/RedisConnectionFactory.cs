using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackExchange.Redis;

namespace VaniAPIsHandler.CacheController
{
    public class RedisConnectionFactory
    {
        private static readonly Lazy<ConnectionMultiplexer> Connection;

        static RedisConnectionFactory()
        {
            var connectionString = "cache1";//System.Configuration.ConfigurationManager.AppSettings["RedisConnection"].ToString();

            var options = ConfigurationOptions.Parse(connectionString);

            Connection = new Lazy<ConnectionMultiplexer>(
                () => ConnectionMultiplexer.Connect(options)
            );
        }

        public static ConnectionMultiplexer GetConnection() { return Connection.Value; }
    }

    //class RedisConnectionFactory
    //{
    //    private static readonly Lazy<ConnectionMultiplexer> Connection;

    //    private static Lazy<ConfigurationOptions> configurationOptions = new Lazy<ConfigurationOptions>(() =>
    //    {
    //        var configurationOptions = new ConfigurationOptions();
    //        configurationOptions.EndPoints.Add("127.0.0.1", 6379);
    //        configurationOptions.Password = "<PASS>";
    //        configurationOptions.ClientName = "JakubStompor";
    //        configurationOptions.AbortOnConnectFail = false;
    //        configurationOptions.Ssl = false;
    //        configurationOptions.ConnectTimeout = 1000;
    //        configurationOptions.SyncTimeout = 3000;
    //        return configurationOptions;
    //    });

    //    static RedisConnectionFactory()
    //    {
    //        Connection = new Lazy<ConnectionMultiplexer>(
    //            () => ConnectionMultiplexer.Connect(configurationOptions.Value)
    //        );
    //    }
    //    public static ConnectionMultiplexer GetConnection() => Connection.Value;
    //}
}