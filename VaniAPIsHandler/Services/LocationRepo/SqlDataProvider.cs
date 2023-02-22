using MP.Lib.Helper;
using MP.Lib.LibUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VaniAPIsHandler.Models.Location;

namespace VaniAPIsHandler.Services.LocationRepo
{
    public class SqlDataProvider : DataAccessProvider
    {
        public ITransactionManager trans { get; set; }
        public SqlDataProvider(ITransactionManager _trans)
        {
            this.trans = _trans;
        }
        public override List<Location> getListLocation(int page, int size)
        {
            List<Location> lstLocs = new List<Location>();
            try
            {
                DataSet ds = OracleTransactionFilter.ExecuteDataset(trans, "PKG_VANI_APIS.getListLocation", page, size);
                if (ds != null && ds.Tables.Count > 0)
                {
                    lstLocs = ObjectHelper.FillTable<Location>(ds.Tables[0]);
                }
            }
            catch (Exception)
            {
                lstLocs = null;
            }
            return lstLocs;
        }
    }
}