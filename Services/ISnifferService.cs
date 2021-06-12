using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPScanner.Services
{
    interface ISnifferService
    {
        ICollection<LibPcapLiveDevice> GetAllDevice();

        LibPcapLiveDevice GetDeviceByIndex(int index);
    }
}
