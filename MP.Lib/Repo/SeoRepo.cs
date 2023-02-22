using MP.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Lib.Repo
{
    class SeoRepo
    {
        public SeoRepo Instance
        {
            get
            {
                return new SeoRepo();
            }
        }
        public SeoInfo GetSeoInfoByUrl(string url)
        {
            return DataAccessProvider.Instance().GetSeoInfoByUrl(url);

        }
    }
}

