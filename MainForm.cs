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

namespace IPScanner
{
    public partial class MainForm : Form
    {
        private IIPScannerService IPScannerService;

        private ScanOptionModel ScanOption;

        private ICollection<string> listLogs = new List<string>();

        private bool ScanPortState { get; set; } = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.IPScannerService = new IPScannerService(this.listLogs);
                this.ScanOption = new ScanOptionModel();
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
                            this.dataGridView.DataSource = null;
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
                    this.dataGridView.DataSource = null;
                    this.dataGridView.DataSource = await this.IPScannerService.ScanPort(this.treeView.SelectedNode.Text.Trim());

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
                ComponentUtils.DataGridViewCellAutoSize(this.dataGridView);

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
                if (this.devices.Count > 0)
                {

                }
                else
                {
                    foreach (LibPcapLiveDevice device in LibPcapLiveDeviceList.Instance)
                    {
                        if (device.Interface.FriendlyName != null)
                        {
                            this.devices.Add(device);
                        }
                    }

                    foreach (LibPcapLiveDevice deviceName in this.devices)
                    {
                        this.comboBoxInterface.Items.Add(deviceName.Interface.FriendlyName);
                    }
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
                    this.device = this.devices.ToArray()[this.comboBoxInterface.SelectedIndex];

                    //Register our handler function to the 'packet arrival' event
                    this.device.OnPacketArrival +=
                        new PacketArrivalEventHandler(device_OnPacketArrival);

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
                        }
                    }
                    // restart capture
                    else if (this.CaptureProcessStatus)
                    {
                        if (MessageBoxUtils.Question("Restart capture"))
                        {
                            packetNumber = 1;
                            this.listView.Items.Clear();
                            this.CapturePacketLists.Clear();

                            this.sniffing = new Thread(new ThreadStart(sniffing_Proccess));
                            this.sniffing.Start();

                            this.CaptureProcessStatus = true;
                            this.buttonStart.Enabled = false;
                            this.buttonStop.Enabled = true;
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

                    this.buttonStart.Enabled = true;
                    this.buttonStop.Enabled = false;
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

                Action action = () => listView.Items.Add(item);
                listView.Invoke(action);
                ++packetNumber;

            }
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                Packet packet;
                string protocol = e.Item.SubItems[4].Text;
                int key = Int32.Parse(e.Item.SubItems[0].Text);
                bool getPacket = CapturePacketLists.TryGetValue(key, out packet);

                if (getPacket)
                {
                    this.richTextBox.Clear();
                    this.richTextBox.AppendText(packet.PrintHex());
                    this.richTextBox.AppendText(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }
    }
}