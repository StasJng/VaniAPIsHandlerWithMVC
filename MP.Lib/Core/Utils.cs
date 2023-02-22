using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json.Linq;

namespace MP.Lib.Core
{
    public static class Utils
    {
        public static string AppPath
        {
            get { return HttpRuntime.AppDomainAppPath; }
        }

        public static string ReadHtml(string fileName)
        {
            return FileHelper.ReadText(string.Format("{0}html/{1}.htm", AppPath, fileName));
        }

        public static int ToMonthInt(this DateTime d)
        {
            return int.Parse(string.Format("{0}{1:00}", d.Year, d.Month));
        }

        public static int ToDayInt(this DateTime d)
        {
            return int.Parse(string.Format("{0}{1:00}{2:00}", d.Year, d.Month, d.Day));
        }

        public static int ToHourInt(this DateTime d)
        {
            return int.Parse(string.Format("{0}{1:00}{2:00}{3:00}", d.Year, d.Month, d.Day, d.Hour));
        }

        public static DateTime ShortDayToString(this string d)
        {
            return DateTime.Parse(string.Format("{0}/{1}/{2} 00:00", d.Substring(4, 2), d.Substring(6, 2), d.Substring(0, 4)));
        }

        public static DateTime ShortHourToString(this string d)
        {
            return DateTime.Parse(string.Format("{0}/{1}/{2} {3}:00", d.Substring(4, 2), d.Substring(6, 2), d.Substring(0, 4), d.Substring(8, 2)));
        }

        public static DateTime ToUnixDateTime(this double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }



