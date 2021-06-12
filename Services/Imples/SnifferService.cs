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

            this.GetAllDevice();
        }

        public ICollection<LibPcapLiveDevice> GetAllDevice()
        {
            try
            {
                LibPcapLiveDeviceList pcapLiveDevices = LibPcapLiveDeviceList.Instance;
                pcapLiveDevices.Refresh();

                if (this.devices.Count > 0)
                {
                    this.devices.Clear();
                }

                foreach (LibPcapLiveDevice device in pcapLiveDevices)
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
