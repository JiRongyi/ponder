namespace FPServer {
	partial class MainForm {
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ToolStripButton tbrHelp;
		private System.Windows.Forms.ToolStripButton tbrStop;
		private System.Windows.Forms.ToolStripButton tbrRun;
		private System.Windows.Forms.ToolStripStatusLabel sbrCount;
		private System.Windows.Forms.ToolStripStatusLabel sbrNotes;
		private System.Windows.Forms.ToolStripMenuItem mnuAbout;
		private System.Windows.Forms.ToolStripMenuItem mnuHelp;
		private System.Windows.Forms.ToolStripMenuItem mnuExit;
		private System.Windows.Forms.ToolStripSeparator mnuBar0;
		private System.Windows.Forms.ToolStripMenuItem mnuRun;
		private System.Windows.Forms.ToolStripMenuItem mnuFileMain;
		private System.Windows.Forms.ToolStripSeparator tbrBar0;
		private System.Windows.Forms.SplitContainer splMain;
		private System.Windows.Forms.ImageList imlMain;
		private System.Windows.Forms.TabControl tabMain;
 

		private System.Windows.Forms.StatusStrip sbrMain;
		private System.Windows.Forms.ToolStrip tbrMain;
		private System.Windows.Forms.MenuStrip mnuMain;
		private System.Windows.Forms.ToolStripMenuItem mnuStop;
		private System.IO.Ports.SerialPort CommA;
		private System.IO.Ports.SerialPort CommB;
		private System.Windows.Forms.ListView lvwLog;
		private System.Windows.Forms.ColumnHeader Date; 
		private System.Windows.Forms.ColumnHeader From;
		private System.Windows.Forms.ColumnHeader Type;
		private System.Windows.Forms.ColumnHeader Message;
		private System.Windows.Forms.CheckBox chkHex;
		private System.Windows.Forms.TabPage tabSetting;
		private System.Windows.Forms.PropertyGrid pgdSetting;
		private System.Windows.Forms.TabPage tabCommand;

		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox txtSend;
		private System.Windows.Forms.ToolStripMenuItem markLineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
		private System.Windows.Forms.ColumnHeader Bytes;
		private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openDeviceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem getIDToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enrollToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeDeviceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem testLEDToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;


		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tbrHelp = new System.Windows.Forms.ToolStripButton();
			this.tbrStop = new System.Windows.Forms.ToolStripButton();
			this.tbrRun = new System.Windows.Forms.ToolStripButton();
			this.sbrCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.sbrNotes = new System.Windows.Forms.ToolStripStatusLabel();
			this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuBar0 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuRun = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileMain = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
			this.markLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tbrBar0 = new System.Windows.Forms.ToolStripSeparator();
			this.splMain = new System.Windows.Forms.SplitContainer();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabSetting = new System.Windows.Forms.TabPage();
			this.pgdSetting = new System.Windows.Forms.PropertyGrid();
			this.tabCommand = new System.Windows.Forms.TabPage();
			this.chkHex = new System.Windows.Forms.CheckBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.txtSend = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lvwLog = new System.Windows.Forms.ListView();
			this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.From = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Bytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imlMain = new System.Windows.Forms.ImageList(this.components);
			this.sbrMain = new System.Windows.Forms.StatusStrip();
			this.tbrMain = new System.Windows.Forms.ToolStrip();
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testLEDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CommA = new System.IO.Ports.SerialPort(this.components);
			this.CommB = new System.IO.Ports.SerialPort(this.components);
			this.WebSocketSvr = new AxWebSocketLite.AxWebSocketSvr();
			((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
			this.splMain.Panel1.SuspendLayout();
			this.splMain.Panel2.SuspendLayout();
			this.splMain.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabSetting.SuspendLayout();
			this.tabCommand.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.sbrMain.SuspendLayout();
			this.tbrMain.SuspendLayout();
			this.mnuMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.WebSocketSvr)).BeginInit();
			this.SuspendLayout();
			// 
			// tbrHelp
			// 
			this.tbrHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbrHelp.Image = ((System.Drawing.Image)(resources.GetObject("tbrHelp.Image")));
			this.tbrHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbrHelp.Name = "tbrHelp";
			this.tbrHelp.Size = new System.Drawing.Size(23, 22);
			// 
			// tbrStop
			// 
			this.tbrStop.Image = ((System.Drawing.Image)(resources.GetObject("tbrStop.Image")));
			this.tbrStop.Name = "tbrStop";
			this.tbrStop.Size = new System.Drawing.Size(55, 22);
			this.tbrStop.Text = "Stop";
			this.tbrStop.Click += new System.EventHandler(this.Stop);
			// 
			// tbrRun
			// 
			this.tbrRun.Image = ((System.Drawing.Image)(resources.GetObject("tbrRun.Image")));
			this.tbrRun.Name = "tbrRun";
			this.tbrRun.Size = new System.Drawing.Size(50, 22);
			this.tbrRun.Text = "Run";
			this.tbrRun.Click += new System.EventHandler(this.Run);
			// 
			// sbrCount
			// 
			this.sbrCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.sbrCount.Name = "sbrCount";
			this.sbrCount.Size = new System.Drawing.Size(0, 17);
			// 
			// sbrNotes
			// 
			this.sbrNotes.Name = "sbrNotes";
			this.sbrNotes.Size = new System.Drawing.Size(1129, 17);
			this.sbrNotes.Spring = true;
			this.sbrNotes.Text = "Ready";
			this.sbrNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mnuAbout
			// 
			this.mnuAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnuAbout.Image")));
			this.mnuAbout.Name = "mnuAbout";
			this.mnuAbout.Size = new System.Drawing.Size(111, 22);
			this.mnuAbout.Text = "&About";
			// 
			// mnuHelp
			// 
			this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
			this.mnuHelp.Name = "mnuHelp";
			this.mnuHelp.Size = new System.Drawing.Size(47, 21);
			this.mnuHelp.Text = "&Help";
			// 
			// mnuExit
			// 
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.mnuExit.Size = new System.Drawing.Size(183, 22);
			this.mnuExit.Text = "E&xit";
			this.mnuExit.Click += new System.EventHandler(this.MnuExitClick);
			// 
			// mnuBar0
			// 
			this.mnuBar0.Name = "mnuBar0";
			this.mnuBar0.Size = new System.Drawing.Size(180, 6);
			// 
			// mnuRun
			// 
			this.mnuRun.Image = ((System.Drawing.Image)(resources.GetObject("mnuRun.Image")));
			this.mnuRun.Name = "mnuRun";
			this.mnuRun.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.mnuRun.Size = new System.Drawing.Size(183, 22);
			this.mnuRun.Text = "&Run";
			this.mnuRun.Click += new System.EventHandler(this.Run);
			// 
			// mnuFileMain
			// 
			this.mnuFileMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRun,
            this.mnuStop,
            this.markLineToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.mnuBar0,
            this.mnuExit});
			this.mnuFileMain.Name = "mnuFileMain";
			this.mnuFileMain.Size = new System.Drawing.Size(39, 21);
			this.mnuFileMain.Text = "&File";
			// 
			// mnuStop
			// 
			this.mnuStop.Image = ((System.Drawing.Image)(resources.GetObject("mnuStop.Image")));
			this.mnuStop.Name = "mnuStop";
			this.mnuStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuStop.Size = new System.Drawing.Size(183, 22);
			this.mnuStop.Text = "&Stop";
			this.mnuStop.Click += new System.EventHandler(this.Stop);
			// 
			// markLineToolStripMenuItem
			// 
			this.markLineToolStripMenuItem.Name = "markLineToolStripMenuItem";
			this.markLineToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
			this.markLineToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.markLineToolStripMenuItem.Text = "&Mark Line";
			this.markLineToolStripMenuItem.Click += new System.EventHandler(this.MarkLine);
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.clearToolStripMenuItem.Text = "&Clear";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearLog);
			// 
			// tbrBar0
			// 
			this.tbrBar0.Name = "tbrBar0";
			this.tbrBar0.Size = new System.Drawing.Size(6, 25);
			// 
			// splMain
			// 
			this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splMain.Location = new System.Drawing.Point(0, 50);
			this.splMain.Name = "splMain";
			// 
			// splMain.Panel1
			// 
			this.splMain.Panel1.Controls.Add(this.tabMain);
			this.splMain.Panel1MinSize = 240;
			// 
			// splMain.Panel2
			// 
			this.splMain.Panel2.Controls.Add(this.splitContainer1);
			this.splMain.Size = new System.Drawing.Size(1144, 473);
			this.splMain.SplitterDistance = 240;
			this.splMain.TabIndex = 5;
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tabSetting);
			this.tabMain.Controls.Add(this.tabCommand);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.ItemSize = new System.Drawing.Size(60, 18);
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(240, 473);
			this.tabMain.TabIndex = 1;
			// 
			// tabSetting
			// 
			this.tabSetting.Controls.Add(this.pgdSetting);
			this.tabSetting.Location = new System.Drawing.Point(4, 22);
			this.tabSetting.Name = "tabSetting";
			this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
			this.tabSetting.Size = new System.Drawing.Size(232, 447);
			this.tabSetting.TabIndex = 0;
			this.tabSetting.Text = "Setting";
			this.tabSetting.UseVisualStyleBackColor = true;
			// 
			// pgdSetting
			// 
			this.pgdSetting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgdSetting.Location = new System.Drawing.Point(3, 3);
			this.pgdSetting.Name = "pgdSetting";
			this.pgdSetting.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.pgdSetting.Size = new System.Drawing.Size(226, 441);
			this.pgdSetting.TabIndex = 1;
			// 
			// tabCommand
			// 
			this.tabCommand.Controls.Add(this.chkHex);
			this.tabCommand.Controls.Add(this.btnSend);
			this.tabCommand.Controls.Add(this.txtSend);
			this.tabCommand.Location = new System.Drawing.Point(4, 22);
			this.tabCommand.Name = "tabCommand";
			this.tabCommand.Padding = new System.Windows.Forms.Padding(3);
			this.tabCommand.Size = new System.Drawing.Size(232, 447);
			this.tabCommand.TabIndex = 1;
			this.tabCommand.Text = "Command";
			this.tabCommand.UseVisualStyleBackColor = true;
			// 
			// chkHex
			// 
			this.chkHex.Checked = true;
			this.chkHex.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkHex.Location = new System.Drawing.Point(8, 194);
			this.chkHex.Name = "chkHex";
			this.chkHex.Size = new System.Drawing.Size(72, 26);
			this.chkHex.TabIndex = 2;
			this.chkHex.Text = "Hex";
			this.chkHex.UseVisualStyleBackColor = true;
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(133, 194);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(93, 26);
			this.btnSend.TabIndex = 1;
			this.btnSend.Text = "&Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.BtnSendClick);
			// 
			// txtSend
			// 
			this.txtSend.Location = new System.Drawing.Point(8, 6);
			this.txtSend.Multiline = true;
			this.txtSend.Name = "txtSend";
			this.txtSend.Size = new System.Drawing.Size(218, 170);
			this.txtSend.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lvwLog);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.WebSocketSvr);
			this.splitContainer1.Size = new System.Drawing.Size(900, 473);
			this.splitContainer1.SplitterDistance = 300;
			this.splitContainer1.TabIndex = 0;
			// 
			// lvwLog
			// 
			this.lvwLog.AllowColumnReorder = true;
			this.lvwLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Date,
            this.From,
            this.Type,
            this.Bytes,
            this.Message});
			this.lvwLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwLog.FullRowSelect = true;
			this.lvwLog.HideSelection = false;
			this.lvwLog.Location = new System.Drawing.Point(0, 0);
			this.lvwLog.MultiSelect = false;
			this.lvwLog.Name = "lvwLog";
			this.lvwLog.Size = new System.Drawing.Size(900, 300);
			this.lvwLog.TabIndex = 1;
			this.lvwLog.UseCompatibleStateImageBehavior = false;
			this.lvwLog.View = System.Windows.Forms.View.Details;
			// 
			// Date
			// 
			this.Date.Text = "Date";
			this.Date.Width = 120;
			// 
			// From
			// 
			this.From.Text = "From";
			this.From.Width = 80;
			// 
			// Type
			// 
			this.Type.Text = "Type";
			this.Type.Width = 80;
			// 
			// Bytes
			// 
			this.Bytes.Text = "Bys";
			this.Bytes.Width = 30;
			// 
			// Message
			// 
			this.Message.Text = "Message";
			this.Message.Width = 600;
			// 
			// imlMain
			// 
			this.imlMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imlMain.ImageSize = new System.Drawing.Size(16, 16);
			this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// sbrMain
			// 
			this.sbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbrNotes,
            this.sbrCount});
			this.sbrMain.Location = new System.Drawing.Point(0, 523);
			this.sbrMain.Name = "sbrMain";
			this.sbrMain.Size = new System.Drawing.Size(1144, 22);
			this.sbrMain.TabIndex = 7;
			// 
			// tbrMain
			// 
			this.tbrMain.BackColor = System.Drawing.Color.Transparent;
			this.tbrMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrRun,
            this.tbrStop,
            this.tbrBar0,
            this.tbrHelp});
			this.tbrMain.Location = new System.Drawing.Point(0, 25);
			this.tbrMain.Name = "tbrMain";
			this.tbrMain.Size = new System.Drawing.Size(1144, 25);
			this.tbrMain.Stretch = true;
			this.tbrMain.TabIndex = 8;
			this.tbrMain.Text = "tbrMain";
			// 
			// mnuMain
			// 
			this.mnuMain.BackColor = System.Drawing.Color.Transparent;
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileMain,
            this.actionToolStripMenuItem,
            this.mnuHelp});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(1144, 25);
			this.mnuMain.TabIndex = 6;
			this.mnuMain.Text = "menuStrip1";
			// 
			// actionToolStripMenuItem
			// 
			this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDeviceToolStripMenuItem,
            this.getIDToolStripMenuItem,
            this.testLEDToolStripMenuItem,
            this.enrollToolStripMenuItem,
            this.matchToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.closeDeviceToolStripMenuItem});
			this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
			this.actionToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
			this.actionToolStripMenuItem.Text = "&Action";
			// 
			// openDeviceToolStripMenuItem
			// 
			this.openDeviceToolStripMenuItem.Name = "openDeviceToolStripMenuItem";
			this.openDeviceToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.openDeviceToolStripMenuItem.Text = "Open Device";
			this.openDeviceToolStripMenuItem.Click += new System.EventHandler(this.OpenDeviceToolStripMenuItemClick);
			// 
			// getIDToolStripMenuItem
			// 
			this.getIDToolStripMenuItem.Name = "getIDToolStripMenuItem";
			this.getIDToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.getIDToolStripMenuItem.Text = "Get ID";
			this.getIDToolStripMenuItem.Click += new System.EventHandler(this.GetIDToolStripMenuItemClick);
			// 
			// testLEDToolStripMenuItem
			// 
			this.testLEDToolStripMenuItem.Name = "testLEDToolStripMenuItem";
			this.testLEDToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.testLEDToolStripMenuItem.Text = "Test LED";
			this.testLEDToolStripMenuItem.Click += new System.EventHandler(this.TestLEDToolStripMenuItemClick);
			// 
			// enrollToolStripMenuItem
			// 
			this.enrollToolStripMenuItem.Name = "enrollToolStripMenuItem";
			this.enrollToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.enrollToolStripMenuItem.Text = "Enroll";
			this.enrollToolStripMenuItem.Click += new System.EventHandler(this.EnrollToolStripMenuItemClick);
			// 
			// matchToolStripMenuItem
			// 
			this.matchToolStripMenuItem.Name = "matchToolStripMenuItem";
			this.matchToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.matchToolStripMenuItem.Text = "Match";
			this.matchToolStripMenuItem.Click += new System.EventHandler(this.MatchToolStripMenuItemClick);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
			// 
			// closeDeviceToolStripMenuItem
			// 
			this.closeDeviceToolStripMenuItem.Name = "closeDeviceToolStripMenuItem";
			this.closeDeviceToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.closeDeviceToolStripMenuItem.Text = "Close Device";
			this.closeDeviceToolStripMenuItem.Click += new System.EventHandler(this.CloseDeviceToolStripMenuItemClick);
			// 
			// CommA
			// 
			this.CommA.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CommDataReceived);
			// 
			// CommB
			// 
			this.CommB.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CommDataReceived);
			// 
			// WebSocketSvr
			// 
			this.WebSocketSvr.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WebSocketSvr.Enabled = true;
			this.WebSocketSvr.Location = new System.Drawing.Point(0, 0);
			this.WebSocketSvr.Name = "WebSocketSvr";
			this.WebSocketSvr.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WebSocketSvr.OcxState")));
			this.WebSocketSvr.Size = new System.Drawing.Size(900, 169);
			this.WebSocketSvr.TabIndex = 1;
			this.WebSocketSvr.Handshaked += new AxWebSocketLite.@__WebSocketSvr_HandshakedEventHandler(this.WebSocketSvr_Handshaked);
			this.WebSocketSvr.RecvData += new AxWebSocketLite.@__WebSocketSvr_RecvDataEventHandler(this.WebSocketSvr_RecvData);
			this.WebSocketSvr.RecvMsg += new AxWebSocketLite.@__WebSocketSvr_RecvMsgEventHandler(this.WebSocketSvr_RecvMsg);
			this.WebSocketSvr.GetStaticRes += new AxWebSocketLite.@__WebSocketSvr_GetStaticResEventHandler(this.WebSocketSvr_GetStaticRes);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1144, 545);
			this.Controls.Add(this.splMain);
			this.Controls.Add(this.sbrMain);
			this.Controls.Add(this.tbrMain);
			this.Controls.Add(this.mnuMain);
			this.Name = "MainForm";
			this.Text = "FPServer";
			this.splMain.Panel1.ResumeLayout(false);
			this.splMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
			this.splMain.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tabSetting.ResumeLayout(false);
			this.tabCommand.ResumeLayout(false);
			this.tabCommand.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.sbrMain.ResumeLayout(false);
			this.sbrMain.PerformLayout();
			this.tbrMain.ResumeLayout(false);
			this.tbrMain.PerformLayout();
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.WebSocketSvr)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private AxWebSocketLite.AxWebSocketSvr WebSocketSvr;
	}
}


