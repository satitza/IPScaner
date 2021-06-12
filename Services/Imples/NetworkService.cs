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
                IPAddress gatewayip = GetGatewayIP(friendlyname);
                if (gatewayip != null)
                {
                    LibPcapLiveDeviceList pcapLiveDevices = LibPcapLiveDeviceList.Instance;
                    LibPcapLiveDevice device = pcapLiveDevices.Where(w => w.Interface.FriendlyName == friendlyname).FirstOrDefault();
                    return GetMacByIP(device, gatewayip.ToString());
                }
                else
                {
                    return null;
                }
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
                IPAddress resolveIp = IPAddress.Parse(ipAddress);
                ARP arper = new ARP(device);
                arper.Timeout = TimeSpan.FromMilliseconds(3000);

                // print the resolved address or indicate that none was found
                var resolvedMacAddress = arper.Resolve(resolveIp);
                if (resolvedMacAddress != null)
                {
                    return resolvedMacAddress;
                }
                else
                {
                    return null;
                }
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
        public string GetMACString(PhysicalAddress physicaladdress)
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
