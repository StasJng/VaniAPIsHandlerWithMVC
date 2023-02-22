using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using MP.Lib.Contrains;
using System.Data;
namespace MP.Lib.Core
{
    public static class IWebsiteContext
    {

        public static HttpContext Context { get { return HttpContext.Current; } }
        public static string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }


        public static string OutputCacheKey
        {
            get
            {
                if (HttpContext.Current.Session["OutputCacheKey"] == null)
                {
                    Context.Session["OutputCacheKey"] = Guid.NewGuid().ToString();
                }
                return Context.Session["OutputCacheKey"].ToString();
            }
        }


        public static string AppVirtualPath
        {
            get { return HttpRuntime.AppDomainAppVirtualPath; }
        }

        public static string AppPath
        {
            get { return HttpRuntime.AppDomainAppPath; }
        }


        public static WebConfiguration Configuration
        {
            get { return WebConfiguration.Instant; }
        }

        public static string ReturnUrl
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Context.Request.QueryString["ReturnUrl"])
                                  ? Context.Request.QueryString["ReturnUrl"]
                                  : Context.Request.UrlReferrer != null ? Context.Request.UrlReferrer.ToString() : "/";
            }
        }


        public static int PageIndex
        {
            get
            {
                int p;

                var _pageIndex = Context.Request["P"] ?? Context.Request["pageIndex"];

                int.TryParse(_pageIndex, out p);

                return p > 0 ? p : 1;
            }
        }
        public static int PageSize
        {
            get
            {
                int p;

                var _pageSize = Context.Request["S"] ?? Context.Request["pageSize"];

                int.TryParse(_pageSize, out p);

                return p > 0 ? p : Convert.ToInt32(Configuration["PageSize"]);
            }
        }

        #region User and Session


        public static string ClientIp
        {
            get
            {
                string _ip = Context.Request.UserHostAddress;
                if (_ip == "::1" || string.IsNullOrEmpty(_ip)) _ip = "127.0.0.1";

                return _ip;
            }
        }

        public static string SessionID
        {
            get
            {
                if (Context.Session[CacheKeys.SESSIONID] == null)
                {
                    Context.Session[CacheKeys.SESSIONID] = Guid.NewGuid().ToString();
                }
                return Context.Session[CacheKeys.SESSIONID].ToString();


            }
        }



        #endregion

        /*
        #region Localizer

        public static Dictionary<string, NameValueCollection> Localizer
        {
            get
            {

                string _cacheKey = string.Format(CacheKeys.LOCALIZER, Configuration.Language);


                var objectValues = (Dictionary<string, NameValueCollection>)CacheHelper.Get(_cacheKey);

                if (objectValues == null)
                {
                    string _filePath = string.Format("{0}languages\\{1}.xml", AppPath, Configuration.Language);

                    if (!File.Exists(_filePath))
                        throw (new ApplicationException("Invalid language file " + _filePath));

                    var _doc = new XmlDocument();
                    _doc.Load(_filePath);

                    objectValues = new Dictionary<string, NameValueCollection>();


                    foreach (XmlNode node in _doc.DocumentElement.ChildNodes)
                    {
                        var values = new NameValueCollection();

                        var childNodes = node.ChildNodes;

                        if (childNodes.Count > 0)
                        {
                            foreach (XmlNode childNode in childNodes)
                            {
                                values.Add(childNode.Name, childNode.InnerText);
                            }
                        }

                        if (!objectValues.ContainsKey(node.Name))
                            objectValues.Add(node.Name.ToLower(), values);

                    }


                    /*if(objectValues.Count==0)
                        throw (new InvalidDataException(string.Format( "Load language file {0} data error.",_filePath)));#1#


                    CacheHelper.Add(_cacheKey, objectValues, new CacheDependency(_filePath));

                }

                return objectValues;

            }
        }


        private static NameValueCollection GetPage(string pageName)
        {
            try
            {
                return Localizer[pageName.ToLower()];
            }
            catch (Exception)
            {

                throw new ArgumentOutOfRangeException(string.Format("The key: {0} not exists.", pageName));
            }
        }


        public static string GetText(string page, string entry)
        {
            var vals = GetPage(page);

            try
            {
                return (vals != null) ? vals[entry] : String.Empty;
            }
            catch (Exception)
            {

                throw new ArgumentOutOfRangeException(string.Format("The key: {0} not exists.", entry));
            }

        }



      /*  public static IHtmlString GetMvcText(string page, string entry)
        {
            return new MvcHtmlString(GetText(page, entry));
        }#1#

        #endregion


        #region DateTime Format

        private static string GetDate(string key)
        {
            return GetText(LanguageSection.DATETIME, key);
        }

        public static TimeSpan TimeZone
        {
            get
            {
                //float min = ForumConfiguration.Instance.TimeZoneOffset * 60;
                return TimeSpan.FromHours(Configuration.TimeZoneOffset);
                // return new TimeSpan(min / 60, min % 60, 0);
            }
        }

        /// <summary>
        /// Returns the user timezone offset from GMT
        /// </summary>
        public static TimeSpan TimeZoneOffsetUser
        {
            get
            {
                /*if (UserInfo != null)
                {


                    return TimeSpan.FromHours(UserInfo.TimeZone);
                    //return new TimeSpan(min / 60, min % 60, 0);


                }#1#
                return TimeZone;
            }
        }


        public static DateTime ToUtcDateTime(this DateTime _dateTime)
        {
            try
            {
                return _dateTime - TimeZoneOffsetUser;
            }
            catch (Exception)
            {
                return DateTime.UtcNow - TimeZoneOffsetUser;
            }


        }

        public static DateTime ToUserTimeZoneDateTime(this DateTime _dateTime)
        {
            return _dateTime + TimeZoneOffsetUser;
        }





        public static string DateTimeFormat(DateTime _dateTime)
        {
            DateTime _dt = _dateTime + TimeZoneOffsetUser;

            return _dt.ToString(Configuration.DateTimeFormat);
        }

        public static string ShortDateTimeFormat(DateTime _dateTime)
        {
            DateTime _dt = _dateTime + TimeZoneOffsetUser;

            return _dt.ToString(GetDate("SHORT_DATE"));
        }

        public static string TimeFormat(DateTime _datetime)
        {

            _datetime += TimeZoneOffsetUser;
            try
            {
                return _datetime.ToString("hh:mm tt");
            }
            catch (Exception)
            {
                return _datetime.ToString("T");
            }
        }


        public static string PrettyDate(DateTime _date)
        {
            // 1.
            // Get time span elapsed since the date.
            TimeSpan _s = DateTime.UtcNow.Subtract(_date);

            // 2.
            // Get total number of days elapsed.
            var _dayDiff = (int)_s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            var _secDiff = (int)_s.TotalSeconds;

            // 4.
            // Don't allow out of range values.
            if (_dayDiff < 0 || _dayDiff >= 31)
            {
                return DateTimeFormat(_date);
            }

            // 5.
            // Handle same-day times.
            if (_dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (_secDiff < 60)
                {
                    return string.Format(GetDate("JUST_NOW"), _secDiff == 0 ? 1 : _secDiff); //"just now";
                }
                // B.
                // Less than 2 minutes ago.
                if (_secDiff < 120)
                {
                    return string.Format(GetDate("MINUTES_AGO"), 1);// "1 minute ago";
                }
                // C.
                // Less than one hour ago.
                if (_secDiff < 3600)
                {
                    return string.Format(GetDate("MINUTES_AGO"),
                        Math.Floor((double)_secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (_secDiff < 7200)
                {
                    return string.Format(GetDate("HOURS_AGO"), 1); ;
                }
                // E.
                // Less than one day ago.
                if (_secDiff < 86400)
                {
                    return string.Format(GetDate("HOURS_AGO"),
                        Math.Floor((double)_secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (_dayDiff == 1)
            {
                var _dt = _date + TimeZoneOffsetUser;
                return string.Format(GetDate("YESTERDAY_AT"), _dt);
            }
            if (_dayDiff < 7)
            {
                return string.Format(GetDate("DAYS_AGO"), _dayDiff);
            }
            return _dayDiff < 31 ? string.Format(GetDate("WEEKS_AGO"), Math.Ceiling((double)_dayDiff / 7)) : DateTimeFormat(_date);

            // Default full date format
        }

        #endregion*/

        #region Encrypt & Decrypt

        public static string Encrypt(params object[] _params)
        {

            var _sb = new StringBuilder();

            for (int i = 0; i < _params.Length; i++)
            {

                if (i > 0)
                    _sb.Append(",");
                _sb.Append(_params[i]);
            }

            // More
            _sb.AppendFormat(",{0}", SessionID);

            return Encryptor.Encrypt(_sb.ToString(), Configuration.EncryptPassword);

        }


        public static string[] Decrypt(string _encryptString)
        {

            string _plainText = Encryptor.Decrypt(_encryptString, Configuration.EncryptPassword);
            return _plainText.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        }


        #endregion


        #region PageRender


        public static string PageRender(string pageFormatString, double totalRows)
        {
            return PageRender(pageFormatString, PageIndex, Configuration.PageSize, totalRows);
        }

        public static string PageRender(string pageFormatString, int pageIndex, int pageSize, double totalRows)
        {

            var totalPage = (int)Math.Ceiling(totalRows / pageSize);

            if (totalPage <= 1) return string.Empty;

            const int pageButtonCount = 3;
            int min = pageIndex - pageButtonCount;
            int max = pageIndex + pageButtonCount;

            if (max > totalPage)
                min -= max - totalPage;
            else if (min < 1)
                max += 1 - min;

            var _sb = new StringBuilder(1000);
            bool needDiv = false;


            _sb.Append("<ul class=\"paging\">");


            /*   _sb.Append("<li class=\"page-x-of-y\"><span>");
               _sb.AppendFormat(GetText(LanguageSection.GLOBAL, "PAGE_X_OF_Y"), pageIndex, totalPage);
               _sb.Append("</span></li>");*/

            //Ren Prev Button
            if (pageIndex > 1)
            {
                _sb.AppendFormat(
               "<li class=\"{1}\"><a title=\"Go to First Page\" class=\"ajax-page\" rel=\"{2}\" href=\"{0}\"></a></li>", string.Format(pageFormatString, 1), "page-first", 1);


                _sb.AppendFormat("<li class=\"page-prev\"><a href=\"{0}\" class=\"ajax-page\" rel=\"{1}\" title=\"Go to Previous Page\">&nbsp;&nbsp;</a></li>", string.Format(pageFormatString, pageIndex - 1), pageIndex - 1);


            }



            for (int i = 1; i <= totalPage; i++)
            {
                if (i <= 2 || i > totalPage - 2 || (min <= i && i <= max))
                {

                    string _className = (i == pageIndex) ? "actived" : "page-item";

                    _sb.AppendFormat("<li class=\"{1}\"><a class=\"ajax-page\" rel=\"{2}\" href=\"{0}\">{2}</a></li>", string.Format(pageFormatString, i), _className, i);
                    needDiv = true;
                }
                else if (needDiv)
                {
                    _sb.AppendFormat("<li class=\"{0}\"><a>...</a></li>", "page-item");
                    needDiv = false;
                }
            }

            //Ren Next Button
            if (pageIndex < totalPage)
            {
                _sb.AppendFormat(
                    "<li class=\"{1}\"><a title=\"Go to Next Page\" class=\"ajax-page\" rel=\"{2}\" href=\"{0}\"></a></li>", string.Format(pageFormatString, pageIndex + 1), "page-next", pageIndex + 1);

                _sb.AppendFormat(
                  "<li class=\"{1}\"><a title=\"Go to Last Page\" class=\"ajax-page\" rel=\"{2}\" href=\"{0}\"></a></li>", string.Format(pageFormatString, totalPage), "page-last", totalPage);


            }


            _sb.Append("</ul>");

            return _sb.ToString();


        }

        public static string DefaultPageRender(string pageFormatString, double totalRows)
        {
            return DefaultPageRender(pageFormatString, PageIndex, PageSize, totalRows);
        }

        public static string DefaultPageRender(string pageFormatString, int pageIndex, int pageSize, double totalRows)
        {

            var totalPage = (int)Math.Ceiling(totalRows / pageSize);

            if (totalPage <= 1) return string.Empty;

            const int pageButtonCount = 5;
            int min = pageIndex - pageButtonCount;
            int max = pageIndex + pageButtonCount;

            if (max > totalPage)
                min -= max - totalPage;
            else if (min < 1)
                max += 1 - min;

            var _sb = new StringBuilder(1000);

            string strselected10 = pageSize == 10 ? "class=\"ajax-page\" rel=\"1\" relsize=\"10\" href=\"" + string.Format(pageFormatString, 1, 10) + "\" selected=\"selected\"" : "rel=\"1\" relsize=\"10\"  class=\"ajax-page\" href=\"" + string.Format(pageFormatString, 1, 10) + "\"";
            string strselected20 = pageSize == 20 ? "class=\"ajax-page\" rel=\"1\" relsize=\"20\" href=\"" + string.Format(pageFormatString, 1, 20) + "\" selected=\"selected\"" : "rel=\"1\" relsize=\"20\" class=\"ajax-page\" href=\"" + string.Format(pageFormatString, 1, 20) + "\"";
            string strselected30 = pageSize == 30 ? "class=\"ajax-page\" rel=\"1\" relsize=\"30\" href=\"" + string.Format(pageFormatString, 1, 30) + "\" selected=\"selected\"" : "rel=\"1\" relsize=\"30\" class=\"ajax-page\" href=\"" + string.Format(pageFormatString, 1, 30) + "\"";
            string strselected40 = pageSize == 40 ? "class=\"ajax-page\" rel=\"1\" relsize=\"40\" href=\"" + string.Format(pageFormatString, 1, 40) + "\" selected=\"selected\"" : "rel=\"1\" relsize=\"40\" class=\"ajax-page\" href=\"" + string.Format(pageFormatString, 1, 40) + "\"";
            string strselected50 = pageSize == 50 ? "class=\"ajax-page\" rel=\"1\" relsize=\"50\" href=\"" + string.Format(pageFormatString, 1, 50) + "\" selected=\"selected\"" : "rel=\"1\" relsize=\"50\" class=\"ajax-page\" href=\"" + string.Format(pageFormatString, 1, 50) + "\"";
            _sb.Append("<div class=\"box-pagination\"><span>Hiển thị</span><span><select><option " + strselected10 + " value=\"10\" >10</option><option " + strselected20 + " value=\"20\">20</option><option " + strselected30 + " value=\"30\">30</option><option " + strselected40 + " value=\"40\">40</option><option " + strselected50 + " value=\"50\">50</option></select></span><span>trong số " + totalRows.ToString("N0") + "</span><ul class=\"pagination\">");

            //Ren Prev Button
            if (pageIndex > 1)
            {
                _sb.AppendFormat("<li class=\"\"><a class=\"ajax-page\" rel=\"{1}\" relsize=\"{2}\" href=\"{0}\"><i class=\"mdi mdi-chevron-double-left\"></i></a></li>", string.Format(pageFormatString, 1, pageSize), 1, pageSize);
                _sb.AppendFormat("<li class=\"\"><a class=\"ajax-page\" rel=\"{1}\" relsize=\"{2}\"  href=\"{0}\"><i class=\"mdi mdi-chevron-left\"></i></a></li>", string.Format(pageFormatString, pageIndex - 1, pageSize), 1, pageSize);
            }




            for (int i = 1; i <= totalPage; i++)
            {
                if (i <= 2 || i > totalPage - 2 || (min <= i && i <= max))
                {

                    string _className = (i == pageIndex) ? "active" : "waves-effect";
                    _sb.AppendFormat("<li class=\"{0}\"><a class=\"ajax-page\" rel=\"{3}\" relsize=\"{4}\"  href=\"{1}\">{2}</a></li>", _className, string.Format(pageFormatString, i, pageSize), i, i, pageSize);
                }
            }

            //Ren Next Button
            if (pageIndex < totalPage)
            {
                _sb.AppendFormat("<li class=\"\"><a class=\"ajax-page\" rel=\"{1}\" relsize=\"{2}\" href=\"{0}\"><i class=\"mdi mdi-chevron-right\"></i></a></li>", string.Format(pageFormatString, pageIndex + 1, pageSize), 1, pageSize);
                _sb.AppendFormat("<li class=\"\"><a class=\"ajax-page\" rel=\"{1}\" relsize=\"{2}\" href=\"{0}\"><i class=\"mdi mdi-chevron-double-right\"></i></a></li>", string.Format(pageFormatString, totalPage, pageSize), 1, pageSize);
            }


            _sb.Append("</ul></div>");

            return _sb.ToString();


        }

        //HungMT edit paging new style
        public static string DefaultPageRenderNew(string pageFormatString, int pageIndex, int pageSize, double totalRows, string type)
        {

            var totalPage = (int)Math.Ceiling(totalRows / pageSize);

            var _sb = new StringBuilder(1000);
            _sb.Append("<div class=\"container-c\"><div class=\"row\">");
            //string strselected10 = "<li><a href=\"" + string.Format(pageFormatString, 1, 20) + "\" >20 " + type + "</a></li>";
            //string strselected20 = "<li><a href=\"" + string.Format(pageFormatString, 1, 40) + "\" >40 " + type + "</a></li>";
            //string strselected30 = "<li><a href=\"" + string.Format(pageFormatString, 1, 60) + "\" >60 " + type + "</a></li>";
            //string strselected40 = "<li><a href=\"" + string.Format(pageFormatString, 1, 80) + "\" >80 " + type + "</a></li>";
            //string strselected50 = "<li><a href=\"" + string.Format(pageFormatString, 1, 100) + "\" >100 " + type + "</a></li>";
            //_sb.Append(
            //    "<div class=\"col-xs-3\"><div class=\"page-size\"><span>Hiển thị</span><span class=\"drop-pagesize\"><input type=\"text\" value='" + pageSize + "&nbsp;" + type + "' class=\"form-control\" data-toggle=\"dropdown\" aria-expanded=\"false\"><ul class=\"dropdown-menu\">" +
            //    strselected10 + strselected20 + strselected30 + strselected40 + strselected50 +
            //    "</ul></span><span>/ 1 trang</span></div></div>");

            if (totalPage <= 1)
            {
                _sb.Append("</div></div>");
                return _sb.ToString();
            }

            const int pageButtonCount = 5;
            int min = pageIndex - pageButtonCount;
            int max = pageIndex + pageButtonCount;

            if (max > totalPage)
                min -= max - totalPage;
            else if (min < 1)
                max += 1 - min;

            _sb.Append("<div class=\"col-xs-12\"><ul class=\"pager pagination-c pull-right\">");

            //Ren Prev Button
            if (pageIndex > 1)
            {
                _sb.AppendFormat("<li class=\"\"><a class=\"ajax-page\"   href=\"{0}\">Trước</a></li>", string.Format(pageFormatString, pageIndex - 1, pageSize));
            }

            for (int i = 1; i <= totalPage; i++)
            {
                if (i <= 5 || i > totalPage - 5 || (min <= i && i <= max))
                {

                    string _className = (i == pageIndex) ? "active" : "waves-effect";
                    _sb.AppendFormat("<li class=\"{0}\"><a class=\"ajax-page\"  href=\"{1}\">{2}</a></li>", _className, string.Format(pageFormatString, i, pageSize), i);
                }
            }

            //Ren Next Button
            if (pageIndex < totalPage)
            {
                _sb.AppendFormat("<li class=\"\"><a class=\"ajax-page\"  href=\"{0}\">Sau</a></li>", string.Format(pageFormatString, pageIndex + 1, pageSize));
            }

            _sb.Append("</ul></div></div></div>");
            return _sb.ToString();
        }

        //HungMT edit paging load ajax
        public static string DefaultPageRenderAjax(string pageFormatString, int pageIndex, int pageSize, double totalRows, string type)
        {

            var totalPage = (int)Math.Ceiling(totalRows / pageSize);

            var _sb = new StringBuilder(1000);

            string strselected4 = string.Empty;
            if (type == "chương trình")
            {
                strselected4 = "<li><a href=\"javascript:void(0);\" onclick=\"PagingAjax('" + string.Format(pageFormatString, 1, 4) + "')\">4 " + type + "</a></li>";
            }
            string strselected10 = "<li><a href=\"javascript:void(0);\" onclick=\"PagingAjax('" + string.Format(pageFormatString, 1, 8) + "')\">8 " + type + "</a></li>";
            string strselected20 = "<li><a href=\"javascript:void(0);\" onclick=\"PagingAjax('" + string.Format(pageFormatString, 1, 16) + "')\">16 " + type + "</a></li>";
            string strselected30 = "<li><a href=\"javascript:void(0);\" onclick=\"PagingAjax('" + string.Format(pageFormatString, 1, 24) + "')\">24 " + type + "</a></li>";
            string strselected40 = "<li><a href=\"javascript:void(0);\" onclick=\"PagingAjax('" + string.Format(pageFormatString, 1, 32) + "')\">32 " + type + "</a></li>";
            string strselected50 = "<li><a href=\"javascript:void(0);\" onclick=\"PagingAjax('" + string.Format(pageFormatString, 1, 40) + "')\">40 " + type + "</a></li>";
            string selected = string.Format(pageSize + "&nbsp;" + type);
            _sb.Append(
                "<div class=\"container-c\"><div class=\"row\"><div class=\"col-xs-3\"><div class=\"page-size\"><span>Hiển thị</span><span class=\"drop-pagesize\"><input type=\"text\" value='" + selected + "' class=\"form-control\" data-toggle=\"dropdown\" aria-expanded=\"false\"><ul class=\"dropdown-menu\">" +
              strselected4 + strselected10 + strselected20 + strselected30 + strselected40 + strselected50 +
                "</ul></span><span>/ 1 trang</span></div></div>");

            if (totalPage <= 1)
            {
                _sb.Append("</div></div>");
                return _sb.ToString();
            }

            const int pageButtonCount = 5;
            int min = pageIndex - pageButtonCount;
            int max = pageIndex + pageButtonCount;

            if (max > totalPage)
                min -= max - totalPage;
            else if (min < 1)
                max += 1 - min;

            _sb.Append("<div class=\"col-xs-9\"><ul class=\"pager pagination-c pull-right\">");

            //Ren Prev Button
            if (pageIndex > 1)
            {
                _sb.AppendFormat("<li class=\"\"><a class=\"ajax-page\"  onclick=\"PagingAjax('{0}')\" href=\"javascript:void(0);\">Trước</a></li>", string.Format(pageFormatString, pageIndex - 1, pageSize));
            }

            for (int i = 1; i <= totalPage; i++)
            {
                if (i <= 2 || i > totalPage - 2 || (min <= i && i <= max))
                {

                    string _className = (i == pageIndex) ? "active" : "waves-effect";
                    _sb.AppendFormat("<li class=\"{0}\"><a class=\"ajax-page\" onclick=\"PagingAjax('{1}')\" href=\"javascript:void(0);\">{2}</a></li>", _className, string.Format(pageFormatString, i, pageSize), i);
                }
            }

            //Ren Next Button
            if (pageIndex < totalPage)
            {
                _sb.AppendFormat("<li class=\"\"><a class=\"ajax-page\"  onclick=\"PagingAjax('{0}')\" href=\"javascript:void(0);\">Sau</a></li>", string.Format(pageFormatString, pageIndex + 1, pageSize));
            }

            _sb.Append("</ul></div></div></div>");
            return _sb.ToString();
        }

        public static string MobilePaging(string urlformat, int total, int curp, int psite)
        {
            int totalpage = (int)Math.Ceiling((double)total / psite);
            //if (total % psite == 1) total = total + 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"pagination\">");
            if (totalpage <= 1)
            {
                return string.Empty;
            }
            if (curp > 1)
            {
                sb.AppendFormat("<div class=\"prev\"><a href=\"{0}\">« Trang trước</a></div>", string.Format(urlformat, curp - 1));
            }
            else
            {
                sb.Append("<div class=\"prev disabled\"><a href=\"javascript:\">« Trang trước</a></div>");
            }
            sb.AppendFormat("<div class=\"current\">Trang {0}</div>", curp);
            if (totalpage > 1 && totalpage > curp)
            {
                sb.AppendFormat("<div class=\"next\"><a href=\"{0}\">Trang sau »</a></div>", string.Format(urlformat, curp + 1));
            }
            else
            {
                sb.Append("<div class=\"next disabled\"><a href=\"javascript:\">Trang sau »</a></div>");
            }
            sb.Append("</div>");
            return sb.ToString();
        }

        #endregion

        #region URL Format

        public static string GetAbsoluteUrl(string _pageStringFormat, params object[] _params)
        {


            int _length = _params.Length + 2;
            var _args = new string[_length];
            _args[0] = Configuration.AbsoluteUrl;// first arg set Root Path

            for (int i = 0; i < _params.Length; i++)
            {

                if (_params[i] == null)
                    throw new NoNullAllowedException(string.Format("params[{0}] is NULL.", i));


                _args[i + 1] = _params[i].ToString();
            }

            _args[_length - 1] = Configuration.Extension; // Last arg set extension


            return string.Format(_pageStringFormat, _args);
        }

        //public static string ToPlainText(this string title)
        //{
        //    if (Configuration.Language.Equals("vn", StringComparison.OrdinalIgnoreCase))
        //        title = title.VietNamToPlainText();
        //    return title.ToFriendlyUrl();
        //}


        //public static string GroupLink(int _id, string _title, object _pageIndex)
        //{

        //    if (_pageIndex != null)
        //    {
        //        return GetAbsoluteUrl(Urls.GROUP_PAGING, _title.ToPlainText(), _id, _pageIndex);// string.Format(Urls.GROUP_PAGING, Configuration.AbsoluteUrl, _id, _pageIndex, _title.ToPlainText(), Configuration.Extension);

        //    }
        //    return GetAbsoluteUrl(Urls.GROUP_LIST, _title.ToPlainText(), _id); //string.Format(Urls.GROUP_LIST, Configuration.AbsoluteUrl, _id, _title.ToPlainText(), Configuration.Extension);

        //}

        //public static string GroupLink(int _id, string _title)
        //{
        //    return GroupLink(_id, _title, null);
        //}

        public static string DetailFriendUrl(int _id, string _title)
        {
            return GetAbsoluteUrl(Urls.NEWS_DETAIL, _title, _id.ToString("X"));
        }

        public static string DetailFriendUrl(string _title)
        {
            return DetailFriendUrl("detail", _title);// GetAbsoluteUrl(Urls.POST_DETAIL_FRIEND_URL, _prefix, _title);
        }

        public static string DetailFriendUrl(string _prefix, string _title)
        {
            return GetAbsoluteUrl(Urls.POST_DETAIL_FRIEND_URL, _prefix, _title);
        }


        public static string DetailFriendUrl(DateTime _date, string _title)
        {
            return GetAbsoluteUrl(Urls.NEWS_DETAIL_FRIEND_URL, _date.Year, _date.Month, _title);
        }

        public static string GroupFriendUrl(string _title, object _pageIndex)
        {

            if (_pageIndex != null)
            {
                return GetAbsoluteUrl(Urls.GROUP_FRIEND_URL_LIST_PAGING, _title, _pageIndex);

            }
            return GetAbsoluteUrl(Urls.GROUP_FRIEND_URL_LIST, _title);

        }

        public static string GroupFriendUrl(string _title)
        {
            return GroupFriendUrl(_title, null);
        }

        //public static string DetailLink(int _id, string _title)
        //{
        //    return GetAbsoluteUrl(Urls.NEWS_DETAIL, _title.ToPlainText(), _id.ToString("X"));
        //}

        //public static string DetailLink(DateTime _date, string _title)
        //{
        //    return GetAbsoluteUrl(Urls.NEWS_DETAIL_FRIEND_URL, _date.Year, _date.Month, _title.ToPlainText());
        //}



        public static string FindPost(string id)
        {
            return GetAbsoluteUrl(Urls.FIND_POST, id);
        }



        public static string Tag(string tag)
        {
            return GetAbsoluteUrl(Urls.TAG, Context.Server.UrlEncode(tag.Trim()));
        }

        public static string Tag(string tag, int p)
        {
            return GetAbsoluteUrl(Urls.TAG_PAGING, Context.Server.UrlEncode(tag.Trim()), string.Format("page-{0}", p));
        }


        #endregion


        #region Utils

        public static string ReadHtml(string fileName)
        {
            return FileHelper.ReadText(string.Format("{0}html/{1}.htm", AppPath, fileName));
        }

        public static String Padding(int? i)
        {


            if (!i.HasValue || i <= 1) return string.Empty;

            string _out = "|";

            for (int j = 0; j < i; j++)
            {
                _out += "-";
            }
            return _out;
        }

        /*
                public static string ImageRender(string image, int width, int height)
                {

                    return ImageRender(image, new Size { Width = width, Height = height });

                }

                public static string ImageRender(string image)
                {
                    return ImageRender(image, new Size { Width = 0, Height = 0 });

                }

                public static string ImageRender(string image, int size)
                {

                    return ImageRender(image, new Size { Width = size });


                }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">Image url</param>
        /// <param name="thumbSize">WidthxHeight</param>
        /// <returns></returns>
        public static string ImageRender(string image, string thumbSize)
        {

            if (image != null && image.Length > 3)
            {
                if (image.StartsWith("//") || image.StartsWith("http"))
                {
                    return image;
                }

                return string.Format("{0}{1}&size={2}", Configuration.ImagePrefix, image, thumbSize);
            }

            return string.Format("{0}blank.gif", ConfigurationManager.AppSettings["Cdn"]);

        }

        /* public static string ImageRender(string image, Size size)
         {
             if (image != null && image.Length > 3)
             {
                 if (image.StartsWith("//") || image.StartsWith("http"))
                 {
                     return image;
                 }

                 return string.Format("{0}{1}{2}{3}", Configuration.ImagePrefix, image, size.Width > 0 ? "&w=" + size.Width : "", size.Height > 0 ? "&h=" + size.Height : "");
             }

             return string.Format("{0}blank.gif", ConfigurationManager.AppSettings["Cdn"]);

         }
 */

        //public static string Thumbnail(string image, string thumbSize)
        //{

        //    if (image != null && image.Length > 3)
        //    {

        //        if (!image.StartsWith("//") && !image.StartsWith("http"))
        //        {
        //            image = image.ToAbsoluteUrl(Context.Request.Url);
        //        }

        //        return string.Format("{0}/{1}/?{2}", Configuration.ThumbnailPrefix, thumbSize, Context.Server.UrlEncode(string.Format("{0}{1}", Configuration.ImagePrefix, image)));
        //    }

        //    return string.Format("/blank.gif");

        //}

        //public static string Description(this string html)
        //{

        //    var _indexOf = html.IndexOf("<p><!-- pagebreak --></p>", StringComparison.Ordinal);

        //    if (_indexOf > 0)
        //    {
        //        return html.Substring(0, _indexOf);
        //    }
        //    return html.HtmlToPlainText().Truncate(350);
        //}

        #endregion

        #region Send Mail


        public static void SendMail(string from, string to, string subject, string body)
        {

            var _mail = new MailMessage(new MailAddress(from, ConfigurationManager.AppSettings["domain"]), new MailAddress(to))
            {
                Subject = string.Format("[{0}] - {1}", ConfigurationManager.AppSettings["domain"], subject),
                Body = body + Configuration.EmailSignature,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Priority = MailPriority.High
            };


            //SmtpClient smtpMail = new SmtpClient("smtp.gmail.com");
            var _smtpMail = new SmtpClient(Configuration.SmtpServer, Configuration.SmtpPort)
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = Configuration.SmtpSsl
            };

            if (Configuration.SmtpCredentials)
            {
                _smtpMail.Credentials = new NetworkCredential(Configuration.SmtpUserName, Configuration.SmtpUserPass);
            }

            // and then send the mail
            _smtpMail.Send(_mail);


        }


        #endregion
    }
}
