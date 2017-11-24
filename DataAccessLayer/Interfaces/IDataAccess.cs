using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IDataAccess
    {
        bool CreateDatabase();
        IDataReader GetDBTablesStructure();
    }
}
