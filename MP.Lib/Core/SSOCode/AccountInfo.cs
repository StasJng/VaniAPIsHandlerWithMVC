using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Lib.Core.SSOCode
{
    public class AccountInfo
    {
        public int account_id{get;set;}

        public string Token { get; set; }

        public int partner_id{get;set;}

        public int acceptance_point_id{get;set;}

        public string account_name{get;set;}

        public string full_name{get;set;}

        public string description{get;set;}

        public int is_admin{get;set;}

        public int is_active{get;set;}

        public DateTime created_on{get;set;}

        public string created_by{get;set;}

        public string password { get; set; }

        public string subcriber_id { get; set; }

        public string company_name { get; set; }

        public string logo { get; set; }
    }
}
