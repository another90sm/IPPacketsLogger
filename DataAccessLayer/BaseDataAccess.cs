using DataAccess.Enums;
using DataAccess.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SQLite;
namespace DataAccess
{
    internal abstract class BaseDataAccess : IBaseDataAccess
    {
        private DataBaseType _dbType;
        private string _connectionString;
        IDbConnection _connection;
        IDbTransaction _transaction;

        public DataBaseType DBType
        {
            get { return this._dbType; }
            protected set { this._dbType = value; }
        }
        public string ConnectionString
        {
            get { return this._connectionString; }
            protected set { this._connectionString = value; }
        }
        internal BaseDataAccess(DataBaseType dbType, string connectionString)
        {
            this._dbType = dbType;
            this._connectionString = connectionString;
            this._connection = null;
            this._transaction = null;
        }
        public bool HasConnection
        {
            get
            {
                return this._connection != null;
            }
        }

        public bool HasTransaction
        {
            get
            {
                return this._transaction != null;
            }
        }

        public IDbTransaction BeginTransaction()
        {
            if (this._transaction != null)
                throw new DataException("BeginTransaction: Old transaction running!");

            this.OpenConnection();
            this._transaction = this._connection.BeginTransaction();
            return this._transaction;
        }

        public void ClearConnection()
        {
            this._connection = null;
        }

        public void ClearTransaction()
        {
            this._transaction = null;
        }

        public void CloseConnection()
        {
            this.GetConnection();
            if (this._connection != null)
            {
                if (this._transaction == null)
                {
                    if (this._connection.State != ConnectionState.Closed)
                    {
                        this._connection.Close();
                        this._connection.Dispose();
                        this._connection = null;
                    }
                }
            }
        }

        public void CommitTransaction()
        {
            if (this._transaction == null)
                throw new DataException("CommitTransaction: No transaction running!");
            this._transaction.Commit();
            this._transaction.Dispose();
            this._transaction = null;
            this.CloseConnection();
        }

        public virtual IDbConnection GetConnection()
        {
            if (this._connection == null)
            {
                switch (this._dbType)
                {
                    case DataBaseType.SQLite:
                        this._connection = new SQLiteConnection(this._connectionString);
                        break;
                    case DataBaseType.MySQL:
                        this._connection = new MySqlConnection(this._connectionString);
                        break;
                    case DataBaseType.MSSQL:
                        this._connection = new SqlConnection(this._connectionString);
                        break;
                    case DataBaseType.Oracle:
                        this._connection = new OracleConnection(this._connectionString);
                        break;
                }
            }
            return this._connection;
        }

        public void OpenConnection()
        {
            this.GetConnection();
            if (this._connection != null)
            {
                if (this._connection.State == ConnectionState.Closed || this._connection.State == ConnectionState.Broken)
                {
                    this._connection.Open();
                }
            }
        }

        public void RollbackTransaction()
        {
            if (this._transaction == null)
                throw new DataException("RollbackTransaction: No transaction running!");
            try
            {
                this._transaction.Rollback();
            }
            finally
            {
                this._transaction.Dispose();
                this._transaction = null;
                this.CloseConnection();
            }
        }

        public void SetTransaction(IDbTransaction transaction)
        {
            this._transaction = transaction;
        }

        public IDbCommand GetCommand(string commandText, IDbConnection connection)
        {
            switch (this._dbType)
            {
                case DataBaseType.SQLite:
                    return new SQLiteCommand(commandText, (SQLiteConnection)connection);
                case DataBaseType.MySQL:
                    return new MySqlCommand(commandText, (MySqlConnection)connection);
                case DataBaseType.MSSQL:
                    return new SqlCommand(commandText, (SqlConnection)connection);
                case DataBaseType.Oracle:
                    return new OracleCommand(commandText, (OracleConnection)connection);
                default:
                    return null;
            }
        }

        public IDbDataAdapter GetaDataAdapter(IDbCommand command)
        {
            switch (this._dbType)
            {
                case DataBaseType.SQLite:
                    return new SQLiteDataAdapter((SQLiteCommand)command);
                case DataBaseType.MySQL:
                    return new MySqlDataAdapter((MySqlCommand)command);
                case DataBaseType.MSSQL:
                    return new SqlDataAdapter((SqlCommand)command);
                case DataBaseType.Oracle:
                    return new OracleDataAdapter((OracleCommand)command);
                default:
                    return null;
            }
        }

        public void SetConnection(IDbConnection connection)
        {
            this._connection = connection;
        }
    }
}