        public static long ToJavascriptTimestamp(this DateTime date)
        {
            var _span = new TimeSpan(new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
            DateTime _time = date.Subtract(_span);
            return _time.Ticks / 10000;

        }
        public static double ToUnixTimestamp(this DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static string ToKb(this long num)
        {
            string[] _suffixes = { "B", "K", "M", "G", "T", "P", "E", "Z", "Y" };
            int i = 0;
            decimal _val = num;
            while (Math.Round(_val / 1000) >= 1)
            {
                _val /= 1000;
                i++;
            }

            return string.Format("{0:n1} {1}", _val, _suffixes[i]);

        }

        /*

                public static string MobileModel(string agent)
                {
                    //http://www.zytrax.com/tech/web/mobile_ids.html
                    //http://www.useragentstring.com/pages/Mobile%20Browserlist/
                    //https://gist.github.com/dalethedeveloper/1503252
                }
        */


        public static bool IsMobile(this string agent)
        {
            var _regex =
                new Regex(
                    @"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino",
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return _regex.IsMatch(agent);
        }

        public static string HtmlClean(this string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return html;

            html = Regex.Replace(html, @"<\?xml.*?>", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
            html = Regex.Replace(html, @"<(o:p|\/o:p)>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, @"\s*mso-[^:]+:[^;\x22]+;?", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            html = Regex.Replace(html, @"<\\?\?xml[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, @"<\/?\w+:[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //html = Regex.Replace(html, @"<!(--)?[^>]*>", "", RegexOptions.IgnoreCase|RegexOptions.Compiled);
            html = Regex.Replace(html, @"x:\w{3}(=\x22[\s\w]*\x22)", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, @"\s*tab-stops:[^;""]*;?", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            /* html = html.Replace("<o:p>", "");
             html = html.Replace("</o:p>", "");
             html=Regex.Replace(html, @"\:(?=[^<>]*>)", "",RegexOptions.Compiled);*/

            //html = Regex.Replace(html, @"<([^>]*)(?:font|st1|shape|path|lock|imagedata|stroke|formulas|span|xml|del|ins|[ovwxp][o]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase | RegexOptions.Compiled);


            //html = Regex.Replace(html, @"<\?xml.*?>", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
            // start by completely removing all unwanted tags 
            // html = Regex.Replace(html, @"<[/]?(font|span|xml|del|ins|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase|RegexOptions.Compiled);
            // then run another pass over the html (twice), removing unwanted attributes 
            // html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<([^>]*)(?:font|st1|shape|path|lock|imagedata|stroke|formulas|span|xml|del|ins|[ovwxp][o]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);

            // html = Regex.Replace(html, @"(?></?\w+)(?>(?:[^>'""]+|'[^']*'|""[^""]*"")*)>",string.Empty, RegexOptions.Compiled);

            return html;
        }

        public static List<DateTime> DaysOfDateRange(DateTime startingDate, DateTime endingDate)
        {
            if (startingDate > endingDate)
            {
                return null;
            }
            var rv = new List<DateTime>();
            DateTime tmpDate = startingDate;
            do
            {
                rv.Add(tmpDate);
                tmpDate = tmpDate.AddDays(1);
            } while (tmpDate <= endingDate);
            return rv;
        }

        #region RandomPassword


        public static string NewId()
        {
            return NewId(11);
        }

        public static string NewId(int length)
        {
            char[] _chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var _data = new byte[length];
            var _crypto = new RNGCryptoServiceProvider();
            _crypto.GetNonZeroBytes(_data);
            var _result = new StringBuilder(length);
            foreach (byte _b in _data)
            {
                _result.Append(_chars[_b % (_chars.Length - 1)]);
            }
            return _result.ToString();
        }

        public static string RandomNumber(int length)
        {
            return Random(length, true);
        }

        public static string Random(int length, bool onlyNumber)
        {
            string PASSWORD_CHARS = onlyNumber ? "0123456789" : "123456789ABCDEFGHJKLMNPQRSTWXYZ";
            var s = new StringBuilder();
            char[] chars = PASSWORD_CHARS.ToCharArray();
            var _rd = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = _rd.Next(chars.Length - 1);
                s.Append(chars[index].ToString());
            }
            return s.ToString();

        }

        #endregion


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
            //   _sb.AppendFormat(",{0}", SessionID);

            return Encryptor.Encrypt(_sb.ToString());

        }


        public static string[] Decrypt(string _encryptString)
        {

            string _plainText = Encryptor.Decrypt(_encryptString);
            return _plainText.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        }




        #endregion

        public static byte[] Download(string url)
        {
            byte[] _data;
            try
            {
                //Get a data stream from the url
                var _request = (HttpWebRequest)WebRequest.Create(url);
                _request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";
                _request.Referer = url;
                _request.AllowWriteStreamBuffering = true;
                var _response = _request.GetResponse();
                Stream _stream = _response.GetResponseStream();
                //Download in chuncks
                var _buffer = new byte[2048];

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                using (var _memStream = new MemoryStream())
                {

                    while (true)
                    {
                        //Try to read the data
                        int _bytesRead = _stream.Read(_buffer, 0, _buffer.Length);

                        if (_bytesRead == 0)
                        {
                            break;
                        }
                        //Write the downloaded data
                        _memStream.Write(_buffer, 0, _bytesRead);
                    }

                    //Convert the downloaded stream to a byte array
                    _data = _memStream.ToArray();
                    _memStream.Close();

                }


                //Clean up
                _stream.Close();

            }
            catch (Exception)
            {
                return new byte[0];
            }

            return _data;
        }

        public static string DownloadPageAsString(string url)
        {

            HttpWebRequest request;
            HttpWebResponse response = null;
            StreamReader reader;
            StringBuilder sbSource;

            try
            {
                // Create and initialize the web request  
                request = WebRequest.Create(url) as HttpWebRequest;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:14.0) Gecko/20100101 Firefox/14.0.1";
                request.KeepAlive = false;
                request.Timeout = 10 * 1000;

                // Get response  
                response = request.GetResponse() as HttpWebResponse;

                if (request.HaveResponse && response != null)
                {
                    // Get the response stream  
                    reader = new StreamReader(response.GetResponseStream());

                    // Read it into a StringBuilder  
                    sbSource = new StringBuilder(reader.ReadToEnd());

                    response.Close();

                    // Console application output  
                    return sbSource.ToString();
                }
                return "";
            }
            catch (Exception)
            {
                response.Close();
                return "";
            }
        }

        /* public static string GetMeta(string html, string metaName)
         {
             // --- Parse the meta keywords
             Match KeywordMatch = Regex.Match(html, "<meta name=\"" + metaName + "\" content=\"([^<]*)\">", RegexOptions.IgnoreCase | RegexOptions.Multiline);
             return KeywordMatch.Groups[1].Value;

         }*/

        public static string GetMetaContent(this string html, string metaName)
        {

            return Regex.Match(html, "(?<=" + metaName + "\" content=\")(?<val>.*?)(?=\")").Groups["val"].Value;

        }


        public static int[] ToInt(this string[] s)
        {
            var ints = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                ints[i] = int.Parse(s[i]);
            }

            return ints;
        }

        public static string[] Split(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

            }
            return null;

        }

        public static string ClientIp
        {
            get
            {
                var _ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.UserHostAddress;
                if (_ip == "::1" || string.IsNullOrEmpty(_ip)) _ip = "127.0.0.1";
                return _ip;
            }
        }


        public static int Levenshtein(string source, string target)
        {
            int n = source.Length;
            int m = target.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }



        public static double LevenshteinPercent(string source, string target)
        {

            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
                return 0;

            var _max = Math.Max(source.Length, target.Length);

            int distance = Levenshtein(source, target);

            double percent = (double)distance / _max;

            return (1 - percent) * 100;

        }

        public static string Truncate(object _input, int _length)
        {
            if (_input == null)
            {
                return null;
            }
            string str = _input.ToString();
            if (str.Length <= _length)
            {
                return str;
            }
            string[] strArray = str.Replace("&nbsp;", " ").Split(new char[] { ' ' });
            StringBuilder builder = new StringBuilder(0x3e8);
            foreach (string str2 in strArray)
            {
                if (str2.Length > _length)
                {
                    builder.Append(str2.Substring(0, _length));
                }
                else if ((builder + str2).Length < _length)
                {
                    builder.Append(" " + str2);
                }
            }
            return (builder + "...");
        }

        public static string SeoSpin(string input, string location = "", string district = "", string cate = "", string partner = "", string keyword = "", string minPrice = "", string maxPrice = "", string total = "")
        {
            if (!string.IsNullOrEmpty(input))
            {
                var random = new Random();
                var result = string.Empty;
                //input = input.Replace("][", ",");
                int c = Regex.Matches(input, @"Value").Count;
                JArray v = JArray.Parse(input);
                List<int> randomValue = new List<int>();//radom spin
                for (int i = c-1; i < c; i++)
                {
                    int rvalue;
                    while (true)
                    {
                        rvalue = random.Next(0, c);
                        if (!randomValue.Contains(rvalue))
                        {
                            randomValue.Add(rvalue);
                            break;
                        }
                    }
                    result += v[rvalue]["Value"].ToString();
                }
                return ReplaceParameters(result, location, district, cate, partner, keyword, minPrice, maxPrice, total);
            }
            else
            {
                return null;
            }
        }

        public static string ReplaceParameters(string input = "", string location = "", string district = "", string cate = "", string partner = "", string keyword = "", string minPrice = "", string maxPrice = "", string total = "")
        {
            var result = string.Empty;
            try
            {
                result =
                    input.Replace("#dtd-tinhthanh#", location)
                         .Replace("#dtd-quanhuyen#", district)
                         .Replace("#dtd-nganhhang#", cate)
                         .Replace("#dtd-thuonghieu#", partner)
                         .Replace("#dtd-tukhoa#", keyword)
                         .Replace("#dtd-giathapnhat#", minPrice)
                         .Replace("#dtd-giacaonhat#", maxPrice)
                         .Replace("#dtd-soluong#", total);
            }
            catch
            {
                return result;
            }
            return result;
        }

        public static string GetBetween(string strSource, string strStart, string strEnd)
        {

            int Start, End;
            if (!string.IsNullOrEmpty(strSource) && strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public static string RemoveBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (!string.IsNullOrEmpty(strSource) && strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Remove(Start, End - Start).Replace("<h2></h2>", "");
            }
            else if (!string.IsNullOrEmpty(strSource))
            {
                return strSource;
            }
            else
            {
                return "";
            }
        }
    }
}
