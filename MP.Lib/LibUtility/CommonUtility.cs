using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using MP.Lib.ImageService;
using System.Text.RegularExpressions;
using System.Text;
using System.Resources;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Reflection;

namespace MP.Lib.LibUtility
{
    public static class CommonUtility
    {

        #region Avatar
        static string DomailLocated(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return url;
            string domailConfig = AppEnv.GetSetting("imageDomain");
            string[] arrDomainConfig = domailConfig.Trim().Trim(';').Split(';');
            foreach (string str in arrDomainConfig)
            {
                string[] paserItem = str.Split('=');
                if (paserItem.Length > 1)
                {
                    url = url.Replace(paserItem[0], paserItem[1]);
                }
            }
            return url;
            //if(url.ToLower().StartsWith(AppEnv.GetSetting())
        }
        static string DomailLocated(string url, int w, int h)
        {
            if (string.IsNullOrWhiteSpace(url)) return url;
            string domailConfig = AppEnv.GetSetting("imageDomain");
            string[] arrDomainConfig = domailConfig.Trim().Trim(';').Split(';');
            foreach (string str in arrDomainConfig)
            {
                string[] paserItem = str.Split('=');

                //if (paserItem.Length > 1)
                //{
                //    url = url.Replace(paserItem[0], paserItem[1] + "/WebAvatar" + w + "_" + h);
                //}
                //Ngnix version
                if (paserItem.Length > 1)
                {
                    if (w == 265)
                        w = 318;
                    if (h == 220)
                        h = 264;
                    string key = url.Replace(paserItem[0], "/img/s" + w + "x" + h) + " deal2day2O15";
                    byte[] md5 = SecurityMethod.CreateDefaultMD5(key);
                    string base64 = Convert.ToBase64String(md5);
                    url = url.Replace(paserItem[0], paserItem[1] + "/img/s" + w + "x" + h)
                        + "?sign=" + Convert.ToBase64String(SecurityMethod.CreateDefaultMD5(key)).Replace("+", "-").Replace("/", "_").TrimEnd('=');
                }
            }

            return url;
            //if(url.ToLower().StartsWith(AppEnv.GetSetting())
        }
        public static string getAvatarHotel(string avatar, int width, int height)
        {
            string url = "";
            if (!string.IsNullOrEmpty(avatar))
            {
                string format = "http://cdn.dealtoday.vn/safe_image?url={0}&sign={1}&w={2}&h={3}&crop=1";
                string key = string.Format("/safe_image{0}{1}{2} deal2day2O15", width, height, avatar);
                url = string.Format(format, avatar, Convert.ToBase64String(SecurityMethod.CreateDefaultMD5(key)).Replace("+", "-").Replace("/", "_").TrimEnd('='), width, height);
            }
            else
            {
                url = "https://www.dealtoday.vn/AssetBooking/Images/defaultHotelImg.jpg";
            }
            return url;
        }
        public static string getAvatarHotel(string avatar)
        {
            string url = "";
            if (!string.IsNullOrEmpty(avatar))
            {
                string format = "http://cdn.dealtoday.vn/safe_image?url={0}&sign={1}";
                string key = string.Format("/safe_image{0} deal2day2O15", avatar);
                url = string.Format(format, avatar, Convert.ToBase64String(SecurityMethod.CreateDefaultMD5(key)).Replace("+", "-").Replace("/", "_").TrimEnd('='));
            }
            else
            {
                url = "https://www.dealtoday.vn/AssetBooking/Images/defaultHotelImg.jpg";
            }
            return url;
        }
        public static string GetAvatarThumnail(string avatar, int width, int height)
        {
            return AvatarPrefix(avatar, width, height);
        }

