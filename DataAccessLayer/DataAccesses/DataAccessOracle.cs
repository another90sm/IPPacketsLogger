using System;
using System.Data;
using DataAccess.Enums;
using DataAccess.Interfaces;

namespace DataAccess.DataAccesses
{
    internal class DataAccessOracle : BaseDataAccess, IDataAccess
    {
        internal DataAccessOracle(string connectionString)
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

        public bool IsDatabaseExist()
        {
            throw new NotImplementedException();
        }
    }
}
