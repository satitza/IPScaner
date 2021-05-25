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
                            Thread thread = new Thread(() => PingHost(ipAddress, scanOption.timeOut, hostAliveList));
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
                                Thread thread = new Thread(() => PingHost(ipAddress, scanOption.timeOut, hostAliveList));
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
                                Thread thread = new Thread(() => GetHostName(host.IPAddress, host));
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
                                    Thread thread = new Thread(() => GetHostName(host.IPAddress, host));
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

        private void PingHost(object ipAddress, object timeout, object hostAliveList)
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
            }
            catch (PingException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
        }

        private void GetHostName(object ipAddress, object hostAliveList)
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress.ToString());
                if (!String.IsNullOrEmpty(hostEntry.HostName))
                {
                    var host = (HostInformationModel)hostAliveList;
                    host.Hostname = hostEntry.HostName;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
