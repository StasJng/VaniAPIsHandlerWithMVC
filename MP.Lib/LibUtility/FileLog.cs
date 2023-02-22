using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MP.Lib.LibUtility
{
    public class FileLog
    {
        static int Delay = 0;
        public static bool isReady = true;
        public static void LogNormal(string Module, string p)
        {
            isReady = false;
            string strFile;
            strFile = AppEnv.GetSetting("LogFolder") + "/" + Module + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt";
            strFile = HttpContext.Current.Server.MapPath(strFile);
            string strDir;
            if (!File.Exists(strFile))
            {
                try
                {
                    strDir = strFile.Substring(0, strFile.LastIndexOf("\\"));
                    if (!Directory.Exists(strDir)) Directory.CreateDirectory(strDir);
                    File.WriteAllText(string.Format(strFile), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + p + "\n", Encoding.UTF8);
                    LogNormal(Module, "");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(strFile))
                    {
                        try
                        {

                            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + p);

                        }
                        finally
                        {
                            if (sw != null) sw.Close();
                            if (sw != null) sw.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            isReady = true;

        }
        public static void AddOrderLog(string orderID, string p)
        {
            isReady = false;
            string strFile = string.Format(AppEnv.GetSetting("logOrder"), DateTime.Now.ToString("yyyy-MM-dd") + "-" + orderID);
            strFile = HttpContext.Current.Server.MapPath(strFile);
            string strDir;
            if (!File.Exists(strFile))
            {
                try
                {
                    strDir = strFile.Substring(0, strFile.LastIndexOf("\\"));
                    if (!Directory.Exists(strDir)) Directory.CreateDirectory(strDir);
                    File.WriteAllText(string.Format(strFile), p + "\n", Encoding.UTF8);
                    AddOrderLog(orderID, "");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(strFile))
                    {
                        try
                        {

                            sw.WriteLine(p);

                        }
                        finally
                        {
                            if (sw != null) sw.Close();
                            if (sw != null) sw.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            isReady = true;

        }
        public static void AddOrderLogSMS(string orderID, string mobile, string status, string p)
        {
            isReady = false;
            string strFile = string.Format(AppEnv.GetSetting("logOrder"), DateTime.Now.ToString("yyyy-MM-dd") + "-" + orderID + "-mobile" + mobile + "-" + status);
            strFile = HttpContext.Current.Server.MapPath(strFile);
            string strDir;
            if (!File.Exists(strFile))
            {
                try
                {
                    strDir = strFile.Substring(0, strFile.LastIndexOf("\\"));
                    if (!Directory.Exists(strDir)) Directory.CreateDirectory(strDir);
                    File.WriteAllText(string.Format(strFile), p + "\n", Encoding.UTF8);
                    AddOrderLogSMS(orderID, mobile, status, "");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(strFile))
                    {
                        try
                        {

                            sw.WriteLine(p);

                        }
                        finally
                        {
                            if (sw != null) sw.Close();
                            if (sw != null) sw.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            isReady = true;

        }
        public static void AddSyncLog(string RequestID, string p)
        {
            isReady = false;
            string strFile = string.Format(AppEnv.GetSetting("translog"), DateTime.Now.ToString("yyyy-MM-dd") + "-" + RequestID);
            strFile = HttpContext.Current.Server.MapPath(strFile);
            string strDir;
            if (!File.Exists(strFile))
            {
                try
                {
                    strDir = strFile.Substring(0, strFile.LastIndexOf("\\"));
                    if (!Directory.Exists(strDir)) Directory.CreateDirectory(strDir);
                    File.WriteAllText(string.Format(strFile), p + "\n", Encoding.UTF8);
                    AddSyncLog(RequestID, "");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(strFile))
                    {
                        try
                        {

                            sw.WriteLine(p);

                        }
                        finally
                        {
                            if (sw != null) sw.Close();
                            if (sw != null) sw.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            isReady = true;

        }
        public static void AddpendDaily(string p)
        {
            isReady = false;
            string strFile = string.Format(AppEnv.GetSetting("logFile"), DateTime.Now.ToString("yyyy-MM-dd"));
            strFile = HttpContext.Current.Server.MapPath(strFile);
            string strDir;
            if (!File.Exists(strFile))
            {
                try
                {
                    strDir = strFile.Substring(0, strFile.LastIndexOf("\\"));
                    if (!Directory.Exists(strDir)) Directory.CreateDirectory(strDir);
                    File.WriteAllText(string.Format(strFile), p + "\n", Encoding.UTF8);
                    AddpendDaily("");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(strFile))
                    {
                        try
                        {

                            sw.WriteLine(p);

                        }
                        finally
                        {
                            if (sw != null) sw.Close();
                            if (sw != null) sw.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            isReady = true;

        }
        public static void Addpend(string p)
        {
            isReady = false;
            string strFile = string.Format(AppEnv.GetSetting("logFile"), "");
            strFile = HttpContext.Current.Server.MapPath(strFile);
            string strDir;
            if (!File.Exists(strFile))
            {
                try
                {
                    strDir = strFile.Substring(0, strFile.LastIndexOf("\\"));
                    if (!Directory.Exists(strDir)) Directory.CreateDirectory(strDir);
                    File.WriteAllText(strFile, p + "\n", Encoding.UTF8);
                    Addpend("");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(strFile))
                    {
                        try
                        {

                            sw.WriteLine(p);

                        }
                        finally
                        {
                            if (sw != null) sw.Close();
                            if (sw != null) sw.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            isReady = true;

        }
        static bool isFileOpenOrReadOnly(string file)
        {
            try
            {
                //first make sure it's not a read only file
                if ((File.GetAttributes(file) & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                {
                    //first we open the file with a FileStream
                    using (FileStream stream = new FileStream(file, FileMode.Append))
                    {
                        try
                        {
                            stream.ReadByte();
                            return false;
                        }
                        catch (IOException)
                        {
                            return true;
                        }
                        finally
                        {
                            stream.Close();
                            stream.Dispose();
                        }
                    }
                }
                else
                    return true;
            }
            catch (IOException)
            {
                return true;
            }
        }
    }
    public class FileMng
    {
        public static void WriteTextFile(string FileName, string p)
        {
            string strFile = "/JsonTemplate/" + FileName + ".txt";
            strFile = HttpContext.Current.Server.MapPath(strFile);
            string strDir;
            try
            {
                strDir = strFile.Substring(0, strFile.LastIndexOf("\\"));
                if (!Directory.Exists(strDir)) Directory.CreateDirectory(strDir);
                File.WriteAllText(string.Format(strFile), p + "\n", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public static string ReadTextFile(string FileName)
        {
            string strFile = "/JsonTemplate/" + FileName + ".txt";
            strFile = HttpContext.Current.Server.MapPath(strFile);
            string ret = "";
            try
            {
                ret = File.ReadAllText(strFile, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                //throw;
            }
            return ret;
        }
        public static string ReadFile(string FileName)
        {
            //string strFile = "/JsonTemplate/" + FileName + ".txt";
            string strFile = HttpContext.Current.Server.MapPath(FileName);
            string ret = "";
            try
            {
                ret = HttpContext.Current.Cache.Get(FileName) as string;
                if (ret == null)
                {
                    ret = File.ReadAllText(strFile, Encoding.UTF8);
                    HttpContext.Current.Cache.Insert(FileName, ret, new System.Web.Caching.CacheDependency(strFile));

                }
            }
            catch (Exception ex)
            {
                //throw;
            }
            return ret;
        }
        public static DataSet RedXml(string fileName)
        {
            if (fileName.StartsWith("/"))
            {
                fileName = HttpContext.Current.Server.MapPath(fileName);
            }
            DataSet ds = new DataSet();
            ds.ReadXml(fileName);
            return ds;
        }
    }
}
