using SharpPcap;
using SharpPcap.AirPcap;
using SharpPcap.WinPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
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

        public string GetMacByIP(string friendlyname, string ipAddress)
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
