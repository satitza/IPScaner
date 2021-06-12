using PacketDotNet;
using SharpPcap;
using SharpPcap.AirPcap;
using SharpPcap.LibPcap;
using SharpPcap.WinPcap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPScanner.Services.Imples
{
    class NetworkService : INetworkService
    {
        public IPAddress GetGatewayIP(string friendlyname)
        {
            try
            {
                IPAddress retval = null;
                string interfacename = "";

                foreach (ICaptureDevice capturedevice in CaptureDeviceList.Instance)
                {
                    if (capturedevice is WinPcapDevice)
                    {
                        WinPcapDevice winpcapdevice = (WinPcapDevice)capturedevice;
                        if (winpcapdevice.Interface.FriendlyName == friendlyname)
                        {
                            interfacename = winpcapdevice.Interface.Name;
                        }
                    }
                    else if (capturedevice is AirPcapDevice)
                    {
                        AirPcapDevice airpcapdevice = (AirPcapDevice)capturedevice;
                        if (airpcapdevice.Interface.FriendlyName == friendlyname)
                        {
                            interfacename = airpcapdevice.Interface.Name;
                        }
                    }
                }

                if (interfacename != "")
                {
                    foreach (var networkinterface in NetworkInterface.GetAllNetworkInterfaces())
                    {
                        if (networkinterface.Name == friendlyname)
                        {
                            foreach (var gateway in networkinterface.GetIPProperties().GatewayAddresses)
                            {
                                if (gateway.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) //filter ipv4 gateway ip address
                                    retval = gateway.Address;
                            }
                        }
                    }
                }
                return retval;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PhysicalAddress GetGatewayMAC(string friendlyname)
        {
            try
            {
                string gatewayip = GetGatewayIP(friendlyname).ToString();
                return PhysicalAddress.Parse("");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PhysicalAddress GetMacByIP(LibPcapLiveDevice device, string ipAddress)
        {
            try
            {
                device.Open(DeviceMode.Promiscuous, 1000); //open device with 1000ms timeout
                IPAddress ipV4 = device.Addresses[3].Addr.ipAddress; //possible critical point : Addresses[1] in hardcoding the index for obtaining ipv4 address

                // send arp request
                ARPPacket arprequestpacket = new ARPPacket(ARPOperation.Request, PhysicalAddress.Parse("00-00-00-00-00-00"), IPAddress.Parse(ipAddress), device.MacAddress, ipV4);
                EthernetPacket ethernetpacket = new EthernetPacket(device.MacAddress, PhysicalAddress.Parse("FF-FF-FF-FF-FF-FF"), EthernetPacketType.Arp);
                ethernetpacket.PayloadPacket = arprequestpacket;
                device.SendPacket(ethernetpacket);

                device.Filter = "arp";
                RawCapture rawcapture = null;
                long scanduration = 5000;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while ((rawcapture = device.GetNextPacket()) != null && stopwatch.ElapsedMilliseconds <= scanduration)
                {
                    Packet packet = Packet.ParsePacket(rawcapture.LinkLayerType, rawcapture.Data);
                    ARPPacket arppacket = (ARPPacket)packet.Extract(typeof(ARPPacket));

                    if (arppacket != null)
                    {
                        //return GetMACString(arppacket.SenderHardwareAddress);
                        return arppacket.SenderHardwareAddress;
                    }
                }

                stopwatch.Stop();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Converts a PhysicalAddress to colon delimited string like FF:FF:FF:FF:FF:FF
        /// </summary>
        /// <param name="physicaladdress"></param>
        /// <returns></returns>
        private static string GetMACString(PhysicalAddress physicaladdress)
        {
            try
            {
                string retval = "";
                for (int i = 0; i <= 5; i++)
                    retval += physicaladdress.GetAddressBytes()[i].ToString("X2") + ":";
                return retval.Substring(0, retval.Length - 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
