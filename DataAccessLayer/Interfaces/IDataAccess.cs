using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IDataAccess : IBaseDataAccess
    {
        bool IsDatabaseExist();
        bool CreateDatabase();
        IDataReader GetDBTablesStructure();
        void CreateTable(string tableName, object[,] columnsParameter);
    }
}
