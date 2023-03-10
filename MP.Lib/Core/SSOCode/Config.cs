using System.Configuration;

namespace MP.Lib.Core.SSOCode
{
    public class Config
    {
        public Config()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Cookie timeout value in minutes in web.config
        /// </summary>
        public static int AUTH_COOKIE_TIMEOUT_IN_MINUTES
        {
            get
            {
                string StrCookieTimeout = ConfigurationManager.AppSettings[AppConstants.Configuration.AUTH_COOKIE_TIMEOUT_IN_MINUTES];
                int CookieTimeout = 30; //Default value
                int.TryParse(StrCookieTimeout, out CookieTimeout);

                return CookieTimeout;
            }
        }

        /// <summary>
        /// Sliding expiration
        /// </summary>
        public static bool SLIDING_EXPIRATION
        {
            get
            {
                string StrSlidingExpiration = ConfigurationManager.AppSettings[AppConstants.Configuration.SLIDING_EXPIRATION];
                bool SlidingExpiration = true; //Default value
                bool.TryParse(StrSlidingExpiration, out SlidingExpiration);

                return SlidingExpiration;
            }
        }

        /// <summary>
        /// Sliding expiration
        /// </summary>
        public static string DEFAULT_URL
        {
            get
            {
                return ConfigurationManager.AppSettings[AppConstants.Configuration.DEFAULT_URL];
            }
        }
    }
}