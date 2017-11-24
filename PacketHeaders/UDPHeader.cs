using System;
using System.IO;
using System.Net;

namespace PacketHeaders
{
    public class UDPHeader
    {
        private ushort _sourcePort;
        private ushort _destinationPort;
        private ushort _length;
        private short _checksum;
        private byte[] _UDPData = new byte[4096];

        public UDPHeader(byte[] buffer, int received)
        {
            MemoryStream memoryStream = new MemoryStream(buffer, 0, received);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            _sourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            _destinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            _length = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            _checksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            Array.Copy(buffer,
                       8,
                       _UDPData,
                       0,
                       received - 8);
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

        public string Length
        {
            get
            {
                return _length.ToString();
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
                return _UDPData;
            }
        }
    }
}