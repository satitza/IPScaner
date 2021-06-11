
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_timeout = new System.Windows.Forms.TextBox();
            this.txt_thread = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxHostname = new System.Windows.Forms.CheckBox();
            this.checkBoxIPAddress = new System.Windows.Forms.CheckBox();
            this.btn_scan = new System.Windows.Forms.Button();
            this.txt_to = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_from = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listViewPacket = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.richTextBoxHex = new System.Windows.Forms.RichTextBox();
            this.richTextBoxDetail = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.comboBoxInterface = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.listViewPort = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.logConsole = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scanPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capturePacketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.netcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip.Location = new System.Drawing.Point(0, 599);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1164, 22);
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
            this.panel1.Controls.Add(this.checkBoxHostname);
            this.panel1.Controls.Add(this.checkBoxIPAddress);
            this.panel1.Controls.Add(this.btn_scan);
            this.panel1.Controls.Add(this.txt_to);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_from);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1164, 106);
            this.panel1.TabIndex = 1;
            // 
            // txt_timeout
            // 
            this.txt_timeout.Location = new System.Drawing.Point(481, 40);
            this.txt_timeout.Name = "txt_timeout";
            this.txt_timeout.Size = new System.Drawing.Size(228, 20);
            this.txt_timeout.TabIndex = 11;
            this.txt_timeout.Text = "5000";
            // 
            // txt_thread
            // 
            this.txt_thread.Location = new System.Drawing.Point(481, 15);
            this.txt_thread.Name = "txt_thread";
            this.txt_thread.Size = new System.Drawing.Size(228, 20);
            this.txt_thread.TabIndex = 10;
            this.txt_thread.Text = "100";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Number of Thread :";
            // 
            // checkBoxHostname
            // 
            this.checkBoxHostname.AutoSize = true;
            this.checkBoxHostname.Location = new System.Drawing.Point(795, 43);
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
            this.checkBoxIPAddress.Location = new System.Drawing.Point(795, 17);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1156, 467);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Packet Capture";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 73);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listViewPacket);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1150, 391);
            this.splitContainer2.SplitterDistance = 195;
            this.splitContainer2.TabIndex = 7;
            // 
            // listViewPacket
            // 
            this.listViewPacket.AllowColumnReorder = true;
            this.listViewPacket.AutoArrange = false;
            this.listViewPacket.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewPacket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPacket.FullRowSelect = true;
            this.listViewPacket.GridLines = true;
            this.listViewPacket.HideSelection = false;
            this.listViewPacket.Location = new System.Drawing.Point(0, 0);
            this.listViewPacket.Margin = new System.Windows.Forms.Padding(2);
            this.listViewPacket.MultiSelect = false;
            this.listViewPacket.Name = "listViewPacket";
            this.listViewPacket.Size = new System.Drawing.Size(1150, 195);
            this.listViewPacket.TabIndex = 5;
            this.listViewPacket.UseCompatibleStateImageBehavior = false;
            this.listViewPacket.View = System.Windows.Forms.View.Details;
            this.listViewPacket.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Number";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Time";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Source";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 140;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Destination";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 140;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Protocol";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Length";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 80;
            // 
            // richTextBoxHex
            // 
            this.richTextBoxHex.BackColor = System.Drawing.Color.Blue;
            this.richTextBoxHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxHex.ForeColor = System.Drawing.Color.White;
            this.richTextBoxHex.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxHex.Name = "richTextBoxHex";
            this.richTextBoxHex.Size = new System.Drawing.Size(763, 192);
            this.richTextBoxHex.TabIndex = 7;
            this.richTextBoxHex.Text = "";
            // 
            // richTextBoxDetail
            // 
            this.richTextBoxDetail.BackColor = System.Drawing.Color.Black;
            this.richTextBoxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.richTextBoxDetail.ForeColor = System.Drawing.Color.Lime;
            this.richTextBoxDetail.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxDetail.Name = "richTextBoxDetail";
            this.richTextBoxDetail.Size = new System.Drawing.Size(383, 192);
            this.richTextBoxDetail.TabIndex = 6;
            this.richTextBoxDetail.Text = "";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.textBoxFilter);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.buttonStop);
            this.panel2.Controls.Add(this.buttonStart);
            this.panel2.Controls.Add(this.comboBoxInterface);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1150, 70);
            this.panel2.TabIndex = 0;
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(105, 36);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(320, 20);
            this.textBoxFilter.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(64, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Filter";
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(504, 10);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(70, 23);
            this.buttonStop.TabIndex = 3;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(431, 10);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(70, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonCapture_Click);
            // 
            // comboBoxInterface
            // 
            this.comboBoxInterface.FormattingEnabled = true;
            this.comboBoxInterface.Location = new System.Drawing.Point(105, 11);
            this.comboBoxInterface.Name = "comboBoxInterface";
            this.comboBoxInterface.Size = new System.Drawing.Size(320, 21);
            this.comboBoxInterface.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Choose Interface";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1156, 467);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Result";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewPort);
            this.splitContainer1.Size = new System.Drawing.Size(1150, 461);
            this.splitContainer1.SplitterDistance = 383;
            this.splitContainer1.TabIndex = 5;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(383, 461);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // listViewPort
            // 
            this.listViewPort.AllowColumnReorder = true;
            this.listViewPort.AutoArrange = false;
            this.listViewPort.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader7,
            this.columnHeader8});
            this.listViewPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPort.FullRowSelect = true;
            this.listViewPort.GridLines = true;
            this.listViewPort.HideSelection = false;
            this.listViewPort.Location = new System.Drawing.Point(0, 0);
            this.listViewPort.Margin = new System.Windows.Forms.Padding(2);
            this.listViewPort.MultiSelect = false;
            this.listViewPort.Name = "listViewPort";
            this.listViewPort.Size = new System.Drawing.Size(763, 461);
            this.listViewPort.TabIndex = 6;
            this.listViewPort.UseCompatibleStateImageBehavior = false;
            this.listViewPort.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Number";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "IP Address";
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Port";
            this.columnHeader8.Width = 100;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 106);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1164, 493);
            this.tabControl.TabIndex = 3;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.logConsole);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1156, 467);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // logConsole
            // 
            this.logConsole.BackColor = System.Drawing.Color.Black;
            this.logConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logConsole.ForeColor = System.Drawing.Color.Lime;
            this.logConsole.Location = new System.Drawing.Point(3, 3);
            this.logConsole.Name = "logConsole";
            this.logConsole.Size = new System.Drawing.Size(1150, 461);
            this.logConsole.TabIndex = 2;
            this.logConsole.Text = "";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanPortToolStripMenuItem,
            this.capturePacketToolStripMenuItem,
            this.netcutToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 92);
            // 
            // scanPortToolStripMenuItem
            // 
            this.scanPortToolStripMenuItem.Name = "scanPortToolStripMenuItem";
            this.scanPortToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.scanPortToolStripMenuItem.Text = "Scan Port";
            this.scanPortToolStripMenuItem.Click += new System.EventHandler(this.scanPortToolStripMenuItem_Click);
            // 
            // capturePacketToolStripMenuItem
            // 
            this.capturePacketToolStripMenuItem.Name = "capturePacketToolStripMenuItem";
            this.capturePacketToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.capturePacketToolStripMenuItem.Text = "Capture Packet";
            this.capturePacketToolStripMenuItem.Click += new System.EventHandler(this.capturePacketToolStripMenuItem_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.richTextBoxDetail);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.richTextBoxHex);
            this.splitContainer3.Size = new System.Drawing.Size(1150, 192);
            this.splitContainer3.SplitterDistance = 383;
            this.splitContainer3.TabIndex = 8;
            // 
            // netcutToolStripMenuItem
            // 
            this.netcutToolStripMenuItem.Name = "netcutToolStripMenuItem";
            this.netcutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.netcutToolStripMenuItem.Text = "Netcut";
            this.netcutToolStripMenuItem.Click += new System.EventHandler(this.netcutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 621);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Tools";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_from;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_scan;
        private System.Windows.Forms.CheckBox checkBoxHostname;
        private System.Windows.Forms.CheckBox checkBoxIPAddress;
        private System.Windows.Forms.TextBox txt_timeout;
        private System.Windows.Forms.TextBox txt_thread;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem scanPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capturePacketToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox logConsole;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView listViewPacket;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ComboBox comboBoxInterface;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox richTextBoxDetail;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView listViewPort;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.RichTextBox richTextBoxHex;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStripMenuItem netcutToolStripMenuItem;
    }
}

