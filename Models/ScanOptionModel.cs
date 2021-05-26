using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPScanner.Models
{
    class ScanOptionModel
    {
        public string[] ipAddressFrom { get; set; }

        public string[] ipAddressTo { get; set; }

        public int[] space { get; set; }

        public bool scanIp { get; set; }

        public bool scanHostname { get; set; }

        public int numberOfThread { get; set; }

        public int timeOut { get; set; }
    }
}
