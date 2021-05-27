using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPScanner.Models;

namespace IPScanner.Services
{
    interface IIPScannerService
    {
        Task<bool> IsIPAddress(string ipAddress);

        Task<ICollection<HostInformationModel>> Scan(ScanOptionModel scanOption);

        Task<ICollection<PortInformationModel>> ScanPort(string ipAddress);
    }
}
