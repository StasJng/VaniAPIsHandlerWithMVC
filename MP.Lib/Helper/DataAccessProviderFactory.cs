using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Compilation;
using MP.Lib.LibUtility;

namespace MP.Lib.Helper
{
    public class DataAccessProviderFactory
    {
        
        public static object CreateDataAccess(Provider provider)
        {
            Type type = BuildManager.GetType(provider.ProviderType, true);
            object obj = Activator.CreateInstance(type, AppEnv.GetConnectionString(provider.ConnectionString));
            return obj;
        }
        public static object CreateTransactionDataAccess(Provider provider)
        {
            Type type = BuildManager.GetType(provider.ProviderType, true);
            object obj = Activator.CreateInstance(type, provider.Session);
            return obj;
        }

        //public static IDictionary<String, Object> controllers = new Dictionary<String, Object>();
        public static T CreateDataController<T>()
        {
            object controller;
            //created by thanhnv create interface instace
            string stype = typeof(T).ToString();
            string ClassName = stype.Substring(stype.LastIndexOf("."));
            stype = stype.Substring(0, stype.LastIndexOf("."));
            stype = stype.Replace(".Interface.", ".Library.");
            ClassName = ClassName.Replace(".I", ".");
            stype = stype + ClassName; // build controller type
            //if (!controllers.ContainsKey(stype))
            //{
            Type type = BuildManager.GetType(stype, true);
            controller = Activator.CreateInstance(type);
            //    controllers.Add(stype, controller);
            //}
            //else
            //{
            //    controller = controllers[stype] ;
            //}
            return (T)controller;
        }
    }
}
