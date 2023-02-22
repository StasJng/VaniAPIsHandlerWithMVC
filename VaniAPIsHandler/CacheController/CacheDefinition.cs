using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MP.Lib.LibUtility;

namespace VaniAPIsHandler.CacheController
{
    public class CacheDefinition
    {
        #region Module Cache Definition
        public const string DealCache = "DEAL";
        public const string PartnerCache = "PARTNER";
        public const string EloyaltyCache = "ELOYALTY";
        public const string OtherCache = "OTHER";
        public const string MobileAppCache = "APPLICATION";
        public const string CommunityCache = "COMMUNITY";
        public const string HotelBooking = "HOTEL_BOOKING";
        public const string Suggestion = "Google";
        #endregion
        //Minute
        public enum TimeIntervalCaching { 
            shortestcache = 5,
            shortcache = 30,
            normalcache = 60,
            longcache = 600,
            permanent = 6000
        }

        public const string CacheServer = "cache1";//ConvertUtility.ToString(AppEnv.GetSetting("CacheServer")); //"static1";//IP cache servercache1
        public const string CacheUser = "";
        public const string CachePwd = "";
    }

}