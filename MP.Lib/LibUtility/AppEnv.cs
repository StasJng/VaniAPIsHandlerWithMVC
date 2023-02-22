using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml;

namespace MP.Lib.LibUtility
{
    public class AppEnv
    {
        public static string ConnectionString
        {
            get { return WebConfigurationManager.ConnectionStrings["localsql"].ConnectionString; }
        }
        public static string ConnectionStringMysql
        {
            get { return WebConfigurationManager.ConnectionStrings["localmysql"].ConnectionString; }
        }
        public static string ADMIN_EMAIL = "admin@vmgmedia.vn";
        public static string ContactMail
        {
            get { return GetSetting("ContactMail"); }
        }
        public static string GetConnectionString(string name)
        {
            return WebConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public static string GetSetting(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }
        public static double GetTimeCacheExpire()
        {
            return ConvertUtility.ToInt32(GetSetting("CacheExpire"));
        }
        public static string GetAppSetting(string key, string configFileName)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(HttpContext.Current.Server.MapPath(configFileName));
            XmlNode node = xd.DocumentElement.SelectSingleNode("/configuration/appSettings/add[@key=\"" + key + "\"]");
            if (node != null) return node.Attributes.GetNamedItem("value").Value;
            else return null;
        }


        public static string GetDefaultLanguage()
        {
            string strLanguage = "";
            if (WebConfigurationManager.AppSettings["Default_Language"] != null)
                strLanguage = WebConfigurationManager.AppSettings["Default_Language"];
            else
                strLanguage = "vi-VN";
            return strLanguage;
        }

        public static string AdminUrlParams(string cmd)
        {
            return ADMIN_CMD + "?portalid=" + HttpContext.Current.Request["portalid"] + "&cmd=" + cmd;
        }

        public static string IframeUrlParams(string cmd)
        {
            return FRAME_PAGE + "?portalid=" + HttpContext.Current.Request["portalid"] + "&cmd=" + cmd;
        }



        public static void LogFile(string message)
        {
            FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath("/Logservice.txt"));
            if (!f.Exists)
            {
                f.Create();
                TextWriter tw = f.AppendText();

                tw.WriteLine(message);
                tw.Flush();
                tw.Close();
            }
            else
            {
                TextWriter tw = f.AppendText(); ; // new StreamWriter(HttpContext.Current.Server.MapPath("/Logservice.txt"));

                tw.WriteLine(message);
                tw.Flush();
                tw.Close();
            }

        }

        public const string DEFAULT_PAGE = "/Default.aspx";
        public const string DEFAULT_EXTENTSION = ".html";
        public const string FRAME_PAGE = "/Frame.aspx";
        public const string ADMIN_CMD = "/Default.aspx";
        public const string ACCESSDENY = "/Default.aspx?cmd=accessdeny";

        private const string CONFIG_FILENAME = "/Module_Control/Customer/Customer.config";
        private const string GAMECONFIG_FILENAME = "/Module_Control/Sport_Game/game.config";
        public static string Server_Mail
        {
            get { return ConvertUtility.ToString(AppEnv.GetAppSetting("ServerMail", CONFIG_FILENAME)); }
        }

        public static string Username_Mail
        {
            get { return ConvertUtility.ToString(AppEnv.GetAppSetting("Username_Mail", CONFIG_FILENAME)); }
        }
        public static string Password
        {
            get { return ConvertUtility.ToString(AppEnv.GetAppSetting("Password", CONFIG_FILENAME)); }
        }


        public static string expand
        {
            get { return ConvertUtility.ToString(AppEnv.GetAppSetting("expand", GAMECONFIG_FILENAME)); }
        }

        public static DataTable GetDataFromXML(string path)
        {
            DataSet data = new DataSet();

            data.ReadXml(HttpContext.Current.Server.MapPath(path));
            DataTable dtRoot = data.Tables[0];
            return dtRoot;
        }

        public static int Customer_ID()
        {
            return ConvertUtility.ToInt32(HttpContext.Current.Session["MyPay_CustomerID_OK"]);
        }
     
        public static string RandomPsw(int Size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
