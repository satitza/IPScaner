using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPScanner.Utility;
using System.Windows.Forms;
using IPScanner.Models;
using IPScanner.Services;
using IPScanner.Services.Imples;
using System.Threading;
using SharpPcap.LibPcap;
using SharpPcap;
using PacketDotNet;
using System.Net;
using System.Net.NetworkInformation;

namespace IPScanner
{
    public partial class MainForm : Form
    {
        private ScanOptionModel ScanOption;

        private ICollection<string> listLogs = new List<string>();

        private IIPScannerService IPScannerService;

        private ISnifferService SnifferService;

        private INetworkService NetworkService;

        private IARPSproofService ARPSproofService;

        private bool ScanPortState { get; set; } = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.ScanOption = new ScanOptionModel();
                this.IPScannerService = new IPScannerService(this.listLogs);
                this.SnifferService = new SnifferService();
                this.NetworkService = new NetworkService();
                this.ARPSproofService = new ARPSproofService();
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (!e.Node.Text.Equals("Host") && this.ScanPortState == false)
                    {
                        this.treeView.ContextMenuStrip = this.contextMenuStrip;
                    }
                    else
                    {
                        this.treeView.ContextMenuStrip = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private async void btn_scan_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.txt_from.Text.Trim()) || String.IsNullOrEmpty(this.txt_to.Text.Trim()))
                {
                    MessageBoxUtils.Warning("กรุณาระบุช่วงของ IP Address");
                }
                else if (!(await this.IPScannerService.IsIPAddress(this.txt_from.Text.Trim())) || !(await this.IPScannerService.IsIPAddress(this.txt_to.Text.Trim())))
                {
                    MessageBoxUtils.Warning("รูปแบบ IP Address ไม่ถูกต้อง");
                }
                else if (!StringUtils.IsNumberOnly(this.txt_thread.Text.Trim()))
                {
                    MessageBoxUtils.Warning("กรุณาระบุจำนวน Thread เป็นตัวเลข");
                    this.txt_thread.Focus();
                }
                else if (!StringUtils.IsNumberOnly(this.txt_timeout.Text.Trim()))
                {
                    MessageBoxUtils.Warning("กรุณาระบุจำนวน Timeout เป็นตัวเลข");
                    this.txt_timeout.Focus();
                }
                else
                {

                    // set scan option
                    this.ScanOption.ipAddressFrom = this.txt_from.Text.Trim().Split('.');
                    this.ScanOption.ipAddressTo = this.txt_to.Text.Trim().Split('.');
                    int[] space = {
                                int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 4]) - int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 4]),
                                int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 3]) - int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 3]),
                                int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 2]) - int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 2]),
                                int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 1]) - int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 1])
                            };
                    this.ScanOption.space = space;
                    this.ScanOption.scanIp = this.checkBoxIPAddress.Checked;
                    this.ScanOption.scanHostname = this.checkBoxHostname.Checked;
                    this.ScanOption.numberOfThread = int.Parse(this.txt_thread.Text.Trim());
                    this.ScanOption.timeOut = int.Parse(this.txt_thread.Text.Trim());

                    if (int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 1]) > int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 1]))
                    {
                        MessageBoxUtils.Warning(String.Format("[ตำแหน่งที่ 4] {0} มากกว่า {1}", this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 1], this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 1]));
                    }
                    else if (int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 2]) > int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 2]))
                    {
                        MessageBoxUtils.Warning(String.Format("[ตำแหน่งที่ 3] {0} มากกว่า {1}", this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 2], this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 2]));
                    }
                    else if (int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 3]) > int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 3]))
                    {
                        MessageBoxUtils.Warning(String.Format("[ตำแหน่งที่ 2] {0} มากกว่า {1}", this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 3], this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 3]));
                    }
                    else if (int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 4]) > int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 4]))
                    {
                        MessageBoxUtils.Warning(String.Format("[ตำแหน่งที่ 1] {0} มากกว่า {1}", this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 4], this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 4]));
                    }
                    else
                    {
                        if (MessageBoxUtils.Question("ยืนยันการ Scan IP Address"))
                        {
                            this.btn_scan.Enabled = false;
                            this.btn_scan.Text = "Scanning...";
                            this.listViewPort.Items.Clear();
                            this.toolStripStatusLabel2.Text = "0";

                            /*---------------------------------------------------------------------------------------------------*/

                            this.treeView.Nodes.Clear();
                            this.listLogs.Clear();
                            this.logConsole.Clear();

                            this.logConsole.AppendText(String.Format("[{0}] Start scanning ...", DateTime.Now));
                            this.logConsole.AppendText(Environment.NewLine);

                            ICollection<HostInformationModel> results = await this.IPScannerService.Scan(this.ScanOption);

                            /* Tree view */
                            if (results.Count > 0)
                            {
                                this.treeView.Nodes.Add(new TreeNode("Host"));

                                foreach (HostInformationModel host in results)
                                {
                                    string data = "";
                                    if (!String.IsNullOrEmpty(host.Hostname))
                                    {
                                        data = String.Format("{0} [{1}]", host.IPAddress, host.Hostname);
                                    }
                                    else
                                    {
                                        data = String.Format("{0}", host.IPAddress);
                                    }

                                    this.treeView.Nodes[0].Nodes.Add(data);
                                }

                                this.treeView.ExpandAll();
                            }

                            /* Log console */
                            if (this.listLogs.Count > 0)
                            {
                                foreach (string log in listLogs)
                                {
                                    if (!String.IsNullOrEmpty(log))
                                    {
                                        this.logConsole.AppendText(log);
                                        this.logConsole.AppendText(Environment.NewLine);
                                    }
                                }
                            }

                            this.logConsole.AppendText(String.Format("[{0}] Scan success", DateTime.Now));
                            this.logConsole.AppendText(Environment.NewLine);

                            /*---------------------------------------------------------------------------------------------------*/

                            MessageBoxUtils.Information("Scan IP Address สำเร็จ");
                            this.btn_scan.Enabled = true;
                            this.btn_scan.Text = "Scan";

                            this.toolStripStatusLabel2.Text = results.Count.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private async void scanPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxUtils.Question("ยืนยันการ Scan Port"))
                {
                    this.listLogs.Clear();
                    this.logConsole.AppendText(String.Format("[{0}] Start port scanning ...", DateTime.Now));
                    this.logConsole.AppendText(Environment.NewLine);

                    this.ScanPortState = true;
                    this.listViewPort.Items.Clear();
                    ICollection<PortInformationModel> ports = await this.IPScannerService.ScanPort(this.treeView.SelectedNode.Text.Trim());

                    int number = 1;
                    foreach (var model in ports)
                    {
                        ListViewItem item = new ListViewItem(number.ToString());
                        item.SubItems.Add(model.IPAddress);
                        item.SubItems.Add(String.Format("{0}  [{1}]", model.PortNumber, model.PortDetail));
                        Action action = () => this.listViewPort.Items.Add(item);
                        this.listViewPort.Invoke(action);
                        number++;
                    }

                    foreach (string log in this.listLogs)
                    {
                        this.logConsole.AppendText(log);
                        this.logConsole.AppendText(Environment.NewLine);
                    }

                    MessageBoxUtils.Information("Scan port success.");
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
            finally
            {
                this.ScanPortState = false;
                ComponentUtils.ListViewCellAutoSize(this.listViewPort);
                this.logConsole.AppendText(String.Format("[{0}] Port scan success.", DateTime.Now));
                this.logConsole.AppendText(Environment.NewLine);
            }
        }

        private void capturePacketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.tabControl.SelectedTab = this.tabPage2;
                string[] ipArr = this.treeView.SelectedNode.Text.Split(' ');
                this.textBoxFilter.Text = String.Format("host {0}", ipArr[0]);
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        /******************************************************************************************************************************************/

        private Thread sniffing;

        private int packetNumber = 1;

        private LibPcapLiveDevice device;

        private ICollection<LibPcapLiveDevice> devices;

        private bool CaptureProcessStatus = false;

        Dictionary<int, Packet> CapturePacketLists;

        private PhysicalAddress gatewayMac;

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            // selected capture packet tab
            if (this.tabControl.SelectedIndex == 1)
            {
                if (this.devices == null)
                {
                    this.devices = new List<LibPcapLiveDevice>();
                }

                if (this.CapturePacketLists == null)
                {
                    this.CapturePacketLists = new Dictionary<int, Packet>();
                }

                this.InitialInterfaceCombobox();
            }
        }

        public void InitialInterfaceCombobox()
        {
            try
            {
                if (this.comboBoxInterface.Items.Count > 0)
                {
                    this.comboBoxInterface.Items.Clear();
                }

                foreach (LibPcapLiveDevice deviceName in this.SnifferService.GetAllDevice())
                {
                    this.comboBoxInterface.Items.Add(deviceName.Interface.FriendlyName);
                }

            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private void comboBoxInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.device = this.SnifferService.GetDeviceByIndex(this.comboBoxInterface.SelectedIndex);
                this.gatewayMac = this.NetworkService.GetGatewayMAC(this.device.Interface.FriendlyName);
                if (this.gatewayMac != null)
                {
                    this.toolStripStatusLabel4.Text = this.NetworkService.GetMACString(this.gatewayMac);
                    this.toolStripStatusLabel4.ForeColor = Color.Green;
                }
                else
                {
                    this.toolStripStatusLabel4.Text = "00:00:00:00:00:00";
                    this.toolStripStatusLabel4.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private void buttonCapture_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.comboBoxInterface.Text))
                {
                    MessageBoxUtils.Warning("กรุณาเลือก Interface");
                }
                else
                {
                    //Register our handler function to the 'packet arrival' event
                    this.device.OnPacketArrival +=
                        new PacketArrivalEventHandler(device_OnPacketArrival);

                    this.richTextBoxDetail.Clear();
                    this.richTextBoxHex.Clear();
                    this.comboBoxInterface.Enabled = false;

                    // start capture
                    if (this.CaptureProcessStatus == false)
                    {
                        if (MessageBoxUtils.Question("Start Capture"))
                        {
                            this.sniffing = new Thread(new ThreadStart(sniffing_Proccess));
                            this.sniffing.Start();

                            this.CaptureProcessStatus = true;
                            this.buttonStart.Enabled = false;
                            this.buttonStop.Enabled = true;

                            this.logConsole.AppendText(String.Format("[{0}] Start capture packet on interface {1}", DateTime.Now, this.device.Interface.FriendlyName));
                            this.logConsole.AppendText(Environment.NewLine);
                        }
                    }
                    // restart capture
                    else if (this.CaptureProcessStatus)
                    {
                        if (MessageBoxUtils.Question("Restart capture"))
                        {
                            packetNumber = 1;
                            this.listViewPacket.Items.Clear();
                            this.CapturePacketLists.Clear();

                            this.sniffing = new Thread(new ThreadStart(sniffing_Proccess));
                            this.sniffing.Start();

                            this.CaptureProcessStatus = true;
                            this.buttonStart.Enabled = false;
                            this.buttonStop.Enabled = true;

                            this.logConsole.AppendText(String.Format("[{0}] Start capture packet on interface {1}", DateTime.Now, this.device.Interface.FriendlyName));
                            this.logConsole.AppendText(Environment.NewLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxUtils.Question("Stop Capture"))
                {
                    this.sniffing.Abort();
                    this.device.StopCapture();
                    this.device.Close();

                    this.comboBoxInterface.Enabled = true;
                    this.buttonStart.Enabled = true;
                    this.buttonStop.Enabled = false;

                    this.logConsole.AppendText(String.Format("[{0}] Stop capture packet on interface {1}", DateTime.Now, this.device.Interface.FriendlyName));
                    this.logConsole.AppendText(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void sniffing_Proccess()
        {
            try
            {
                // Open the device for capturing
                int readTimeoutMilliseconds = 1000;
                this.device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                this.device.Filter = this.textBoxFilter.Text.Trim();
                this.device.Capture();
            }
            catch (PcapException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Prints the time and length of each received packet
        /// </summary>
        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var ipPacket = (IpPacket)packet.Extract(typeof(IpPacket));
            var tcpPacket = (TcpPacket)packet.Extract(typeof(TcpPacket));

            // start extracting properties for the listview 
            DateTime time = e.Packet.Timeval.Date;
            string time_str = (time.Hour + 1) + ":" + time.Minute + ":" + time.Second + ":" + time.Millisecond;
            string length = e.Packet.Data.Length.ToString();

            Packet hasPacket;
            bool hasPacketNumber = CapturePacketLists.TryGetValue(packetNumber, out hasPacket);
            if (!hasPacketNumber)
            {
                // add to the list
                CapturePacketLists.Add(packetNumber, packet);

                if (ipPacket != null)
                {
                    string source_port = String.Empty;
                    string destination_port = String.Empty;

                    if (tcpPacket != null)
                    {
                        source_port = tcpPacket.SourcePort.ToString();
                        destination_port = tcpPacket.DestinationPort.ToString();
                    }

                    System.Net.IPAddress srcIp = ipPacket.SourceAddress;
                    System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
                    string protocol_type = ipPacket.Protocol.ToString();
                    string sourceIP = String.Format("{0}:{1}", srcIp.ToString(), source_port);
                    string destinationIP = String.Format("{0}:{1}", dstIp.ToString(), destination_port);

                    ListViewItem item = new ListViewItem(packetNumber.ToString());
                    item.SubItems.Add(time_str);
                    item.SubItems.Add(sourceIP);
                    item.SubItems.Add(destinationIP);
                    item.SubItems.Add(protocol_type);
                    item.SubItems.Add(length);

                    Action action = () => listViewPacket.Items.Add(item);
                    listViewPacket.Invoke(action);
                    packetNumber++;

                }
            }
            else
            {
                packetNumber++;
            }
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                PacketDotNet.Packet packet;
                string protocol = e.Item.SubItems[4].Text;
                int key = Int32.Parse(e.Item.SubItems[0].Text);
                bool getPacket = CapturePacketLists.TryGetValue(key, out packet);

                if (getPacket)
                {
                    var ipPacket = (IpPacket)packet.Extract(typeof(IpPacket));
                    this.richTextBoxDetail.Clear();

                    this.richTextBoxHex.Clear();
                    this.richTextBoxHex.AppendText(ipPacket.PrintHex());
                    this.richTextBoxHex.AppendText(Environment.NewLine);

                    switch (protocol)
                    {
                        case "TCP":

                            var tcpPacket = (TcpPacket)packet.Extract(typeof(TcpPacket));
                            int TCPsrcPort = tcpPacket.SourcePort;
                            int TCPdstPort = tcpPacket.DestinationPort;
                            var TCPchecksum = tcpPacket.Checksum;
                            string source = String.Format("{0}:{1}", ipPacket.SourceAddress, TCPsrcPort);
                            string destination = String.Format("{0}:{1}", ipPacket.DestinationAddress, TCPdstPort);

                            this.richTextBoxDetail.AppendText("Packet number: " + key +
                                            " Type: TCP" +
                                            "\r\nSource : " + source +
                                            "\r\nDestination : " + destination +
                                            "\r\nTCP header size: " + tcpPacket.DataOffset +
                                            "\r\nWindow size: " + tcpPacket.WindowSize + // bytes that the receiver is willing to receive
                                            "\r\nChecksum:" + TCPchecksum.ToString() + (tcpPacket.ValidChecksum ? ", valid" : ", invalid") +
                                            "\r\nTCP checksum: " + (tcpPacket.ValidTCPChecksum ? ", valid" : ", invalid") +
                                            "\r\nSequence number: " + tcpPacket.SequenceNumber.ToString() +
                                            "\r\nAcknowledgment number: " + tcpPacket.AcknowledgmentNumber + (tcpPacket.Ack ? ", valid" : ", invalid") +
                                            // flags
                                            "\r\nSynchronization (SYN) flag: " + (tcpPacket.Syn ? "True" : "False") + // first packet from sender
                                            "\r\nAcknowledgement (ACK) flag: " + (tcpPacket.Ack ? "True" : "False") + // indicates if the AcknowledgmentNumber is valid
                                            "\r\nFinish (FIN) flag: " + (tcpPacket.Fin ? "True" : "False") + // finish packet
                                            "\r\nUrgent (URG) flag: " + (tcpPacket.Urg ? "valid" : "invalid") +
                                            "\r\nPush (PSH) flag: " + (tcpPacket.Psh ? "True" : "False") + // push 1 = the receiver should pass the data to the app immidiatly, don't buffer it
                                            "\r\nReset (RST) flag: " + (tcpPacket.Rst ? "True" : "False") + // reset 1 is to abort existing connection
                                                                                                            // SYN indicates the sequence numbers should be synchronized between the sender and receiver to initiate a connection
                                                                                                            // closing the connection with a deal, host_A sends FIN to host_B, B responds with ACK
                                                                                                            // FIN flag indicates the sender is finished sending
                                            "\r\nExplicit Congestion Notification (ECN) flag: " + (tcpPacket.ECN ? "True" : "False") + // send by router or gateway for tell traffic crowded
                                            "\r\nCongestion window reduced (CWR) flag: " + (tcpPacket.CWR ? "True" : "False") + // send by host to router or gateway for tell receive ECN flag
                                            "\r\nNS flag: " + (tcpPacket.NS ? "True" : "False"));
                            break;

                        case "UDP":

                            var udpPacket = (UdpPacket)packet.Extract(typeof(UdpPacket));
                            if (udpPacket != null)
                            {
                                int UDPsrcPort = udpPacket.SourcePort;
                                int UDPdstPort = udpPacket.DestinationPort;
                                var UDPchecksum = udpPacket.Checksum;

                                this.richTextBoxDetail.AppendText("Packet number: " + key +
                                                " Type: UDP" +
                                                "\r\nSource port: " + UDPsrcPort +
                                                "\r\nDestination port: " + UDPdstPort +
                                                "\r\nChecksum: " + UDPchecksum.ToString() + " valid: " + udpPacket.ValidChecksum +
                                                "\r\nValid UDP checksum: " + udpPacket.ValidUDPChecksum);
                            }
                            break;

                        case "ARP":

                            var arpPacket = (ARPPacket)packet.Extract(typeof(ARPPacket));
                            if (arpPacket != null)
                            {
                                System.Net.IPAddress senderAddress = arpPacket.SenderProtocolAddress;
                                System.Net.IPAddress targerAddress = arpPacket.TargetProtocolAddress;
                                System.Net.NetworkInformation.PhysicalAddress senderHardwareAddress = arpPacket.SenderHardwareAddress;
                                System.Net.NetworkInformation.PhysicalAddress targerHardwareAddress = arpPacket.TargetHardwareAddress;

                                this.richTextBoxDetail.AppendText("Packet number: " + key +
                                                " Type: ARP" +
                                                "\r\nHardware address length:" + arpPacket.HardwareAddressLength +
                                                "\r\nProtocol address length: " + arpPacket.ProtocolAddressLength +
                                                "\r\nOperation: " + arpPacket.Operation.ToString() + // ARP request or ARP reply ARP_OP_REQ_CODE, ARP_OP_REP_CODE
                                                "\r\nSender protocol address: " + senderAddress +
                                                "\r\nTarget protocol address: " + targerAddress +
                                                "\r\nSender hardware address: " + senderHardwareAddress +
                                                "\r\nTarget hardware address: " + targerHardwareAddress);
                            }
                            break;

                        case "ICMP":

                            var icmpPacket = (ICMPv4Packet)packet.Extract(typeof(ICMPv4Packet));
                            if (icmpPacket != null)
                            {
                                this.richTextBoxDetail.AppendText("Packet number: " + key +
                                                " Type: ICMP v4" +
                                                "\r\nType Code: 0x" + icmpPacket.TypeCode.ToString("x") +
                                                "\r\nChecksum: " + icmpPacket.Checksum.ToString("x") +
                                                "\r\nID: 0x" + icmpPacket.ID.ToString("x") +
                                                "\r\nSequence number: " + icmpPacket.Sequence.ToString("x"));
                            }
                            break;

                        case "IGMP":

                            if (getPacket)
                            {
                                var igmpPacket = (IGMPv2Packet)packet.Extract(typeof(IGMPv2Packet));
                                if (igmpPacket != null)
                                {
                                    this.richTextBoxDetail.AppendText("Packet number: " + key +
                                                    " Type: IGMP v2" +
                                                    "\r\nType: " + igmpPacket.Type +
                                                    "\r\nGroup address: " + igmpPacket.GroupAddress +
                                                    "\r\nMax response time" + igmpPacket.MaxResponseTime);
                                }
                            }
                            break;
                    }

                    this.richTextBoxDetail.AppendText(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private void buttonFilterHelp_Click(object sender, EventArgs e)
        {
            string message = String.Format(
                "host 172.18.5.4\r\n" +
                "net 192.168.0.0/24\r\n" +
                "net 192.168.0.0 mask 255.255.255.0\r\n" +
                "src net 192.168.0.0/24\r\n" +
                "src net 192.168.0.0 mask 255.255.255.0\r\n" +
                "dst net 192.168.0.0/24\r\n" +
                "dst net 192.168.0.0 mask 255.255.255.0\r\n" +
                "port 53\r\n" +
                "host www.example.com and not (port 80 or port 25)\r\n" +
                "host www.example.com and not port 80 and not port 25\r\n" +
                "port not 53 and not arp\r\n" +
                "tcp portrange 1501-1549\r\n" +
                "ip (IPv4 Only)\r\n" +
                "not broadcast and not multicast\r\n"
            );

            MessageBoxUtils.Information(message, "Filter example");
        }

        /************************************************************************************************************************************************/

        private Dictionary<IPAddress, PhysicalAddress> victimList = new Dictionary<IPAddress, PhysicalAddress>();

        private void netcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.comboBoxInterface.Text))
                {
                    MessageBoxUtils.Warning("กรุณาเลือก Interface");
                    this.tabControl.SelectedTab = this.tabPage2;
                }
                else
                {
                    string[] ipArr = this.treeView.SelectedNode.Text.Split(' ');

                    if (ipArr[0].Split('.')[3].Trim() == "1")
                    {
                        MessageBoxUtils.Warning("Cannot arp sproof gateway");
                    }
                    else
                    {
                        IPAddress newVictim = IPAddress.Parse(ipArr[0].Trim());
                        bool existingVictim = this.victimList.ContainsKey(newVictim);

                        if (!existingVictim)
                        {
                            PhysicalAddress mac = this.NetworkService.GetMacByIP(this.device, newVictim.ToString());
                            mac = this.NetworkService.GetMacByIP(this.device, newVictim.ToString());
                            if (mac != null && this.gatewayMac != null)
                            {
                                this.victimList.Add(newVictim, mac);
                                this.treeView.SelectedNode.ForeColor = Color.Red;
                                MessageBoxUtils.Information(String.Format("Netcut success."));

                                Task.Factory.StartNew(() =>
                                {
                                    this.ARPSproofService.Disconnect(this.victimList, this.device, this.gatewayMac);
                                });
                            }
                            else
                            {
                                MessageBoxUtils.Warning("Get mac address fail!");
                            }
                        }
                        else if (existingVictim && this.victimList.Remove(newVictim))
                        {
                            this.treeView.SelectedNode.ForeColor = Color.Black;
                            MessageBoxUtils.Information(String.Format("Cancel netcut success."));

                            Task.Factory.StartNew(() =>
                            {
                                this.ARPSproofService.Disconnect(this.victimList, this.device, this.gatewayMac);
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this.sniffing != null && this.sniffing.IsAlive)
                {
                    this.sniffing.Abort();
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }


    }
}