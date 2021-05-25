
namespace IPScanner
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxPort = new System.Windows.Forms.CheckBox();
            this.checkBoxHostname = new System.Windows.Forms.CheckBox();
            this.checkBoxIPAddress = new System.Windows.Forms.CheckBox();
            this.btn_scan = new System.Windows.Forms.Button();
            this.txt_to = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_from = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_thread = new System.Windows.Forms.TextBox();
            this.txt_timeout = new System.Windows.Forms.TextBox();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip.Location = new System.Drawing.Point(0, 464);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(964, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabel1.Text = "Host Alive :";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel2.Text = "0";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txt_timeout);
            this.panel1.Controls.Add(this.txt_thread);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.checkBoxPort);
            this.panel1.Controls.Add(this.checkBoxHostname);
            this.panel1.Controls.Add(this.checkBoxIPAddress);
            this.panel1.Controls.Add(this.btn_scan);
            this.panel1.Controls.Add(this.txt_to);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_from);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(940, 106);
            this.panel1.TabIndex = 1;
            // 
            // checkBoxPort
            // 
            this.checkBoxPort.AutoSize = true;
            this.checkBoxPort.Location = new System.Drawing.Point(726, 66);
            this.checkBoxPort.Name = "checkBoxPort";
            this.checkBoxPort.Size = new System.Drawing.Size(45, 17);
            this.checkBoxPort.TabIndex = 7;
            this.checkBoxPort.Text = "Port";
            this.checkBoxPort.UseVisualStyleBackColor = true;
            // 
            // checkBoxHostname
            // 
            this.checkBoxHostname.AutoSize = true;
            this.checkBoxHostname.Location = new System.Drawing.Point(726, 43);
            this.checkBoxHostname.Name = "checkBoxHostname";
            this.checkBoxHostname.Size = new System.Drawing.Size(74, 17);
            this.checkBoxHostname.TabIndex = 6;
            this.checkBoxHostname.Text = "Hostname";
            this.checkBoxHostname.UseVisualStyleBackColor = true;
            // 
            // checkBoxIPAddress
            // 
            this.checkBoxIPAddress.AutoSize = true;
            this.checkBoxIPAddress.Checked = true;
            this.checkBoxIPAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIPAddress.Enabled = false;
            this.checkBoxIPAddress.Location = new System.Drawing.Point(726, 17);
            this.checkBoxIPAddress.Name = "checkBoxIPAddress";
            this.checkBoxIPAddress.Size = new System.Drawing.Size(77, 17);
            this.checkBoxIPAddress.TabIndex = 5;
            this.checkBoxIPAddress.Text = "IP Address";
            this.checkBoxIPAddress.UseVisualStyleBackColor = true;
            // 
            // btn_scan
            // 
            this.btn_scan.Location = new System.Drawing.Point(55, 66);
            this.btn_scan.Name = "btn_scan";
            this.btn_scan.Size = new System.Drawing.Size(269, 23);
            this.btn_scan.TabIndex = 4;
            this.btn_scan.Text = "Scan";
            this.btn_scan.UseVisualStyleBackColor = true;
            this.btn_scan.Click += new System.EventHandler(this.btn_scan_Click);
            // 
            // txt_to
            // 
            this.txt_to.Location = new System.Drawing.Point(55, 40);
            this.txt_to.Name = "txt_to";
            this.txt_to.Size = new System.Drawing.Size(269, 20);
            this.txt_to.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To :";
            // 
            // txt_from
            // 
            this.txt_from.Location = new System.Drawing.Point(55, 14);
            this.txt_from.Name = "txt_from";
            this.txt_from.Size = new System.Drawing.Size(269, 20);
            this.txt_from.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From :";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 124);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(940, 337);
            this.dataGridView.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Number of Thread :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Timeout :";
            // 
            // txt_thread
            // 
            this.txt_thread.Location = new System.Drawing.Point(481, 15);
            this.txt_thread.Name = "txt_thread";
            this.txt_thread.Size = new System.Drawing.Size(181, 20);
            this.txt_thread.TabIndex = 10;
            this.txt_thread.Text = "100";
            // 
            // txt_timeout
            // 
            this.txt_timeout.Location = new System.Drawing.Point(481, 40);
            this.txt_timeout.Name = "txt_timeout";
            this.txt_timeout.Size = new System.Drawing.Size(181, 20);
            this.txt_timeout.TabIndex = 11;
            this.txt_timeout.Text = "5000";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 486);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IP Scanner";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox txt_from;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_scan;
        private System.Windows.Forms.CheckBox checkBoxPort;
        private System.Windows.Forms.CheckBox checkBoxHostname;
        private System.Windows.Forms.CheckBox checkBoxIPAddress;
        private System.Windows.Forms.TextBox txt_timeout;
        private System.Windows.Forms.TextBox txt_thread;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

