using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Lib.Core.SSOCode;
using MP.Lib.Helper;
using MP.Lib.Models;

namespace MP.Lib.Repo
{
    public class PartnerUserRepo
    {
        public static AccountInfo Authenticate(string username, string pw)
        {
            return DataAccessProvider.Instance().Authenticate(username,pw);
        }
        //
        public static User GetPartnerAccountInfo(int accountid)
        {
            return DataAccessProvider.Instance().GetPartnerAccountInfo(accountid);
        }
        public static int ChangePassword(int accountid, string pwd)
        {
            var trans = TransactionManager.Instance().BeginTransaction();
            int ret = 0;
            try
            {
                //
                ret = DataAccessProvider.Instance(trans).ChangePassword(accountid,pwd);
                if (ret > 0)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }
            }
            catch
            {
                trans.Rollback();
            }
            return ret;
        }
    }
}
