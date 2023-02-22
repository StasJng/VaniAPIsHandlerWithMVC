using System.Configuration;

namespace MP.Lib.Core.SSOCode
{
    public class AppConstants
    {
        /// <summary>
        /// Url Parameter constant class
        /// </summary>
        public class UrlParams
        {
            public const string TOKEN = "Token";
            public const string ACTION = "Action";
            public const string REQUEST_ID = "RequestId";
            public const string RETURN_URL = "ReturnUrl";
        }

        /// <summary>
        /// Constant class for Cookie
        /// </summary>
        public class Cookie
        {
            public const string AUTH_COOKIE = "AUTH_COOKIE";
            public const string SLIDING_EXPIRATION = "SLIDING_EXPIRATION";
        }

        /// <summary>
        /// Constant class for Parameter values
        /// </summary>
        public class ParamValues
        {
            public const string LOGOUT = "Logout";
            public const string LOGINCOUNT = "LOGINCOUNT";
        }

        /// <summary>
        /// Constant class for Configuration keys
        /// </summary>
        public class Configuration
        {
            public const string AUTH_COOKIE_TIMEOUT_IN_MINUTES = "AUTH_COOKIE_TIMEOUT_IN_MINUTES";
            public const string SLIDING_EXPIRATION = "SLIDING_EXPIRATION";
            public const string DEFAULT_URL = "DEFAULT_URL";
            public const string SSO_ALLOW_DOMAIN = "SSO_ALLOW_DOMAIN";
        }

        /// <summary>
        /// Constant class for Session keys
        /// </summary>
        public class SessionParams
        {
            public const string DO_NOT_REDIRECT = "DO_NOT_REDIRECT";
        }

        /// <summary>
        /// Check url sso allow
        /// </summary>
        /// <param name="domainSSO">Domain current is calling</param>
        /// <returns>True the domain current is allow else return false</returns>
        public static bool IsUrlSSO(string domainSSO)
        {
            bool result = false;
            string domainMaster = ConfigurationManager.AppSettings["DOMAIN_MASTER"].Trim();
            if (System.String.Compare(domainSSO, domainMaster, System.StringComparison.Ordinal) != 0)
            {
                string configDomain = ConfigurationManager.AppSettings[Configuration.SSO_ALLOW_DOMAIN];
                string[] arrayDomain = configDomain.Split(';');
                if (arrayDomain.Length > 0)
                {
                    string domainSetting;
                    for (int i = 0; i < arrayDomain.Length; i++)
                    {
                        domainSetting = arrayDomain[i].ToLower().Trim();
                        if (domainSSO == domainSetting || domainSSO == "www." + domainSetting)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}