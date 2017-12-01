using System;
using System.Data;
using DataAccess.Enums;
using DataAccess.Interfaces;
using System.IO;
using System.Data.SQLite;

namespace DataAccess.DataAccesses
{
    internal class DataAccessSQLite : BaseDataAccess, IDataAccess
    {
        private const string _passPhrase = "Wh0gIvEsAsH1t";
        private const string _dbFileName = "sqlite.db";
        private string _filePath;
        internal DataAccessSQLite(string filePath)
            : base(DataBaseType.SQLite, string.Format("Data source={0}\\{1};Version=3;Page Size=1024;Password={2};", filePath, _dbFileName, _passPhrase))
        {
            this._filePath = filePath;
        }
        public bool IsDatabaseExist()
        {
            return File.Exists(this._filePath + "\\" + _dbFileName);
        }
        public bool CreateDatabase()
        {
            try
            {
                if (!Directory.Exists(this._filePath))
                {
                    Directory.CreateDirectory(this._filePath);
                }
                SQLiteConnection.CreateFile(this._filePath + "\\" + _dbFileName);
                var conn = (SQLiteConnection)base.GetConnection();
                base.OpenConnection();
                conn.ChangePassword(_passPhrase);
                base.CloseConnection();
                return true;
            }
            catch (SQLiteException sqex)
            {
                throw sqex;
            }
            catch (IOException ioex)
            {
                throw ioex;
            }
        }
        public IDataReader GetDBTablesStructure()
        {
            throw new NotImplementedException();
        }

        public void CreateTable(string tableName, object[,] columnsParameter)
        {
           
        }
    }
}