        public static string GetAvatarThumnailCrop(string avatar, int w, int h)
        {
            if (string.IsNullOrWhiteSpace(avatar)) return avatar;
            string domailConfig = AppEnv.GetSetting("imageDomain");
            string[] arrDomainConfig = domailConfig.Trim().Trim(';').Split(';');
            foreach (string str in arrDomainConfig)
            {
                string[] paserItem = str.Split('=');

                //if (paserItem.Length > 1)
                //{
                //    url = url.Replace(paserItem[0], paserItem[1] + "/WebAvatar" + w + "_" + h);
                //}
                //Ngnix version
                if (paserItem.Length > 1)
                {
                    if (w == 265)
                        w = 318;
                    if (h == 220)
                        h = 264;
                    string key = avatar.Replace(paserItem[0], "/img/c" + w + "x" + h) + " deal2day2O15";
                    byte[] md5 = SecurityMethod.CreateDefaultMD5(key);
                    string base64 = Convert.ToBase64String(md5);
                    avatar = avatar.Replace(paserItem[0], paserItem[1] + "/img/c" + w + "x" + h)
                        + "?sign=" + Convert.ToBase64String(SecurityMethod.CreateDefaultMD5(key)).Replace("+", "-").Replace("/", "_").TrimEnd('=');
                }
            }
            return avatar;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Vitual part</param>
        /// <param name="w">image width</param>
        /// <param name="h">image height</param>
        /// <returns>url</returns>
        public static string AvatarPrefix(this string url, int w, int h)
        {
            url = DomailLocated(url, w, h);
            if (string.IsNullOrWhiteSpace(url)) url = AppEnv.GetSetting("defaultAvatar");
            string imageUrlPrefix = AppEnv.GetSetting("imageUrlPrefix");
            if (!string.IsNullOrEmpty(url) && !url.StartsWith("//") && !url.ToLower().StartsWith("http://"))
            {
                url = imageUrlPrefix + url;
            }
            return url;
        }
        //
        public static string ManualGenerateAvatar(this string url, int w, int h)
        {
            string avatar = ConvertUtility.ToString(url);

            ImagesResizeSoapClient service = new ImagesResizeSoapClient();
            service.GenerateAvatarThumnail(avatar, w, h);
            int splitIndex = avatar.LastIndexOf("/");
            if (splitIndex >= 0)
                avatar = avatar.Replace(WebConfigurationManager.AppSettings["ftpsid"], WebConfigurationManager.AppSettings["3fd51f24d27243d2baa23dec682ee88e"] + "/" + "WebAvatar" + w + "_" + h + "/");
            else return string.Empty;


            avatar = DomailLocated(avatar, w, h);
            if (string.IsNullOrWhiteSpace(url)) url = AppEnv.GetSetting("defaultAvatar");
            string imageUrlPrefix = AppEnv.GetSetting("imageUrlPrefix");
            if (!string.IsNullOrEmpty(avatar) && !avatar.StartsWith("//") && !avatar.ToLower().StartsWith("http://"))
            {
                avatar = imageUrlPrefix + avatar;
            }
            return avatar;
        }
        //
        public static string AvatarPrefix(this string url)
        {
            //GenerateAvatarThumnail(url, w, h);
            url = DomailLocated(url);
            if (string.IsNullOrWhiteSpace(url)) url = AppEnv.GetSetting("defaultAvatar");
            string imageUrlPrefix = AppEnv.GetSetting("imageUrlPrefix");
            if (!string.IsNullOrEmpty(url) && !url.StartsWith("//") && !url.ToLower().StartsWith("http://"))
            {
                url = imageUrlPrefix + url;
            }
            return url;
        }
        //hotel
        public static string AvatarHotelPrefix(this string url)
        {
            //GenerateAvatarThumnail(url, w, h);
            string imageUrlPrefix = AppEnv.GetSetting("imageUrlPrefix");
            if (!string.IsNullOrEmpty(url) && url.Contains(AppEnv.GetSetting("ftpsid")))
            {
                url = imageUrlPrefix + "/hotels" + url.Replace(AppEnv.GetSetting("ftpsid"), "");
            }
            return url;
        }
        #region getavatarThumb user
        public static string AvatarPrefixUser(this string url)
        {
            //GenerateAvatarThumnail(url, w, h);
            url = DomailLocatedUser(url);
            if (string.IsNullOrWhiteSpace(url)) url = AppEnv.GetSetting("defaultAvatar");
            return url;
        }
        public static string AvatarPrefixUser(this string url, int w, int h)
        {
            url = DomailLocatedUser(url, w, h);
            if (string.IsNullOrWhiteSpace(url)) url = AppEnv.GetSetting("defaultAvatar");
            return url;
        }
        static string DomailLocatedUser(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return url;
            string domailConfig = AppEnv.GetSetting("imageDomainUserUpload");
            string[] arrDomainConfig = domailConfig.Trim().Trim(';').Split(';');
            foreach (string str in arrDomainConfig)
            {
                string[] paserItem = str.Split('=');
                if (paserItem.Length > 1)
                {
                    url = url.Replace(paserItem[0], paserItem[1] + "userimg");
                }
            }
            return url;
            //if(url.ToLower().StartsWith(AppEnv.GetSetting())
        }
        static string DomailLocatedUser(string url, int w, int h)
        {
            if (string.IsNullOrWhiteSpace(url)) return url;
            string domailConfig = AppEnv.GetSetting("imageDomainUserUpload");
            string[] arrDomainConfig = domailConfig.Trim().Trim(';').Split(';');
            foreach (string str in arrDomainConfig)
            {
                string[] paserItem = str.Split('=');
                //Ngnix version
                if (paserItem.Length > 1)
                {
                    if (w == 265)
                        w = 318;
                    if (h == 220)
                        h = 264;
                    string key = url.Replace(paserItem[0], "/img/user/s" + w + "x" + h) + " deal2day2O15";
                    byte[] md5 = SecurityMethod.CreateDefaultMD5(key);
                    string base64 = Convert.ToBase64String(md5);

                    url = url.Replace(paserItem[0], paserItem[1] + "/img/user/s" + w + "x" + h) + "?sign=" + Convert.ToBase64String(SecurityMethod.CreateDefaultMD5(key)).Replace("+", "-").Replace("/", "_").TrimEnd('=');
                }
            }
            return url;
            //if(url.ToLower().StartsWith(AppEnv.GetSetting())
        }
        public static string GetAvatarThumnailUser(string avatar, int width, int height)
        {
            return AvatarPrefixUser(avatar, width, height);
        }
        #endregion

        public static string GetAvatarThumnail(object avt, int width, int height)
        {

            string avatar = ConvertUtility.ToString(avt);
            GenerateAvatarThumnail(avatar, width, height);

            int splitIndex = avatar.LastIndexOf("/");
            if (splitIndex >= 0)
                return avatar.Replace(WebConfigurationManager.AppSettings["ftpsid"], WebConfigurationManager.AppSettings["3fd51f24d27243d2baa23dec682ee88e"] + "/" + "WebAvatar" + width + "_" + height + "/");
            else return string.Empty;

        }

        public static void GenerateAvatarThumnail(string avatar, int width, int height)
        {
            ImagesResizeSoapClient service = new ImagesResizeSoapClient();
            service.GenerateAvatarThumnail(avatar, width, height);
        }
        #endregion


        public static string GetValueRating(int point)
        {
            if (point >= 9 && point <= 10)
            {
                return "Tuyệt vời";
            }
            else if (point >= 7 && point < 9)
            {
                return "Khá tốt";
            }
            else if (point >= 5 && point < 7)
            {
                return "Trung bình";
            }
            else
            {
                return "kém";
            }
        }
        public static int GetPercenPromotion(double OriginalPrice, double SalePrice)
        {
            double result = (OriginalPrice - SalePrice) / (OriginalPrice / 100);
            return (int)Math.Round(result);
        }
        #region Other
        public static string ToK(this int numberVal)
        {
            string ret = "";
            if (numberVal >= 1000000)
            {
                double divison = (double)numberVal / (double)1000000;
                ret = Math.Round(divison, 2).ToString() + "M";
            }
            else if (numberVal > 1000 && numberVal % 1000 == 0)
            {
                ret = (numberVal / 1000).ToString("N0") + "k";
            }
            else
            {
                ret = numberVal.ToString("N0");
            }
            return ret;
        }
        public static string RemoveQueryStringByKey(string url, string key)
        {
            var indexOfQuestionMark = url.IndexOf("?");
            if (indexOfQuestionMark == -1)
            {
                return url;
            }

            var result = url.Substring(0, indexOfQuestionMark);
            var queryStrings = url.Substring(indexOfQuestionMark + 1);
            var queryStringParts = queryStrings.Split(new[] { '&' });
            var isFirstAdded = false;

            for (int index = 0; index < queryStringParts.Length; index++)
            {
                var keyValue = queryStringParts[index].Split(new char[] { '=' });
                if (keyValue[0] == key)
                {
                    continue;
                }

                if (!isFirstAdded)
                {
                    result += "?";
                    isFirstAdded = true;
                }
                else
                {
                    result += "&";
                }

                result += queryStringParts[index];
            }

            return result;
        }
        #endregion

        public static string ReplaceContentDetail(this string input)
        {
            try
            {
                string sid = AppEnv.GetSetting("ftpsid");
                string cdn = AppEnv.GetSetting(sid);
                return input.Replace(sid, cdn);
            }
            catch (Exception)
            {
                return input;
            }
        }

        public static string RenderPartialView(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static string RenderButtonBuy(DateTime sellOn, DateTime sellExpired, int dealId)
        {
            string button = "";
            if (sellOn > DateTime.Now)
            {
                button = "<a href=\"javascript:void(0)\" class=\"btn-buy\">Sắp bán</a>";
            }
            else if (sellOn < DateTime.Now && sellExpired > DateTime.Now)
            {
                button = string.Format("<a href=\"javascript:void(0)\" it=\"{0}\" class=\"addCart btn-buy\">Mua</a>", dealId);
            }
            else
            {
                button = "<a href=\"javascript:void(0)\" class=\"btn-buy btn-invisible\">Hết hạn</a>";
            }
            return button;

        }

        public static bool IsMobileNumber(string mobilenumber)
        {
            string list = AppEnv.GetSetting("ispremobilenumber"); //"0164,0163,0129,091,094,0123,0125,0127,097,098,0165,0166,0167,0168,0169,090,093,0121,0122,0126,0128,092,095,0199,096,0188,0124,099";
            bool retval = false;
            if (mobilenumber.Length > 9 && mobilenumber.Length < 12)
            {
                for (int j = 0; j < mobilenumber.Length; j++)
                {
                    char c = mobilenumber[j];
                    if (!(c >= '0' && c <= '9')) return false;
                }


                string[] valid = list.Split(',');
                int index = -1;
                for (int i = 0; i < valid.Length; i++)
                {
                    index = mobilenumber.Substring(0, 5).ToString().IndexOf(valid[i]);
                    if (index >= 0) break;

                }
                if (index >= 0)
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                }
            }
            else
            {
                return retval;
            }
            return retval;
        }
        public static bool IsViettelMobileNumber(string mobilenumber)
        {
            string list = AppEnv.GetSetting("viettelnumber"); //"0164,0163,0129,091,094,0123,0125,0127,097,098,0165,0166,0167,0168,0169,090,093,0121,0122,0126,0128,092,095,0199,096,0188,0124,099";
            bool retval = false;
            if (mobilenumber.Length > 9 && mobilenumber.Length < 12)
            {
                for (int j = 0; j < mobilenumber.Length; j++)
                {
                    char c = mobilenumber[j];
                    if (!(c >= '0' && c <= '9')) return false;
                }


                string[] valid = list.Split(',');
                int index = -1;
                for (int i = 0; i < valid.Length; i++)
                {
                    index = mobilenumber.Substring(0, 5).ToString().IndexOf(valid[i]);
                    if (index >= 0) break;

                }
                if (index >= 0)
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                }
            }
            else
            {
                return retval;
            }
            return retval;
        }
        public static bool IsVinaMobileNumber(string mobilenumber)
        {
            string list = AppEnv.GetSetting("vinanumber"); //"0164,0163,0129,091,094,0123,0125,0127,097,098,0165,0166,0167,0168,0169,090,093,0121,0122,0126,0128,092,095,0199,096,0188,0124,099";
            bool retval = false;
            if (mobilenumber.Length > 9 && mobilenumber.Length < 12)
            {
                for (int j = 0; j < mobilenumber.Length; j++)
                {
                    char c = mobilenumber[j];
                    if (!(c >= '0' && c <= '9')) return false;
                }


                string[] valid = list.Split(',');
                int index = -1;
                for (int i = 0; i < valid.Length; i++)
                {
                    index = mobilenumber.Substring(0, 5).ToString().IndexOf(valid[i]);
                    if (index >= 0) break;

                }
                if (index >= 0)
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                }
            }
            else
            {
                return retval;
            }
            return retval;
        }
        public static bool IsMobifoneMobileNumber(string mobilenumber)
        {
            string list = AppEnv.GetSetting("mobifonenumber"); //"0164,0163,0129,091,094,0123,0125,0127,097,098,0165,0166,0167,0168,0169,090,093,0121,0122,0126,0128,092,095,0199,096,0188,0124,099";
            bool retval = false;
            if (mobilenumber.Length > 9 && mobilenumber.Length < 12)
            {
                for (int j = 0; j < mobilenumber.Length; j++)
                {
                    char c = mobilenumber[j];
                    if (!(c >= '0' && c <= '9')) return false;
                }


                string[] valid = list.Split(',');
                int index = -1;
                for (int i = 0; i < valid.Length; i++)
                {
                    index = mobilenumber.Substring(0, 5).ToString().IndexOf(valid[i]);
                    if (index >= 0) break;

                }
                if (index >= 0)
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                }
            }
            else
            {
                return retval;
            }
            return retval;
        }

        public static string GetWebRequest(string method, string url, string strObj, WebHeaderCollection headers)
        {
            string ret = "";
            try
            {
                // Create a request for the URL.   
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                // If required by the server, set the credentials.  
                request.Credentials = CredentialCache.DefaultCredentials;
                request.ContentType = "application/json";
                request.Accept = "application/json";
                if (headers != null)
                {
                    request.Headers = headers;
                }
                request.Method = method;
                //set data
                if (!string.IsNullOrEmpty(strObj))
                {
                    StreamWriter streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(strObj);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                // Get the response.  
                WebResponse response = request.GetResponse();
                // Display the status.  
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                if (((HttpWebResponse)response).StatusDescription == HttpStatusCode.OK.ToString())
                {
                    // Get the stream containing content returned by the server.  
                    Stream dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.  
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.  
                    string responseFromServer = reader.ReadToEnd();
                    // Clean up the streams and the response.  
                    reader.Close();
                    response.Close();
                    ret = responseFromServer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("PushApi empay:" + ex.Message + ex.StackTrace);
                throw;
            }
            return ret;
        }
        public static Dictionary<string, object> ObjectProtoArr(object obj)
        {
            return obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null));
        }

    }

    public static class UrlUtility
    {
        private static string dangeChar = "%&+^#";
        private static string removeDangeChar(string ret)
        {
            if (!string.IsNullOrWhiteSpace(ret))
            {
                foreach (char str in dangeChar.ToCharArray())
                {
                    ret = ret.Replace(str, '-');
                }
            }
            return ret;
        }
        public static string ToCatUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                //Edit by Hunglee 29/03/2022: Set Url No Province
                return string.Format("/deal/{0}-{1}", url.name, url.id);//string.Format("/{2}/deal/{0}-{1}", url.name, url.id, url.provinceCode);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(url.seo))
                {
                    //Edit by Hunglee 29/03/2022: Set Url No Province
                    return string.Format("/deal/{0}-{1}/{2}/?{3}", url.name, url.id, url.seo, url.param);// string.Format("/{5}/deal/{0}-{1}/{3}/?{4}", url.name, url.id, url.seo, url.param, url.provinceCode);
                }
                else
                {
                    //Edit by Hunglee 29/03/2022: Set Url No Province
                    return string.Format("/deal/{0}-{1}/?{3}", url.name, url.id, url.seo, url.param);
                    //return string.Format("/{5}/deal/{0}-{1}/?{4}", url.name, url.id, url.seo, url.param, url.provinceCode);
                }
            }

        }
        public static string ToCmsUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName().ToLower();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/thong-tin/{0}-{1}", url.name, url.id);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(url.seo))
                {
                    return string.Format("/thong-tin/{0}-{1}/{3}/?{4}", url.name, url.id, url.seo, url.param);
                }
                else
                {
                    return string.Format("/thong-tin/{0}-{1}/?{2}", url.name, url.id, url.param);
                }
            }

        }
        public static string ToDealUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                //Edit by Hunglee 29/03/2022: Set Url No Province
                return string.Format("/{0}-{1}", url.name, url.id); //string.Format("/{2}/{0}-{1}", url.name, url.id, url.provinceCode);
            }
            else
            {
                //Edit by Hunglee 29/03/2022: Set Url No Province
                if (!string.IsNullOrWhiteSpace(url.seo))
                {
                    return string.Format("/{0}-{1}/{2}/?{4}", url.name, url.id, url.seo, url.param);// string.Format("/{5}/{0}-{1}/{3}/?{4}", url.name, url.id, url.seo, url.param, url.provinceCode);
                }
                else
                {
                    return string.Format("/{0}-{1}/?{2}", url.name, url.id, url.param);//string.Format("/{3}/{0}-{1}/?{2}", url.name, url.id, url.param, url.provinceCode);
                }
            }

        }
        public static string ToUrlName(this string s)
        {
            string UrlNameRemove = "&:/'\"!~@#$%^&*()=;,.'{}[]+?\\>";
            if (string.IsNullOrWhiteSpace(s)) s = "NA";
            s = s.Trim();
            Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string v_str_FormD = s.Normalize(NormalizationForm.FormD);
            var retVal = v_reg_regex.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            retVal = retVal.Replace(" ", "-");
            while (retVal.IndexOf("--") >= 0)
            {
                retVal = retVal.Replace("--", "-");
            }
            foreach (char c in UrlNameRemove)
            {
                retVal = retVal.Replace(c.ToString(), "");
            }
            retVal = retVal.Replace("--","-").Trim('-');
            return retVal.ToLower();
        }

        public static string ToPartnerUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/kham-pha/{0}-{1}", url.name.ToLower(), url.id);
            }
            else
            {
                return url.param;
            }
        }
        public static string ToGolfBookingDetailUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/golf/{0}-{1}", url.name.ToLower(), url.id);
            }
            else
            {
                return url.param;
            }
        }
        public static string ToPartnerCategoryUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/kham-pha/{0}/{1}", url.name.ToLower(), url.id);
            }
            else
            {
                return url.param;
            }
        }

        public static string ToPromotionUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            var urlRewrite = string.Empty;
            if (string.IsNullOrWhiteSpace(url.param))
            {
                urlRewrite = string.Format("/promotion/{0}-{1}", url.name.ToLower(), url.id);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(url.seo))
                {
                    return string.Format("/promotion/{0}-{1}/{2}/?{3}", url.name.ToLower(), url.id, url.seo, url.param);
                }
                else
                {
                    return string.Format("/promotion/{0}-{1}/?{2}", url.name.ToLower(), url.id, url.param);
                }
            }
            return urlRewrite;
        }

        public static string ToPartnerMicrositeUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/{2}/partner-mic/{0}-{1}", url.name.ToLower(), url.id, url.provinceCode);
            }
            else
            {
                return url.param;
            }
        }

        public static string ToSearchCommonUrl(UrlBaseInfo url)
        {
            //Remove param Province
            if (string.IsNullOrWhiteSpace(url.param))
            {
                //return string.Format("/{1}/search?q={0}&catId={2}", url.name, url.provinceCode, url.seo);
                return string.Format("/search?q={0}&catId={1}", url.name, url.seo);
            }
            else
            {
                return url.param;
            }
        }

        public static string ToVasSearchUrl(UrlBaseInfo url)
        {
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/vas-search?q={0}", url.name);
            }
            else
            {
                return url.param;
            }
        }


        public static string ToSearchDealUrl(UrlBaseInfo url)
        {
            //url.name = url.name.ToUrlName();
            //Remove param Province
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/deal/search?q={0}&catId={1}", url.name.ToLower(), url.seo); ;//string.Format("/{1}/deal/search?q={0}&catId={2}", url.name.ToLower(), url.provinceCode, url.seo);
            }
            else
            {
                //return string.Format("/{1}/search/deal/{0}/?{2}", url.name.ToLower(), url.provinceCode, url.param);
                return url.param;
            }
        }
        public static string ToSearchPartnerUrl(UrlBaseInfo url)
        {
            //url.name = url.name.ToUrlName();
            //Remove param Province
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/kham-pha/search?q={0}&catId={1}", url.name.ToLower(), url.seo);
            }
            else
            {
                return url.param;
            }
        }


        public static string ToTrendUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/trend/{0}", url.name.ToLower());//string.Format("/{1}/trend/{0}", url.name.ToLower(), url.provinceCode);
            }
            else
            {
                return url.param;
            }
        }

        public static string ToDealTrendUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/deal/trend/{0}-{1}", url.name.ToLower(), url.id);//string.Format("/{1}/deal/trend/{0}-{2}", url.name.ToLower(), url.provinceCode, url.id);
            }
            else
            {
                return url.param;
            }
        }

        public static string ToPartnerTrendUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/partner/trend/{0}-{1}", url.name.ToLower(), url.id);
            }
            else
            {
                return url.param;
            }
        }

        public static string ToCompareUrl(UrlBaseInfo url)
        {
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/so-sanh/{0}", url.id);
            }
            else
            {
                return url.param;
            }
        }

        public static string BuildCanonical(string curUrl)
        {
            if (curUrl.ToLower().Contains("https"))
            {
                return curUrl;
            }
            else
            {
                return curUrl.ToLower().Replace("http", "https");
            }
        }

        public static string ToDetailEgiftUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            if (string.IsNullOrWhiteSpace(url.param))
            {
                return string.Format("/the-qua-tang/{0}-{1}", url.name, url.id);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(url.seo))
                {
                    return string.Format("/the-qua-tang/{0}-{1}/{3}/?{4}", url.name, url.id, url.seo, url.param);
                }
                else
                {
                    return string.Format("/the-qua-tang/{0}-{1}/?{4}", url.name, url.id, url.seo, url.param);
                }
            }

        }
        public static string ToFaqTopicUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            return string.Format("/chu-de-ho-tro/{0}-{1}", url.name, url.id);


        }
        public static string ToDetailFaqUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            return string.Format("/ho-tro/{0}-{1}", url.name, url.id);
        }
        #region Booking Url
        //build Url trang ket qua tim kiem phong
        public static string BuildUrlRoomSearching(string id, int type, string checkin, string checkout, int room, int adult, int child, string sortby, string filterbyamenities, string filterbyprices, string filterbyname, string filterByStarRating, int pageIndex)
        {
            return string.Format("/bookingresult?id={0}&type={1}&checkin={2}&checkout={3}&room={4}&adult={5}&child={6}&sortby={7}&filterbyamenities={8}&filterbyprices={9}&filterbyname={10}&filterByStarRating={11}&curPage={12}&term={13}", id, type, checkin, checkout, room, adult, child, sortby, filterbyamenities, filterbyprices, filterbyname, filterByStarRating, pageIndex, filterbyname);
        }
        //Build Url trang chi tiet ket qua tim kiem phong
        public static string BuildUrlHotelDetail(string id, int type, string checkin, string checkout, int room, int adult, int child, string sortby, string filterbyamenities, string filterbyprices, string filterbyname, string filterByStarRating, int pageIndex, string countrycode)
        {
            return string.Format("/bookingdetail?id={0}&checkin={1}&checkout={2}&room={3}&adult={4}&child={5}&sortby={6}&filterbyamenities={7}&filterbyprices={8}&filterbyname={8}&filterByStarRating={10}&curPage={11}&countrycode={12}", id, checkin, checkout, room, adult, child, sortby, filterbyamenities, filterbyprices, filterbyname, filterByStarRating, pageIndex, countrycode);
        }
        //build Url Trang checkout step 1 --> chua login
        public static string BuildUrlRoomBookingLogin(string policyId, int child)
        {
            return string.Format("/RoomBooking?policyid={0}&child={1}", policyId, child);
        }
        //Build Url Trang thanh toan --> da login
        public static string BuildUrlRoomBookingPayment(string policyId, int child)
        {
            return string.Format("/BookingPayment?policyid={0}&child={1}", policyId, child);
        }
        //Build Url Trang Hoan tat Booking
        public static string BuildUrlRoomBookingComplete(int orderId)
        {
            return string.Format("/RoomBookingComplete", orderId);
        }
        //Build Wap Search Page
        public static string BuildWapUrlRoomSearching(string id, int type, string checkin, string checkout, int room, int adult, int child, string key, int totalNights)
        {
            return string.Format("/bookingsearch?id={0}&type={1}&checkin={2}&checkout={3}&room={4}&adult={5}&child={6}&key={7}&nights={8}", id, type, checkin, checkout, room, adult, child, key, totalNights);
        }

        #endregion

        #region Blog Url
        public static string ToCateBlogUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            return string.Format("/trai-nghiem/{0}-{1}", url.name, url.id);
        }

        public static string ToDetailBlogUrl(UrlBaseInfo url)
        {
            url.name = url.name.ToUrlName();
            return string.Format("/blog/{0}-{1}", url.name, url.id);
        }

        public static string ToSearchBlogUrl(UrlBaseInfo url)
        {

            return string.Format("/tim-kiem-blog?q={0}", url.name, url.seo);

        }
        #endregion
    }
    public class UrlBaseInfo
    {
        public string name { get; set; }
        public string seo { get; set; }
        public int id { get; set; }
        public string param { get; set; }
        public string provinceCode { get; set; }
    }
}