using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MP.Lib.LibUtility
{
    public class SessionUtility
    {
        public static void SetValue(string Name, string Value)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session[Name] == null)
            {
                context.Session.Add(Name, Value);
            }
            else
            {
                context.Session[Name] = Value;
            }
        }

        public static string GetValue(string Name)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session[Name] != null)
            {
                return context.Session[Name].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static void Remove(string Name)
        {
            HttpContext context = HttpContext.Current;
            context.Session.Remove(Name);
        }
    }
}
