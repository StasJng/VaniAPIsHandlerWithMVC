using MP.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaniAPIsHandler.Models.Location;

namespace VaniAPIsHandler.Services.LocationRepo
{
    public abstract class DataAccessProvider
    {
        public static DataAccessProvider Instance(ITransactionManager trans)
        {
            //if (objProvider != null) return objProvider;
            DataAccessProviderConfiguration objConfig = DataAccessProviderConfiguration.GetDataAccessProviderConfiguration();
            Provider provider = new Provider(objConfig.DefaultProvider, "VaniAPIsHandler.Services.LocationRepo." + objConfig.DefaultProvider, trans);
            DataAccessProvider objProvider = (DataAccessProvider)DataAccessProviderFactory.CreateTransactionDataAccess(provider);
            return objProvider;
        }
        public static DataAccessProvider Instance()
        {
            //if (objProvider != null) return objProvider;
            DataAccessProviderConfiguration objConfig = DataAccessProviderConfiguration.GetDataAccessProviderConfiguration();
            Provider provider = new Provider(objConfig.DefaultProvider, "VaniAPIsHandler.Services.LocationRepo." + objConfig.DefaultProvider, TransactionManager.Instance());
            DataAccessProvider objProvider = (DataAccessProvider)DataAccessProviderFactory.CreateTransactionDataAccess(provider);
            return objProvider;
        }

        public abstract List<Location> getListLocation(int page, int size);
    }
}