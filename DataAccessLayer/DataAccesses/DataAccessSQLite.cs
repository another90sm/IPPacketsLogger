using System;
using System.Data;
using DataAccess.Enums;
using DataAccess.Interfaces;

namespace DataAccess.DataAccesses
{
    internal class DataAccessSQLite : BaseDataAccess, IDataAccess
    {
        internal DataAccessSQLite(string connectionString)
            : base(DataBaseType.SQLite, connectionString)
        {

        }

        public bool CreateDatabase()
        {
            throw new NotImplementedException();
        }

        public IDataReader GetDBTablesStructure()
        {
            throw new NotImplementedException();
        }
    }
}
