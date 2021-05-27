using IPScanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPScanner.Services.Imples
{
    class IPScannerService : IIPScannerService
    {

        private ICollection<string> listLogs;

        private Dictionary<int, string> portLists;

        public IPScannerService(ICollection<string> listLogs)
        {
            try
            {
                this.listLogs = listLogs;
                this.portLists = new Dictionary<int, string>();

                // initial port list
                this.portLists.Add(21, "File Transfer Protocol (FTP)");
                this.portLists.Add(22, "Secure Shell (SSH)");
                this.portLists.Add(23, "Telnet protocol");
                this.portLists.Add(25, "Simple Mail Transfer Protocol (SMTP)");
                this.portLists.Add(53, "Domain Name System (DNS)");
                this.portLists.Add(80, "Hypertext Transfer Protocol (HTTP)");
                this.portLists.Add(135, "Microsoft EPMAP (End Point Mapper)");
                this.portLists.Add(443, "Hypertext Transfer Protocol Secure (HTTPS)");
                this.portLists.Add(445, "Microsoft-DS (Directory Services) SMB");
                this.portLists.Add(502, "Modbus Protocol");
                this.portLists.Add(660, "macOS Server administration");
                this.portLists.Add(802, "MODBUS/TCP Security");
                this.portLists.Add(873, "rsync file synchronization protocol");
                this.portLists.Add(902, "VMware ESXi");
                this.portLists.Add(1167, "Cisco IP SLA (Service Assurance Agent)");
                this.portLists.Add(1194, "OpenVPN");
                this.portLists.Add(1433, "Microsoft SQL Server database management system (MSSQL) server");
                this.portLists.Add(1723, "Point-to-Point Tunneling Protocol (PPTP)");
                this.portLists.Add(1812, "RADIUS authentication protocol");
                this.portLists.Add(3260, "iSCSI");
                this.portLists.Add(3306, "MySQL database system");
                this.portLists.Add(3389, "Microsoft Terminal Server (RDP)");
                this.portLists.Add(5432, "PostgreSQL database system");
                this.portLists.Add(5500, "VNC Remote Frame Buffer RFB protocol");
                this.portLists.Add(5938, "TeamViewer remote desktop protocol");
                this.portLists.Add(7025, "Zimbra LMTP [mailbox]—local mail delivery");
                this.portLists.Add(8834, "Nessus, a vulnerability scanner");
                this.portLists.Add(10050, "Zabbix agent");
                this.portLists.Add(10051, "Zabbix trapper");

            }
            catch (Exception ex)
            {
                throw new Exception("Cannot initial ip scan service. " + ex.Message);
            }
        }

        public async Task<bool> IsIPAddress(string ipAddress)
        {
            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    //Match pattern for IP address    
                    string Pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
                    //Regular Expression object    
                    Regex check = new Regex(Pattern);

                    //check to make sure an ip address was provided    
                    if (string.IsNullOrEmpty(ipAddress))
                        //returns false if IP is not provided    
                        return false;
                    else
                        //Matching the pattern    
                        return check.IsMatch(ipAddress, 0);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("{0} is not ip address.{1}", ipAddress, ex.Message));
                }
            });
        }

        public async Task<ICollection<HostInformationModel>> Scan(ScanOptionModel scanOption)
        {
            try
            {
                ICollection<Thread> listOfThread = new List<Thread>();
                ICollection<string> listOfIpAddress = new List<string>();
                ICollection<HostInformationModel> hostAliveList = new List<HostInformationModel>();

                for (int indexPosition1 = 0; indexPosition1 <= scanOption.space[0]; indexPosition1++)
                {
                    int position1 = (int.Parse(scanOption.ipAddressFrom[scanOption.ipAddressFrom.Length - 4]) + indexPosition1);
                    for (int indexPosition2 = 0; indexPosition2 <= scanOption.space[1]; indexPosition2++)
                    {
                        int position2 = (int.Parse(scanOption.ipAddressFrom[scanOption.ipAddressFrom.Length - 3]) + indexPosition2);
                        for (int indexPosition3 = 0; indexPosition3 <= scanOption.space[2]; indexPosition3++)
                        {
                            int position3 = (int.Parse(scanOption.ipAddressFrom[scanOption.ipAddressFrom.Length - 2]) + indexPosition3);
                            for (int indexPosition4 = 0; indexPosition4 <= scanOption.space[3]; indexPosition4++)
                            {
                                int position4 = (int.Parse(scanOption.ipAddressFrom[scanOption.ipAddressFrom.Length - 1]) + indexPosition4);
                                listOfIpAddress.Add(String.Format("{0}.{1}.{2}.{3}", position1, position2, position3, position4));
                            }
                        }
                    }
                }

                return await Task.Factory.StartNew(() =>
                {
                    if (listOfIpAddress.Count < scanOption.numberOfThread)
                    {
                        foreach (string ipAddress in listOfIpAddress)
                        {
                            Thread thread = new Thread(() => this.listLogs.Add(PingHost(ipAddress, scanOption.timeOut, hostAliveList)));
                            thread.Start();
                            listOfThread.Add(thread);
                        }

                        foreach (Thread thread in listOfThread)
                        {
                            thread.Join();
                        }
                    }
                    else
                    {
                        // แบ่ง threading ตามการตั้งค่าที่ option
                        for (int i = 0; i < listOfIpAddress.Count; i = i + scanOption.numberOfThread)
                        {
                            var items = listOfIpAddress.Skip(i).Take(scanOption.numberOfThread);
                            foreach (string ipAddress in items)
                            {
                                Thread thread = new Thread(() => this.listLogs.Add(PingHost(ipAddress, scanOption.timeOut, hostAliveList)));
                                thread.Start();
                                listOfThread.Add(thread);
                            }

                            foreach (Thread thread in listOfThread)
                            {
                                thread.Join();
                            }
                        }
                    }

                    if (scanOption.scanHostname)
                    {
                        listOfThread.Clear();
                        if (hostAliveList.Count < scanOption.numberOfThread)
                        {
                            foreach (HostInformationModel host in hostAliveList)
                            {
                                Thread thread = new Thread(() => this.listLogs.Add(GetHostName(host.IPAddress, host)));
                                thread.Start();
                                listOfThread.Add(thread);
                            }

                            foreach (Thread thread in listOfThread)
                            {
                                thread.Join();
                            }
                        }
                        else
                        {
                            // แบ่ง threading ตามการตั้งค่าที่ option
                            for (int i = 0; i < hostAliveList.Count; i = i + scanOption.numberOfThread)
                            {
                                var items = hostAliveList.Skip(i).Take(scanOption.numberOfThread);
                                foreach (HostInformationModel host in items)
                                {
                                    Thread thread = new Thread(() => this.listLogs.Add(GetHostName(host.IPAddress, host)));
                                    thread.Start();
                                    listOfThread.Add(thread);
                                }

                                foreach (Thread thread in listOfThread)
                                {
                                    thread.Join();
                                }
                            }
                        }
                    }

                    hostAliveList.OrderBy(sort => sort.IPAddress);
                    return hostAliveList;
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string PingHost(object ipAddress, object timeout, object hostAliveList)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(ipAddress.ToString(), int.Parse(timeout.ToString()));

                pingable = reply.Status == IPStatus.Success;
                if (pingable)
                {
                    var results = (ICollection<HostInformationModel>)hostAliveList;
                    results.Add(new HostInformationModel { IPAddress = ipAddress.ToString() });
                }

                return String.Format("[{0}] Ping {1} host alive is {2}", DateTime.Now, ipAddress, pingable);

            }
            catch (PingException ex)
            {
                return String.Format("[{0}] Ping {1} host exception : {2}", DateTime.Now, ipAddress, ex.Message);
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
        }

        private string GetHostName(object ipAddress, object hostAliveList)
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress.ToString());
                if (!String.IsNullOrEmpty(hostEntry.HostName))
                {
                    var host = (HostInformationModel)hostAliveList;
                    host.Hostname = hostEntry.HostName;
                    return String.Format("[{0}] IP Address  {1}   hostname is  {2}   in local network", DateTime.Now, ipAddress, hostEntry.HostName);
                }
                else
                {
                    return String.Format("[{0}] IP Address  {1}  host name not found", DateTime.Now, ipAddress);
                }
            }
            catch (Exception ex)
            {
                return String.Format("[{0}] IP Address  {1}  resolve host name failed  {2} ", DateTime.Now, ipAddress, ex.Message);
            }
        }

        public async Task<ICollection<PortInformationModel>> ScanPort(string ipAddress)
        {
            try
            {
                return await Task.Factory.StartNew(() =>
                {
                    ICollection<PortInformationModel> portOpenLists = new List<PortInformationModel>();

                    if (ipAddress.Split(' ').Length > 0)
                    {
                        ipAddress = ipAddress.Split(' ')[ipAddress.Split(' ').Length - 2];
                    }

                    foreach (KeyValuePair<int, string> entry in this.portLists)
                    {
                        using (TcpClient client = new TcpClient())
                        {
                            var c = client.BeginConnect(IPAddress.Parse(ipAddress), entry.Key, null, null);
                            var success = c.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

                            if (success)
                            {
                                portOpenLists.Add(new PortInformationModel() { PortNumber = entry.Key, PortDetail = entry.Value });
                                this.listLogs.Add(String.Format("[{0}] IP Address  {1}  port  {2}  is open.", DateTime.Now, ipAddress, entry.Key));
                            }
                            else
                            {
                                this.listLogs.Add(String.Format("[{0}] IP Address  {1}  port  {2}  is close.", DateTime.Now, ipAddress, entry.Key));
                            }

                            client.Close();

                        }
                    }

                    return portOpenLists;
                });
            }
            catch (Exception ex)
            {
                this.listLogs.Add(String.Format("[{0}] Scan port ip address  {1}  failed.  {2}", DateTime.Now, ipAddress, ex.Message));
                throw new Exception(String.Format("Scan port ip address  {1}  failed.  {2}", ipAddress, ex.Message));
            }
        }
    }
}
