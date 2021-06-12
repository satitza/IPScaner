using PacketDotNet;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPScanner.Services.Imples
{
    class SnifferService : ISnifferService
    {
        private ICollection<LibPcapLiveDevice> devices;

        public SnifferService()
        {
            if (this.devices == null)
            {
                this.devices = new List<LibPcapLiveDevice>();
            }
        }

        public ICollection<LibPcapLiveDevice> GetAllDevice()
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

                return this.devices;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public LibPcapLiveDevice GetDeviceByIndex(int index)
        {
            try
            {
                return this.devices.ToArray()[index];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
