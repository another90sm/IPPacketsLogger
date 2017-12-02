using System;
using System.Data;
using DataAccess.Enums;
using DataAccess.Interfaces;
using System.IO;
using System.Data.SQLite;
using System.Text;

namespace DataAccess.DataAccesses
{
    internal class DataAccessSQLite : BaseDataAccess, IDataAccess
    {
        private const string _passPhrase = "";//"Wh0gIvEsAsH1t";
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
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(string.Format("CREATE TABLE {0} (", tableName));

            for (int i = 0; i < columnsParameter.GetLength(0); i++)
            {
                var colName = (string)columnsParameter[i, 0];
                var colType = (decimal)columnsParameter[i, 5] <= 0 ? (string)columnsParameter[i, 1] : (string)columnsParameter[i, 1] + "(" + columnsParameter[i, 5].ToString() + ")";
                var allowNull = (bool)columnsParameter[i, 2] ? string.Empty : "NOT NULL ";
                var primaryKey = (bool)columnsParameter[i, 3] ? "PRIMARY KEY " : string.Empty;
                var autoincrement = (bool)columnsParameter[i, 4] ? "AUTOINCREMENT " : string.Empty;

                if (i == columnsParameter.GetLength(0) - 1)
                    sql.AppendLine(string.Format("{0} {1} {2}{3}{4}", colName, colType, allowNull, primaryKey, autoincrement));
                else
                    sql.AppendLine(string.Format("{0} {1} {2}{3}{4},", colName, colType, allowNull, primaryKey, autoincrement));
            }

            sql.Append(")");

            using (var command = base.GetCommand(sql.ToString(),base.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
