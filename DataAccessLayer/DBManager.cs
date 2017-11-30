using DataAccess.DataAccesses;
using DataAccess.DataBaseObjects;
using DataAccess.Enums;
using DataAccess.Interfaces;
using DataAccessLayer.DataBaseObjects;
using System;
using System.Data;

namespace DataAccess
{
    //Singleton Design Pattern
    public class DBManager
    {
        private static DBManager _instance = new DBManager();
        public static DBManager GetInstance(DataBaseType dbType, string connectionString)
        {
            _instance._connectionString = connectionString;
            _instance._databaseType = dbType;
            _instance.SetDataAccess();
            return _instance;
        }

        private DataBaseType _databaseType;
        private IDataAccess _dataAccess;
        private string _connectionString;

        public IDataAccess DataAccess { get { return this._dataAccess; } }
        private DBManager()
        {
            this._databaseType = DataBaseType.SQLite;
            this._dataAccess = null;
            this._connectionString = string.Empty;
        }
        private void SetDataAccess()
        {
            switch (this._databaseType)
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
        public bool CheckIfDatabaseExists()
        {
            try
            {
                return this._dataAccess.IsDatabaseExist();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckDatabaseStructure()
        {
            return true;
        }
        public bool CreateDatabase()
        {
            try
            {
                return this._dataAccess.CreateDatabase();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CreateDatabaseObjects()
        {
            try
            {
                var description = DBDescription.DeserializeFromXML(this._databaseType);
                this._dataAccess.BeginTransaction();
                foreach (var table in description.Tables)
                {
                    var tableName = table.Name;
                    object[,] columnsParameter = new object[table.Columns.Count, 6];

                    foreach (var column in table.Columns)
                    {

                    }


                    this._dataAccess.CreateTable(tableName, columnsParameter);
                }
                this._dataAccess.CommitTransaction();
            }
            catch (Exception ex)
            {
                this._dataAccess.RollbackTransaction();
                throw ex;
            }           
        }       
        public void InitializeDatabase()
        {

        }
    }
}
