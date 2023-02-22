using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Lib.Core.SSOCode;
using MP.Lib.Helper;
using MP.Lib.Models;

namespace MP.Lib.Repo
{
    public abstract class DataAccessProvider
    {
        public static DataAccessProvider Instance(ITransactionManager trans)
        {
            //if (objProvider != null) return objProvider;
            DataAccessProviderConfiguration objConfig = DataAccessProviderConfiguration.GetDataAccessProviderConfiguration();
            Provider provider = new Provider(objConfig.DefaultProvider, "MP.Lib.Repo." + objConfig.DefaultProvider, trans);
            DataAccessProvider objProvider = (DataAccessProvider)DataAccessProviderFactory.CreateTransactionDataAccess(provider);
            return objProvider;
        }
        public static DataAccessProvider Instance()
        {
            //if (objProvider != null) return objProvider;
            DataAccessProviderConfiguration objConfig = DataAccessProviderConfiguration.GetDataAccessProviderConfiguration();
            Provider provider = new Provider(objConfig.DefaultProvider, "MP.Lib.Repo." + objConfig.DefaultProvider, TransactionManager.Instance());
            DataAccessProvider objProvider = (DataAccessProvider)DataAccessProviderFactory.CreateTransactionDataAccess(provider);
            return objProvider;
        }

        //
        public abstract AccountInfo Authenticate(string username, string pw);
        //
        public abstract User GetPartnerAccountInfo(int accountid);
        //
        public abstract int ChangePassword(int accountid, string pwd);
        //
        public abstract SeoInfo GetSeoInfoByUrl(string url);
    }
}
