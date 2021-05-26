using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPScanner.Utility;
using System.Windows.Forms;
using IPScanner.Models;
using IPScanner.Services;
using IPScanner.Services.Imples;

namespace IPScanner
{
    public partial class MainForm : Form
    {
        private IIPScannerService IPScannerService;

        private ScanOptionModel ScanOption;

        private ICollection<string> listLogs = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.IPScannerService = new IPScannerService(this.listLogs);
                this.ScanOption = new ScanOptionModel();
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
        }

        private async void btn_scan_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.txt_from.Text.Trim()) || String.IsNullOrEmpty(this.txt_to.Text.Trim()))
                {
                    MessageBoxUtils.Warning("กรุณาระบุช่วงของ IP Address");
                }
                else if (!(await this.IPScannerService.IsIPAddress(this.txt_from.Text.Trim())) || !(await this.IPScannerService.IsIPAddress(this.txt_to.Text.Trim())))
                {
                    MessageBoxUtils.Warning("รูปแบบ IP Address ไม่ถูกต้อง");
                }
                else if (!StringUtils.IsNumberOnly(this.txt_thread.Text.Trim()))
                {
                    MessageBoxUtils.Warning("กรุณาระบุจำนวน Thread เป็นตัวเลข");
                    this.txt_thread.Focus();
                }
                else if (!StringUtils.IsNumberOnly(this.txt_timeout.Text.Trim()))
                {
                    MessageBoxUtils.Warning("กรุณาระบุจำนวน Timeout เป็นตัวเลข");
                    this.txt_timeout.Focus();
                }
                else
                {
                    // set scan option
                    this.ScanOption.ipAddressFrom = this.txt_from.Text.Trim().Split('.');
                    this.ScanOption.ipAddressTo = this.txt_to.Text.Trim().Split('.');
                    int[] space = {
                                int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 4]) - int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 4]),
                                int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 3]) - int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 3]),
                                int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 2]) - int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 2]),
                                int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 1]) - int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 1])
                            };
                    this.ScanOption.space = space;
                    this.ScanOption.scanIp = this.checkBoxIPAddress.Checked;
                    this.ScanOption.scanHostname = this.checkBoxHostname.Checked;
                    this.ScanOption.numberOfThread = int.Parse(this.txt_thread.Text.Trim());
                    this.ScanOption.timeOut = int.Parse(this.txt_thread.Text.Trim());

                    if (int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 1]) > int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 1]))
                    {
                        MessageBoxUtils.Warning(String.Format("[ตำแหน่งที่ 4] {0} มากกว่า {1}", this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 1], this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 1]));
                    }
                    else if (int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 2]) > int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 2]))
                    {
                        MessageBoxUtils.Warning(String.Format("[ตำแหน่งที่ 3] {0} มากกว่า {1}", this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 2], this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 2]));
                    }
                    else if (int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 3]) > int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 3]))
                    {
                        MessageBoxUtils.Warning(String.Format("[ตำแหน่งที่ 2] {0} มากกว่า {1}", this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 3], this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 3]));
                    }
                    else if (int.Parse(this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 4]) > int.Parse(this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 4]))
                    {
                        MessageBoxUtils.Warning(String.Format("[ตำแหน่งที่ 1] {0} มากกว่า {1}", this.ScanOption.ipAddressFrom[this.ScanOption.ipAddressFrom.Length - 4], this.ScanOption.ipAddressTo[this.ScanOption.ipAddressTo.Length - 4]));
                    }
                    else
                    {
                        if (MessageBoxUtils.Question("ยืนยันการ Scan IP Address"))
                        {
                            this.btn_scan.Enabled = false;
                            this.btn_scan.Text = "Scanning...";
                            this.dataGridView.DataSource = null;
                            this.toolStripStatusLabel2.Text = "0";

                            /*---------------------------------------------------------------------------------------------------*/

                            this.listLogs.Clear();
                            this.logConsole.Clear();


                            this.logConsole.AppendText(String.Format("[{0}] Start scanning ...", DateTime.Now));
                            this.logConsole.AppendText(Environment.NewLine);


                            ICollection<HostInformationModel> results = await this.IPScannerService.Scan(ScanOption);
                            this.dataGridView.DataSource = results;
                            this.toolStripStatusLabel2.Text = results.Count.ToString();

                            if (this.listLogs.Count > 0)
                            {
                                foreach (string log in listLogs)
                                {
                                    this.logConsole.AppendText(log);
                                    this.logConsole.AppendText(Environment.NewLine);
                                }
                            }

                            this.logConsole.AppendText(String.Format("[{0}] Scan success", DateTime.Now));
                            this.logConsole.AppendText(Environment.NewLine);

                            /*---------------------------------------------------------------------------------------------------*/

                            MessageBoxUtils.Information("Scan IP Address สำเร็จ");
                            this.btn_scan.Enabled = true;
                            this.btn_scan.Text = "Scan";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Error(ex.Message);
            }
            finally
            {
                ComponentUtils.DataGridViewCellAutoSize(this.dataGridView);
            }
        }
    }
}