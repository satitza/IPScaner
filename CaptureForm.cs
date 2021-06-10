using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPScanner.Utility;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace IPScanner
{
    public partial class CaptureForm : Form
    {

        public string IPAddress { get; set; }

        public string filter { get; set; }

        private Thread sniffing;

        private int packetNumber = 1;

        private LibPcapLiveDevice device;

        private ICollection<LibPcapLiveDevice> devices;

        public CaptureForm()
        {
            InitializeComponent();
        }

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("{0} [{1}]", this.IPAddress, Pcap.Version);
            string[] ip = this.IPAddress.Split(' ');
            this.filter = String.Format("host {0}", ip[0]);
            this.devices = new List<LibPcapLiveDevice>();
            this.InitialInterfaceCombobox();
        }

        public void InitialInterfaceCombobox()
        {
            try
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
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
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

                if (MessageBoxUtils.Question("Capture Packet"))
                {
                    this.sniffing = new Thread(new ThreadStart(sniffing_Proccess));
                    this.sniffing.Start();
                }
            }
        }

        private void sniffing_Proccess()
        {
            // Open the device for capturing
            int readTimeoutMilliseconds = 1000;
            this.device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
            //this.device.Filter = this.filter;
            this.device.Capture();
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
                string sourceIP = srcIp.ToString();
                string destinationIP = dstIp.ToString();

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
    }
}
