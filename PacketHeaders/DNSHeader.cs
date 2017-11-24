using System.IO;
using System.Net;

namespace PacketHeaders
{
    public class DNSHeader
    {
        private ushort _identification;
        private ushort _flags;
        private ushort _totalQuestions;
        private ushort _totalAnswer;
        private ushort _totalAuthority;
        private ushort _totalAdditional;

        public DNSHeader(byte[] buffer, int received)
        {
            MemoryStream memoryStream = new MemoryStream(buffer, 0, received);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            _identification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            _flags = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            _totalQuestions = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            _totalAnswer = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            _totalAuthority = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            _totalAdditional = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
        }

        public string Identification
        {
            get
            {
                return string.Format("0x{0:x2}", _identification);
            }
        }

        public string Flags
        {
            get
            {
                return string.Format("0x{0:x2}", _flags);
            }
        }

        public string TotalQuestions
        {
            get
            {
                return _totalQuestions.ToString();
            }
        }

        public string TotalAnswer
        {
            get
            {
                return _totalAnswer.ToString();
            }
        }

        public string TotalAuthority
        {
            get
            {
                return _totalAuthority.ToString();
            }
        }

        public string TotalAdditional
        {
            get
            {
                return _totalAdditional.ToString();
            }
        }
    }
}
