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
            return dbManager.CheckIfDataBaseExists();
        }

        public bool CreateDatabase()
        {
            return dbManager.CreateDataBase();
        }

        public bool CheckDatabaseStructure()
        {
            return dbManager.CheckDataBaseStructure();
        }
    }
}
