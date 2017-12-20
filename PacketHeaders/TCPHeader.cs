using System.Net;
using System.Text;
using System;
using System.IO;
using DataAccess;
using System.Data;

namespace PacketHeaders
{
    public class TCPHeader
    {
        private ushort _sourcePort;
        private ushort _destinationPort;
        private uint _sequenceNumber = 555;
        private uint _acknowledgementNumber = 555;
        private ushort _dataOffsetAndFlags = 555;
        private ushort _window = 555;
        private short _checksum = 555;
        private ushort _urgentPointer;
        private byte _headerLength;
        private ushort _messageLength;
        private byte[] _TCPData = new byte[4096];

        public TCPHeader(byte[] buffer, int received)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(buffer, 0, received);
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                _sourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _destinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _sequenceNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
                _acknowledgementNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
                _dataOffsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _window = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _checksum = (short)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _urgentPointer = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                _headerLength = (byte)(_dataOffsetAndFlags >> 12);
                _headerLength *= 4;
                _messageLength = (ushort)(received - _headerLength);

                Array.Copy(buffer, _headerLength, _TCPData, 0, received - _headerLength);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "PLoggerTCP" + (received), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string SourcePort
        {
            get
            {
                return _sourcePort.ToString();
            }
        }

        public string DestinationPort
        {
            get
            {
                return _destinationPort.ToString();
            }
        }

        public string SequenceNumber
        {
            get
            {
                return _sequenceNumber.ToString();
            }
        }

        public string AcknowledgementNumber
        {
            get
            {
                if ((_dataOffsetAndFlags & 0x10) != 0)
                {
                    return _acknowledgementNumber.ToString();
                }
                else
                    return "";
            }
        }

        public string HeaderLength
        {
            get
            {
                return _headerLength.ToString();
            }
        }

        public string WindowSize
        {
            get
            {
                return _window.ToString();
            }
        }

        public string UrgentPointer
        {
            get
            {
                if ((_dataOffsetAndFlags & 0x20) != 0)
                {
                    return _urgentPointer.ToString();
                }
                else
                    return "";
            }
        }

        public string Flags
        {
            get
            {
                int nFlags = _dataOffsetAndFlags & 0x3F;

                string strFlags = string.Format("0x{0:x2} (", nFlags);

                if ((nFlags & 0x01) != 0)
                {
                    strFlags += "FIN, ";
                }
                if ((nFlags & 0x02) != 0)
                {
                    strFlags += "SYN, ";
                }
                if ((nFlags & 0x04) != 0)
                {
                    strFlags += "RST, ";
                }
                if ((nFlags & 0x08) != 0)
                {
                    strFlags += "PSH, ";
                }
                if ((nFlags & 0x10) != 0)
                {
                    strFlags += "ACK, ";
                }
                if ((nFlags & 0x20) != 0)
                {
                    strFlags += "URG";
                }
                strFlags += ")";

                if (strFlags.Contains("()"))
                {
                    strFlags = strFlags.Remove(strFlags.Length - 3);
                }
                else if (strFlags.Contains(", )"))
                {
                    strFlags = strFlags.Remove(strFlags.Length - 3, 2);
                }

                return strFlags;
            }
        }

        public string Checksum
        {
            get
            {
                return string.Format("0x{0:x2}", _checksum);
            }
        }

        public byte[] Data
        {
            get
            {
                return _TCPData;
            }
        }

        public ushort MessageLength
        {
            get
            {
                return _messageLength;
            }
        }

        public void Save(int id, IDbTransaction transaction)
        {
            DBManager.GetInstance().DataAccess.InsertTCPHeader(id, this._sourcePort, this._destinationPort, this._sequenceNumber, this._acknowledgementNumber, this._dataOffsetAndFlags, this._window, this._checksum, this._urgentPointer, this._headerLength, this._messageLength, this._TCPData, transaction);
        }
    }
}