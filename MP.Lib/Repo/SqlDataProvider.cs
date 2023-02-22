using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MP.Lib.Core.SSOCode;
using MP.Lib.Helper;
using MP.Lib.LibUtility;
using MP.Lib.Models;

namespace MP.Lib.Repo
{
    public class SqlDataProvider : DataAccessProvider
    {
        public ITransactionManager trans { get; set; }
        public SqlDataProvider(ITransactionManager _trans)
        {
            this.trans = _trans;
        }

        public override AccountInfo Authenticate(string username, string pw)
        {
            try
            {
                IDataReader dr = OracleTransactionFilter.ExecuteReader(this.trans, "PKG_MP_PARTNERACCOUNT.Authenticate", username, pw);
                AccountInfo command = (AccountInfo)ObjectHelper.CreateObject(typeof(AccountInfo), dr);
                command.Token = Utility.GetGuidHash();
                return command;
            }
            catch
            {
                return null;
            }
        }
        //
        public override User GetPartnerAccountInfo(int accountid)
        {
            try
            {
                IDataReader dr = OracleTransactionFilter.ExecuteReader(this.trans, "PKG_MP_PARTNERACCOUNT.GetAccountById", accountid);
                AccountInfo command = (AccountInfo)ObjectHelper.CreateObject(typeof(AccountInfo), dr);
                User _user = new User
                {
                    account_id = command.account_id,
                    partner_id = command.partner_id,
                    acceptance_point_id = command.acceptance_point_id,
                    account_name = command.account_name,
                    full_name = command.full_name,
                    description = command.description,
                    is_admin = command.is_admin,
                    is_active = command.is_active,
                    created_on = command.created_on,
                    created_by = command.created_by,
                    password = string.Empty
                };
                return _user;
            }
            catch
            {
                return null;
            }
        }
        public override int ChangePassword(int accountid, string pwd)
        {
            object obj = OracleTransactionFilter.ExecuteScalar(trans, "PKG_MP_PARTNERACCOUNT.ChangePwd", accountid, pwd);
            return ConvertUtility.ToInt32(obj);
        }
        //
        public override SeoInfo GetSeoInfoByUrl(string url)
        {
            DataSet ds = OracleTransactionFilter.ExecuteDataset(trans, "", url);
            if (ds != null && ds.Tables.Count > 0)
                return ObjectHelper.FillTable<SeoInfo>(ds.Tables[0]).FirstOrDefault();
            else
                return null;
        }
    }
}
