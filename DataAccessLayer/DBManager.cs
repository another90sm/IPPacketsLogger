using DataAccess.DataAccesses;
using DataAccess.Enums;
using DataAccess.Interfaces;
using System;

namespace DataAccess
{
    //Singleton Design Pattern
    public class DBManager
    {
        private static DBManager _instance = new DBManager();
        public static DBManager GetInstance(DataBaseType dbType, string connectionString)
        {
            _instance._connectionString = connectionString;
            _instance._dataBaseType = dbType;
            _instance.SetDataAccess();
            return _instance;
        }

        private DataBaseType _dataBaseType;
        private IDataAccess _dataAccess;
        private string _connectionString;

        public IDataAccess DataAccess { get { return this._dataAccess; } }
        private DBManager()
        {
            this._dataBaseType = DataBaseType.SQLite;
            this._dataAccess = null;
            this._connectionString = string.Empty;
        }

        private void SetDataAccess()
        {
            switch (this._dataBaseType)
            {
                case DataBaseType.SQLite:
                    this._dataAccess = new DataAccessSQLite(this._connectionString);
                    break;
                case DataBaseType.Oracle:
                    this._dataAccess = new DataAccessOracle(this._connectionString);
                    break;
                case DataBaseType.MySQL:
                    this._dataAccess = new DataAccessMySql(this._connectionString);
                    break;
                case DataBaseType.MSSQL:
                    this._dataAccess = new DataAccessMSSql(this._connectionString);
                    break;
            }
        }
                
        public bool CheckIfDataBaseExists()
        {
            try
            {

            }
            catch (Exception)
            {
                              
            }
            return true;
        }

        public bool CheckDataBaseStructure()
        {
            return true;
        }

        public bool CreateDataBase()
        {
            return true;
        }

        public void InitializeDataBase()
        {

        }
    }
}
