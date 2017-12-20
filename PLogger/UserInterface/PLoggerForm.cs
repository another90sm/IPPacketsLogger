using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using InitialApplicationStart;
using PacketHeaders;

namespace PLogger.UserInterface
{
    public partial class PLoggerForm : Form
    {
        private Socket _socket;
        private byte[] _byteData = new byte[4096];
        private bool _continueCapturing = false;
        private bool _pauseCapturing = false;

        private delegate void AddTreeNode(TreeNode node);

        public PLoggerForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.PLogger_Logo;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (cmbInterfaces.Text == "")
            {
                MessageBox.Show("Select an Interface to capture the packets.", "PLogger",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (!_continueCapturing)
                {
                    this.SetStartReceiveVariables();
                }
                else
                {
                    this.SetStopReceiveVariables();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PLogger", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.SetStopReceiveVariables();
            }
        }

        private void SetStartReceiveVariables()
        {
            btnStart.Text = "Stop";
            btnPause.Text = "Pause";

            btnPause.Enabled = true;
            cmbInterfaces.Enabled = false;

            _continueCapturing = true;
            _pauseCapturing = false;

            _socket = new Socket(AddressFamily.InterNetwork,
SocketType.Raw, ProtocolType.IP);

            _socket.Bind(new IPEndPoint(IPAddress.Parse(cmbInterfaces.Text), 0));

            _socket.SetSocketOption(SocketOptionLevel.IP,
                    SocketOptionName.HeaderIncluded,
                    true);

            byte[] inValue = new byte[4] { 1, 0, 0, 0 };
            byte[] outValue = new byte[4] { 1, 0, 0, 0 };


            _socket.IOControl(IOControlCode.ReceiveAll,
                                 inValue,
                                 outValue);

            _socket.BeginReceive(_byteData, 0, _byteData.Length, SocketFlags.None,
new AsyncCallback(OnReceive), null);
        }
            
        private void SetStopReceiveVariables()
        {
            btnStart.Text = "Start";
            btnPause.Text = "Pause";

            btnPause.Enabled = false;
            cmbInterfaces.Enabled = true;

            _continueCapturing = false;
            _pauseCapturing = true;

            if (_socket != null)
            {
                _socket.Close();
                _socket.Dispose();
            }
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (!_pauseCapturing)
            {
                _pauseCapturing = true;
                btnPause.Text = "Continue";
            }
            else
            {
                _pauseCapturing = false;
                btnPause.Text = "Pause";
            }
        }

        private async void OnReceive(IAsyncResult ar)
        {
            try
            {
                int received = _socket.EndReceive(ar);

                await Task.Factory.StartNew(() => ParseData(_byteData, received));

                if (_continueCapturing)
                {
                    _byteData = new byte[4096];
                    _socket.BeginReceive(_byteData, 0, _byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PLogger", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ParseData(byte[] byteData, int nReceived)
        {
            TreeNode rootNode = new TreeNode();

            IPHeader ipHeader = new IPHeader(byteData, nReceived);
            TCPHeader tcpHeader = null;
            UDPHeader udpHeader = null;
            DNSHeader dnsHeader = null;

            TreeNode ipNode = MakeIPTreeNode(ipHeader);
            rootNode.Nodes.Add(ipNode);

            switch (ipHeader.ProtocolType)
            {
                case Protocol.TCP:

                    tcpHeader = new TCPHeader(ipHeader.Data, ipHeader.MessageLength);

                    TreeNode tcpNode = MakeTCPTreeNode(tcpHeader);

                    rootNode.Nodes.Add(tcpNode);

                    if (tcpHeader.DestinationPort == "53" || tcpHeader.SourcePort == "53")
                    {
                        dnsHeader = new DNSHeader(udpHeader.Data, Convert.ToInt32(udpHeader.Length) - 8);

                        TreeNode dnsNode = MakeDNSTreeNode(dnsHeader);
                        rootNode.Nodes.Add(dnsNode);
                    }

                    break;

                case Protocol.UDP:

                    udpHeader = new UDPHeader(ipHeader.Data, (int)ipHeader.MessageLength);

                    TreeNode udpNode = MakeUDPTreeNode(udpHeader);

                    rootNode.Nodes.Add(udpNode);

                    if (udpHeader.DestinationPort == "53" || udpHeader.SourcePort == "53")
                    {
                        dnsHeader = new DNSHeader(udpHeader.Data, Convert.ToInt32(udpHeader.Length) - 8);

                        TreeNode dnsNode = MakeDNSTreeNode(dnsHeader);
                        rootNode.Nodes.Add(dnsNode);
                    }

                    break;

                case Protocol.Unknown:
                    break;
            }

            if (DataBaseInformation.WorkWithDataBase)
            {
                if (!IPHeader.SaveData(ipHeader, tcpHeader, udpHeader, dnsHeader))
                {
                    this.BeginInvoke(new Action(() => this.SetStopReceiveVariables()));
                    this.BeginInvoke(new Action(() => MessageBox.Show("Error occurred while saving data to Database!\r\nView error log file for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                }
            }

            if (!_pauseCapturing && this.WindowState != FormWindowState.Minimized)
            {
                AddTreeNode addTreeNode = new AddTreeNode(OnAddTreeNode);
                string sourceAddress = ipHeader.SourceAddress.ToString();
                string destinationAddress = ipHeader.DestinationAddress.ToString();
                string when = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.ffff");

                rootNode.Text = $"{sourceAddress} - {destinationAddress}  at:{when}";

                treeView.Invoke(addTreeNode, new object[] { rootNode });
            }
        }

        private TreeNode MakeIPTreeNode(IPHeader ipHeader)
        {
            TreeNode ipNode = new TreeNode();

            ipNode.Text = "IP";
            ipNode.Nodes.Add("Ver: " + ipHeader.Version);
            ipNode.Nodes.Add("Header Length: " + ipHeader.HeaderLength);
            ipNode.Nodes.Add("Differntiated Services: " + ipHeader.DifferentiatedServices);
            ipNode.Nodes.Add("Total Length: " + ipHeader.TotalLength);
            ipNode.Nodes.Add("Identification: " + ipHeader.Identification);
            ipNode.Nodes.Add("Flags: " + ipHeader.Flags);
            ipNode.Nodes.Add("Fragmentation Offset: " + ipHeader.FragmentationOffset);
            ipNode.Nodes.Add("Time to live: " + ipHeader.TTL);
            switch (ipHeader.ProtocolType)
            {
                case Protocol.TCP:
                    ipNode.Nodes.Add("Protocol: " + "TCP");
                    break;
                case Protocol.UDP:
                    ipNode.Nodes.Add("Protocol: " + "UDP");
                    break;
                case Protocol.Unknown:
                    ipNode.Nodes.Add("Protocol: " + "Unknown");
                    break;
            }
            ipNode.Nodes.Add("Checksum: " + ipHeader.Checksum);
            ipNode.Nodes.Add("Source: " + ipHeader.SourceAddress.ToString());
            ipNode.Nodes.Add("Destination: " + ipHeader.DestinationAddress.ToString());

            return ipNode;
        }

        private TreeNode MakeTCPTreeNode(TCPHeader tcpHeader)
        {
            TreeNode tcpNode = new TreeNode();

            tcpNode.Text = "TCP";

            tcpNode.Nodes.Add("Source Port: " + tcpHeader.SourcePort);
            tcpNode.Nodes.Add("Destination Port: " + tcpHeader.DestinationPort);
            tcpNode.Nodes.Add("Sequence Number: " + tcpHeader.SequenceNumber);

            if (tcpHeader.AcknowledgementNumber != "")
                tcpNode.Nodes.Add("Acknowledgement Number: " + tcpHeader.AcknowledgementNumber);

            tcpNode.Nodes.Add("Header Length: " + tcpHeader.HeaderLength);
            tcpNode.Nodes.Add("Flags: " + tcpHeader.Flags);
            tcpNode.Nodes.Add("Window Size: " + tcpHeader.WindowSize);
            tcpNode.Nodes.Add("Checksum: " + tcpHeader.Checksum);

            if (tcpHeader.UrgentPointer != "")
                tcpNode.Nodes.Add("Urgent Pointer: " + tcpHeader.UrgentPointer);

            return tcpNode;
        }

        private TreeNode MakeUDPTreeNode(UDPHeader udpHeader)
        {
            TreeNode udpNode = new TreeNode();

            udpNode.Text = "UDP";
            udpNode.Nodes.Add("Source Port: " + udpHeader.SourcePort);
            udpNode.Nodes.Add("Destination Port: " + udpHeader.DestinationPort);
            udpNode.Nodes.Add("Length: " + udpHeader.Length);
            udpNode.Nodes.Add("Checksum: " + udpHeader.Checksum);

            return udpNode;
        }

        private TreeNode MakeDNSTreeNode(DNSHeader dnsHeader)
        {
            TreeNode dnsNode = new TreeNode();

            dnsNode.Text = "DNS";
            dnsNode.Nodes.Add("Identification: " + dnsHeader.Identification);
            dnsNode.Nodes.Add("Flags: " + dnsHeader.Flags);
            dnsNode.Nodes.Add("Questions: " + dnsHeader.TotalQuestions);
            dnsNode.Nodes.Add("Answer RRs: " + dnsHeader.TotalAnswer);
            dnsNode.Nodes.Add("Authority RRs: " + dnsHeader.TotalAuthority);
            dnsNode.Nodes.Add("Additional RRs: " + dnsHeader.TotalAdditional);

            return dnsNode;
        }

        private void OnAddTreeNode(TreeNode node)
        {
            treeView.Nodes.Insert(0, node);
            if (treeView.Nodes.Count > 100)
            {
                treeView.Nodes.RemoveAt(100);
            }
        }

        private void LoggerForm_Load(object sender, EventArgs e)
        {
            string strIP = null;

            IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
            if (HosyEntry.AddressList.Length > 0)
            {
                foreach (IPAddress ip in HosyEntry.AddressList)
                {
                    strIP = ip.ToString();
                    cmbInterfaces.Items.Add(strIP);
                }
            }
        }

        private void LoggerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_continueCapturing)
            {
                if (_socket != null)
                    _socket.Close();
            }
        }
    }
}
