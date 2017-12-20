using System.Data;

namespace DataAccess.Interfaces
{
    public interface IDataAccess : IBaseDataAccess
    {
        bool IsDatabaseExist();
        bool CreateDatabase();
        IDataReader GetDBTablesStructure();
        void CreateTable(string tableName, object[,] columnsParameter);
        int InsertIPHeader(byte versionAndHeaderLength, byte differentiatedServices, ushort totalLength, ushort identification, ushort flagsAndOffset, byte ttl, byte protocol, short checksum, uint sourceIPAddress, uint destinationIPAddress, byte headerLength, byte[] ipData, IDbTransaction transaction);
        void InsertTCPHeader(int id, ushort sourcePort, ushort destinationPort, uint sequenceNumber, uint acknowledgementNumber, ushort dataOffsetAndFlags, ushort window, short checksum, ushort urgentPointer, byte headerLength, ushort messageLength, byte[] tcpData, IDbTransaction transaction);
        void InsertUDPHeader(int id, ushort sourcePort, ushort destinationPort, ushort length, short checksum, byte[] udpData, IDbTransaction transaction);
        void InsertDNSHeader(int id, ushort identification, ushort flags, ushort totalQuestions, ushort totalAnswer, ushort totalAuthority, ushort totalAdditional, IDbTransaction transaction);
    }
}
