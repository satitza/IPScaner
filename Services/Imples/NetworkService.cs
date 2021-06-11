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
                return null;
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
                return null;
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
