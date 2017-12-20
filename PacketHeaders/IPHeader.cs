using DataAccess;
using System;
using System.IO;
using System.Net;
using System.Data;

namespace PacketHeaders
{
    public enum Protocol
    {
        TCP = 6,
        UDP = 17,
        Unknown = -1
    };
    public class IPHeader
    {

        public static bool SaveData(IPHeader ipHeader, TCPHeader tcpHeader, UDPHeader udpHeader, DNSHeader dnsHeader)
        {
            var dataAccess = DBManager.GetInstance().DataAccess;

            try
            {
                var transaction = dataAccess.BeginTransaction();

                var id = ipHeader.Save(transaction);

                if (tcpHeader != null)
                {
                    tcpHeader.Save(id, transaction);
                }
                if (udpHeader != null)
                {
                    udpHeader.Save(id, transaction);
                }
                if (dnsHeader != null)
                {
                    dnsHeader.Save(id, transaction);
                }

                dataAccess.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                dataAccess.RollbackTransaction();
                return false;
            }
        }

        private byte _versionAndHeaderLength;
        private byte _differentiatedServices;
        private ushort _totalLength;
        private ushort _identification;
        private ushort _flagsAndOffset;
        private byte _ttl;
        private byte _protocol;
        private short _checksum;
        private uint _sourceIPAddress;
        private uint _destinationIPAddress;
        private byte _headerLength;
        private byte[] _IPData = new byte[4096];

        public IPHeader(byte[] buffer, int received)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(buffer, 0, received);
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                _versionAndHeaderLength = binaryReader.ReadByte();
                _differentiatedServices = binaryReader.ReadByte();
                _totalLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _identification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _flagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _ttl = binaryReader.ReadByte();
                _protocol = binaryReader.ReadByte();
                _checksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _sourceIPAddress = (uint)(binaryReader.ReadInt32());
                _destinationIPAddress = (uint)(binaryReader.ReadInt32());
                _headerLength = _versionAndHeaderLength;

                _headerLength <<= 4;
                _headerLength >>= 4;

                _headerLength *= 4;

                Array.Copy(buffer,
                           _headerLength,
                           _IPData, 0,
                           _totalLength - _headerLength);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "PLogger", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public string Version
        {
            get
            {
                if ((_versionAndHeaderLength >> 4) == 4)
                {
                    return "IP v4";
                }
                else if ((_versionAndHeaderLength >> 4) == 6)
                {
                    return "IP v6";
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        public string HeaderLength
        {
            get
            {
                return _headerLength.ToString();
            }
        }

        public ushort MessageLength
        {
            get
            {
                return (ushort)(_totalLength - _headerLength);
            }
        }

        public string DifferentiatedServices
        {
            get
            {
                return string.Format("0x{0:x2} ({1})", _differentiatedServices, _differentiatedServices);
            }
        }

        public string Flags
        {
            get
            {
                int nFlags = _flagsAndOffset >> 13;
                if (nFlags == 2)
                {
                    return "Don't fragment";
                }
                else if (nFlags == 1)
                {
                    return "More fragments to come";
                }
                else
                {
                    return nFlags.ToString();
                }
            }
        }

        public string FragmentationOffset
        {
            get
            {
                int nOffset = _flagsAndOffset << 3;
                nOffset >>= 3;

                return nOffset.ToString();
            }
        }

        public string TTL
        {
            get
            {
                return _ttl.ToString();
            }
        }

        public Protocol ProtocolType
        {
            get
            {
                if (_protocol == 6)
                {
                    return Protocol.TCP;
                }
                else if (_protocol == 17)
                {
                    return Protocol.UDP;
                }
                else
                {
                    return Protocol.Unknown;
                }
            }
        }

        public string Checksum
        {
            get
            {
                return string.Format("0x{0:x2}", _checksum);
            }
        }

        public IPAddress SourceAddress
        {
            get
            {
                return new IPAddress(_sourceIPAddress);
            }
        }

        public IPAddress DestinationAddress
        {
            get
            {
                return new IPAddress(_destinationIPAddress);
            }
        }

        public string TotalLength
        {
            get
            {
                return _totalLength.ToString();
            }
        }

        public string Identification
        {
            get
            {
                return _identification.ToString();
            }
        }

        public byte[] Data
        {
            get
            {
                return _IPData;
            }
        }

        public int Save(IDbTransaction transaction)
        {
            return DBManager.GetInstance().DataAccess.InsertIPHeader(this._versionAndHeaderLength, this._differentiatedServices, this._totalLength, this._identification, this._flagsAndOffset, this._ttl, this._protocol, this._checksum, this._sourceIPAddress, this._destinationIPAddress, this._headerLength, this._IPData, transaction);
        }
    }
}
