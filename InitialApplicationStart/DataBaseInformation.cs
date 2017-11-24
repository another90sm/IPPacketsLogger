using DataAccess.Enums;
using System;
using System.Configuration;

namespace InitialApplicationStart
{
    public class DataBaseInformation
    {
        private static bool _workWithDataBase;
        private static string _connectionString;
        private static DataBaseType _dataBaseType;
        public static bool WorkWithDataBase { get { return _workWithDataBase; } }
        public static string ConnectionString { get { return _connectionString; } }

        public static DataBaseType DatabaseType { get { return _dataBaseType; } }

        static DataBaseInformation()
        {
            ReadAppConfig();
        }

        private static void ReadAppConfig()
        {
            string providerName;
            string connectionString;
            string workWithDatabase;
            try
            {
                providerName = ConfigurationManager.ConnectionStrings["DBConnectionString"].ProviderName.Trim().ToLower();
                connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                workWithDatabase = ConfigurationManager.AppSettings["WorkWithDataBase"].Trim().ToLower();
            }
            catch (NullReferenceException ex)
            {
                throw new ArgumentNullException("One or more attributes was not set to the configuration file!");
            }

            switch (providerName)
            {
                case "sqlite":
                    _dataBaseType = DataBaseType.SQLite;
                    break;
                case "mysql":
                    _dataBaseType = DataBaseType.MySQL;
                    break;
                case "mssql":
                    _dataBaseType = DataBaseType.MSSQL;
                    break;
                case "oracle":
                    _dataBaseType = DataBaseType.Oracle;
                    break;
                default:
                    throw new ArgumentException("Data base type is not valid!");
            }

            _connectionString = connectionString;

            switch (workWithDatabase)
            {
                case "true":
                    _workWithDataBase = true;
                    break;
                case "false":
                    _workWithDataBase = false;
                    break;
                default:
                    throw new ArgumentException("Data base type is not valid!");
            }
        }
    }
}
