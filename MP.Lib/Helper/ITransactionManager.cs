using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MP.Lib.Helper
{
    public interface ITransactionManager
    {
        ITransactionManager BeginTransaction();
        void Commit();
        void Dispose();
        void Finished();
        void Rollback();

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        string ConnectionName { get; }
        string ConnectionString { get; }
        bool IsTransactionInstance { get; }
    }
}
