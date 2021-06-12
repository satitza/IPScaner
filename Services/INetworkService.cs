using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace IPScanner.Services
{
    interface INetworkService
    {
        IPAddress GetGatewayIP(string friendlyname);

        PhysicalAddress GetGatewayMAC(string friendlyname);

        PhysicalAddress GetMacByIP(LibPcapLiveDevice device, string ipAddress);

        string GetMACString(PhysicalAddress physicaladdress);
    }
}
