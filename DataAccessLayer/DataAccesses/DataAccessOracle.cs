using System;
using System.Data;
using DataAccess.Enums;
using DataAccess.Interfaces;

namespace DataAccess.DataAccesses
{
    internal class DataAccessOracle : BaseDataAccess, IDataAccess
    {
        internal DataAccessOracle(string connectionString)
            : base(DataBaseType.SQLite, connectionString)
        {

        }

        public bool CreateDatabase()
        {
            throw new NotImplementedException();
        }

        public void CreateTable(string tableName, object[,] columnsParameter)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetDBTablesStructure()
        {
            throw new NotImplementedException();
        }

        public void InsertDNSHeader(int id, ushort identification, ushort flags, ushort totalQuestions, ushort totalAnswer, ushort totalAuthority, ushort totalAdditional, IDbTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public int InsertIPHeader(byte versionAndHeaderLength, byte differentiatedServices, ushort totalLength, ushort identification, ushort flagsAndOffset, byte ttl, byte protocol, short checksum, uint sourceIPAddress, uint destinationIPAddress, byte headerLength, byte[] ipData, IDbTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void InsertTCPHeader(int id, ushort sourcePort, ushort destinationPort, uint sequenceNumber, uint acknowledgementNumber, ushort dataOffsetAndFlags, ushort window, short checksum, ushort urgentPointer, byte headerLength, ushort messageLength, byte[] tcpData, IDbTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void InsertUDPHeader(int id, ushort sourcePort, ushort destinationPort, ushort length, short checksum, byte[] udpData, IDbTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public bool IsDatabaseExist()
        {
            throw new NotImplementedException();
        }
    }
}
