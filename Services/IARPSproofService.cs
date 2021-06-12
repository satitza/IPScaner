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
    interface IARPSproofService
    {
        void Disconnect(Dictionary<IPAddress, PhysicalAddress> targetlist, LibPcapLiveDevice device, PhysicalAddress gatewayMac);
    }
}
