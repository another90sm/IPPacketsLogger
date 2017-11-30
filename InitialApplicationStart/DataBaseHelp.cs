using DataAccess;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialApplicationStart
{
    public class DataBaseHelp
    {
        private DBManager dbManager;
        public DataBaseHelp()
        {
            dbManager = DBManager.GetInstance(DataBaseInformation.DatabaseType, DataBaseInformation.ConnectionString);
        }

        public bool CheckIfDatabaseExists()
        {
            return dbManager.CheckIfDatabaseExists();
        }

        public bool CreateDatabase()
        {
            return dbManager.CreateDatabase();
        }

        public bool CheckDatabaseStructure()
        {
            return dbManager.CheckDatabaseStructure();
        }
    }
}
