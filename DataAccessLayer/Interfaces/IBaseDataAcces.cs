using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IBaseDataAccess
    {
        DataBaseType DBType { get; }
        string ConnectionString { get; }
        void SetConnection(IDbConnection connection);
        IDbConnection GetConnection();
        void OpenConnection();
        void CloseConnection();
        void ClearConnection();
        bool HasConnection { get; }
        void SetTransaction(IDbTransaction transaction);
        IDbTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void ClearTransaction();
        bool HasTransaction { get; }
        IDbCommand GetCommand(string commandText, IDbConnection connection);
        IDbDataAdapter GetaDataAdapter(IDbCommand command);
    }
}
