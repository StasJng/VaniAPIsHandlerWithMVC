using System;
using System.Text.RegularExpressions;
using System.Web;
using MP.Lib.Core.SSOCode;

namespace MP.Lib.Core
{
    public class IWebContext
    {
        public static HttpContext Context { get { return HttpContext.Current; } }
        public static AccountInfo UserInfo
        {
            get
            {
                HttpCookie AuthCookie = HttpContext.Current.Request.Cookies[AppConstants.Cookie.AUTH_COOKIE];
                if (AuthCookie != null)
                {
                    return CheckCookie(AuthCookie);
                }
                return null;
            }
        }

        public static bool Logined
        {
            get
            {
                return UserInfo != null;
            }
        }
        public static bool isadmin
        {
            get
            {
                return UserInfo.is_admin ==1 ?true:false;
            }
        }
        private static AccountInfo CheckCookie(HttpCookie AuthCookie)
        {
            string Token = Utility.GetCookieValue(AuthCookie);
            DateTime expirytime = Utility.GetExpirationDate(AuthCookie);
            if (HttpContext.Current.Application[Token] == null)
            {
                RemoveCookie(AuthCookie);
                return null;
            }
            if (CookieExpired(expirytime))
            {
                RemoveCookie(AuthCookie);
                HttpContext.Current.Application[Token] = null;
                return null;

            }
            if (Config.SLIDING_EXPIRATION)
            {
                IncreaseCookieExpiryTime(AuthCookie);
            }
            if (!string.IsNullOrEmpty(Token))
            {
                return HttpContext.Current.Application[Token] as AccountInfo;
            }
            return null;
        }

        /// <summary>
        /// Increases Cookie expiry time
        /// </summary>
        /// <param name="AuthCookie"></param>
        /// <returns></returns>
        private static void IncreaseCookieExpiryTime(HttpCookie AuthCookie)
        {
            string Token = Utility.GetCookieValue(AuthCookie);
            Utility.GetExpirationDate(AuthCookie);

            HttpContext.Current.Response.Cookies.Remove(AuthCookie.Name);

            HttpCookie NewCookie = new HttpCookie(AuthCookie.Name);
            NewCookie.Value = Utility.BuildCookueValue(Token, Config.AUTH_COOKIE_TIMEOUT_IN_MINUTES);

            HttpContext.Current.Response.Cookies.Add(NewCookie);
        }

        /// <summary>
        /// Removes Cookie from the response
        /// </summary>
        /// <param name="Cookie"></param>
        private static void RemoveCookie(HttpCookie Cookie)
        {
            HttpContext.Current.Response.Cookies.Remove(Cookie.Name);

            HttpCookie myCookie = new HttpCookie(Cookie.Name);
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

        /// <summary>
        /// Determines whether the Cookie is expired or not
        /// </summary>
        /// <param name="expirytime"></param>
        /// <returns></returns>
        private static bool CookieExpired(DateTime expirytime)
        {
            return expirytime.CompareTo(DateTime.Now) < 0;
        }

        public static bool IsEmail(string input)
        {
            if (input.Trim() == string.Empty) return false;
            Regex mailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (mailRegex.IsMatch(input))
                return true;
            return false;
        } 

        // public static int PageIndex
        //{
        //    get
        //    {
        //        int p;

        //        var _pageIndex = Context.Request["P"] ?? Context.Request["pageIndex"];

        //        int.TryParse(_pageIndex, out p);

        //        return p > 0 ? p : 1;
        //    }
        //}
        // public static int PageSize
        // {
        //     get
        //     {
        //         int p;

        //         var _pageIndex = Context.Request["S"] ?? Context.Request["pageSize"];

        //         int.TryParse(_pageIndex, out p);

        //         return p > 0 ? p : 1;
        //     }
        // }
    }
}
