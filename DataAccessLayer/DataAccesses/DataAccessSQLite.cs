using System;
using System.Data;
using DataAccess.Enums;
using DataAccess.Interfaces;
using System.IO;
using System.Data.SQLite;
using System.Text;
using Utils.MessageLogging;

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

            using (var command = base.GetCommand(sql.ToString(), base.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }

        public int InsertIPHeader(byte versionAndHeaderLength, byte differentiatedServices, ushort totalLength, ushort identification, ushort flagsAndOffset, byte ttl, byte protocol, short checksum, uint sourceIPAddress, uint destinationIPAddress, byte headerLength, byte[] ipData, IDbTransaction transaction)
        {
            string insertSql = "INSERT INTO IP_HEADERS (VERSION_AND_HEADER_LENGTH, DIFFERENTIATED_SERVICES, TOTAL_LENGTH, IDENTIFICATION, FLAGS_AND_OFFSET, TTL, PROTOCOL, CHECKSUM, SOURCE_IP_ADDRESS, DESTINATION_IP_ADDRESS, HEADER_LENGTH, IP_DATA, TDT) VALUES (@pVersionAndHeaderLength, @pDifferentiatedServices, @pTotalLength, @pIdentification, @pFlagsAndOffset, @pTtl, @pProtocol, @pChecksum, @pSourceIPAddress, @pDestinationIPAddress, @pHeaderLength, @pIPData, DATETIME('now'))";
            string selectIdSql = "select last_insert_rowid()";

            try
            {
                int id = 0;

                using (var command = (SQLiteCommand)base.GetCommandWithTransaction(insertSql.ToString(), base.GetConnection(), transaction))
                {
                    command.Parameters.Add("@pVersionAndHeaderLength", DbType.UInt16).Value = versionAndHeaderLength;
                    command.Parameters.Add("@pDifferentiatedServices", DbType.UInt16).Value = differentiatedServices;
                    command.Parameters.Add("@pTotalLength", DbType.UInt32).Value = totalLength;
                    command.Parameters.Add("@pIdentification", DbType.UInt32).Value = identification;
                    command.Parameters.Add("@pFlagsAndOffset", DbType.UInt32).Value = flagsAndOffset;
                    command.Parameters.Add("@pTtl", DbType.UInt16).Value = ttl;
                    command.Parameters.Add("@pProtocol", DbType.UInt16).Value = protocol;
                    command.Parameters.Add("@pChecksum", DbType.Int16).Value = checksum;
                    command.Parameters.Add("@pSourceIPAddress", DbType.UInt64).Value = sourceIPAddress;
                    command.Parameters.Add("@pDestinationIPAddress", DbType.UInt64).Value = destinationIPAddress;
                    command.Parameters.Add("@pHeaderLength", DbType.UInt16).Value = headerLength;
                    command.Parameters.Add("@pIPData", DbType.Binary).Value = ipData;

                    command.ExecuteNonQuery();

                    command.CommandText = selectIdSql;

                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        id = Convert.ToInt32(result);
                    }

                    return id;
                }
            }
            catch (Exception ex)
            {
                MessageLoggManager.LogErr(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public void InsertTCPHeader(int id, ushort sourcePort, ushort destinationPort, uint sequenceNumber, uint acknowledgementNumber, ushort dataOffsetAndFlags, ushort window, short checksum, ushort urgentPointer, byte headerLength, ushort messageLength, byte[] tcpData, IDbTransaction transaction)
        {
            string insertSql = "INSERT INTO TCP_HEADERS (ID, SOURCE_PORT, DESTINATION_PORT, SEQUENCE_NUMBER, ACKNOWLEDGEMENT_NUMBER, DATA_OFFSET_AND_FLAGS, WINDOW, CHECKSUM, URGENT_POINTER, HEADER_LENGTH, MESSAGE_LENGTH, TCP_DATA, TDT) VALUES (@pId, @pSourcePort, @pDestinationPort, @pSequenceNumber, @pAcknowledgementNumber, @pDataOffsetAndFlags, @pWindow, @pChecksum, @pUrgentPointer, @pHeaderLength, @pMessageLength, @pTCPData, DATETIME('now'))";

            try
            {
                using (var command = (SQLiteCommand)base.GetCommandWithTransaction(insertSql.ToString(), base.GetConnection(), transaction))
                {
                    command.Parameters.Add("@pId", DbType.UInt32).Value = id;
                    command.Parameters.Add("@pSourcePort", DbType.UInt16).Value = sourcePort;
                    command.Parameters.Add("@pDestinationPort", DbType.UInt16).Value = destinationPort;
                    command.Parameters.Add("@pSequenceNumber", DbType.UInt32).Value = sequenceNumber;
                    command.Parameters.Add("@pAcknowledgementNumber", DbType.UInt32).Value = acknowledgementNumber;
                    command.Parameters.Add("@pDataOffsetAndFlags", DbType.UInt16).Value = dataOffsetAndFlags;
                    command.Parameters.Add("@pWindow", DbType.UInt16).Value = window;
                    command.Parameters.Add("@pChecksum", DbType.Int16).Value = checksum;
                    command.Parameters.Add("@pUrgentPointer", DbType.UInt16).Value = urgentPointer;
                    command.Parameters.Add("@pHeaderLength", DbType.Byte).Value = headerLength;
                    command.Parameters.Add("@pMessageLength", DbType.UInt16).Value = messageLength;
                    command.Parameters.Add("@pTCPData", DbType.Binary).Value = tcpData;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageLoggManager.LogErr(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public void InsertUDPHeader(int id, ushort sourcePort, ushort destinationPort, ushort length, short checksum, byte[] udpData, IDbTransaction transaction)
        {
            string insertSql = "INSERT INTO UDP_HEADERS (ID, SOURCE_PORT, DESTINATION_PORT, LENGTH, CHECKSUM, UDP_DATA, TDT) VALUES (@pId, @pSourcePort, @pDestinationPort, @pLength, @pChecksum, @pUDPData, DATETIME('now'))";

            try
            {
                using (var command = (SQLiteCommand)base.GetCommandWithTransaction(insertSql.ToString(), base.GetConnection(), transaction))
                {
                    command.Parameters.Add("@pId", DbType.UInt32).Value = id;
                    command.Parameters.Add("@pSourcePort", DbType.UInt16).Value = sourcePort;
                    command.Parameters.Add("@pDestinationPort", DbType.UInt16).Value = destinationPort;
                    command.Parameters.Add("@pLength", DbType.UInt16).Value = length;
                    command.Parameters.Add("@pChecksum", DbType.Int16).Value = checksum;
                    command.Parameters.Add("@pUDPData", DbType.Binary).Value = udpData;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageLoggManager.LogErr(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public void InsertDNSHeader(int id, ushort identification, ushort flags, ushort totalQuestions, ushort totalAnswer, ushort totalAuthority, ushort totalAdditional, IDbTransaction transaction)
        {
            string insertSql = "INSERT INTO DNS_HEADERS (ID, IDENTIFICATION, FLAGS, TOTAL_QUESTIONS, TOTAL_ANSWER, TOTAL_AUTHORITY, TOTAL_ADDITIONAL, TDT) VALUES (@pId, @pIdentification, @pFlags, @pTotalQuestions, @pTotalAnswer, @pTotalAuthority, @pTotalAdditional, DATETIME('now'))";

            try
            {
                using (var command = (SQLiteCommand)base.GetCommandWithTransaction(insertSql.ToString(), base.GetConnection(), transaction))
                {
                    command.Parameters.Add("@pId", DbType.UInt32).Value = id;
                    command.Parameters.Add("@pIdentification", DbType.UInt16).Value = identification;
                    command.Parameters.Add("@pFlags", DbType.UInt16).Value = flags;
                    command.Parameters.Add("@pTotalQuestions", DbType.UInt16).Value = totalQuestions;
                    command.Parameters.Add("@pTotalAnswer", DbType.UInt16).Value = totalAnswer;
                    command.Parameters.Add("@pTotalAuthority", DbType.UInt16).Value = totalAuthority;
                    command.Parameters.Add("@pTotalAdditional", DbType.UInt16).Value = totalAdditional;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageLoggManager.LogErr(ex.Message, ex.StackTrace);
                throw ex;
            }
        }
    }
}
