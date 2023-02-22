using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MP.Lib.LibUtility
{
    public class ConvertUtility
    {
        public static string FormatTimeVn(DateTime dt, string defaultText)
        {
            if (ToDateTime(dt) != new DateTime(1900, 1, 1))
                return dt.ToString("dd-mm-yy");
            else
                return defaultText;
        }
        public static double ToDouble1(string obj)
        {
            double retVal;
            try
            {
                obj = obj.Replace(",", "").Replace(".", ",").Replace(" ", "");

                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }
        public static int ToInt32(object obj)
        {
            int retVal = 0;

            try
            {
                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static int ToInt32(object obj, int defaultValue)
        {
            int retVal = defaultValue;

            try
            {
                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static string ToString(object obj)
        {
            string retVal;

            try
            {
                retVal = Convert.ToString(obj);
            }
            catch
            {
                retVal = String.Empty;
            }

            return retVal;
        }

        public static DateTime ToDateTime(object obj)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.Now;
            }
            if (retVal == new DateTime(1, 1, 1)) return DateTime.Now;

            return retVal;
        }

        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.Now;
            }
            if (retVal == new DateTime(1, 1, 1)) return defaultValue;

            return retVal;
        }

        public static bool ToBoolean(object obj)
        {
            bool retVal;

            try
            {
                retVal = Convert.ToBoolean(obj);
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }

        public static double ToDouble(object obj)
        {
            double retVal;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static double ToDouble(object obj, double defaultValue)
        {
            double retVal;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static String FormatValue(long number)
        {
            return number.ToString("#,##0").Replace(',','.');
        }

        public static DateTime ParseReportDate(String dateText)
        {
            try
            {
                return DateTime.ParseExact(dateText, "dd/MM/yyyy", null);
            }
            catch (Exception ex)
            {
                return DateTime.Today;
            }
        }
        public static DateTime ToDateTimeFrom(object obj)
        {
            DateTime date = new DateTime(2001, 01, 01);
            try
            {
                date = DateTime.Parse(obj.ToString().Trim(), new CultureInfo("vi-VN"));
                return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0); ;

            }
            catch (Exception)
            {
                return date;
            }
        }
        public static DateTime ToDateTimeTo(object obj)
        {
            DateTime date = new DateTime(2001, 01, 01);
            try
            {
                date = DateTime.Parse(obj.ToString().Trim(), new CultureInfo("vi-VN"));
                return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

            }
            catch (Exception)
            {
                return date;
            }
        }

        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("{0} {1} trước",
                years, "Năm");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("{0} {1} trước",
                months, "Tháng");
            }
            if (span.Days > 0)
                return String.Format("{0} {1} trước",
                span.Days, "Ngày");
            if (span.Hours > 0)
                return String.Format("{0} {1} trước",
                span.Hours, "Giờ");
            if (span.Minutes > 0)
                return String.Format(" {0} {1} trước",
                span.Minutes, "Phút");
            if (span.Seconds > 5)
                return String.Format(" {0} Giây trước", span.Seconds);
            if (span.Seconds <= 5)
                return "Vừa xong";
            return string.Empty;
        }

        public static string VNDatetime(DateTime dt)
        {
            string strFormat = "{0}, {1} tháng {2} {3}";
            string dayofweek = string.Empty;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Thursday:
                    dayofweek = "Thứ năm";
                    break;
                case DayOfWeek.Friday:
                    dayofweek = "Thứ sáu";
                    break;
                case DayOfWeek.Monday:
                    dayofweek = "Thứ hai";
                    break;
                case DayOfWeek.Tuesday:
                    dayofweek = "Thứ ba";
                    break;
                case DayOfWeek.Wednesday:
                    dayofweek = "Thứ tư";
                    break;
                case DayOfWeek.Saturday:
                    dayofweek = "Thứ bảy";
                    break;
                case DayOfWeek.Sunday:
                    dayofweek = "Chủ nhật";
                    break;

            }
            string ret = string.Format(strFormat, dayofweek, dt.Day, dt.Month, dt.Year);
            return ret;
        }
    }
}
