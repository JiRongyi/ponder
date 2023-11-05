using System;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Net.WebSockets;
using System.IO.Ports;
using System.Text;
using System.Runtime.InteropServices;

namespace FPServer {

	public partial class MainForm : Form {
		int errCode;
		public MainForm() {
			InitializeComponent();
			Control.CheckForIllegalCrossThreadCalls = false;
			LoadProperty();
			WebSocketSvr.wsPort = 1016;
			WebSocketSvr.Listen = true;

			byte[] strVer = new byte[256];
			float ver = DeviceWrapper.version(strVer);
			System.Diagnostics.Debug.WriteLine(Encoding.ASCII.GetString(strVer));
		}
		void MnuExitClick(object sender, EventArgs e) {
			Close();
		}
		private void LoadProperty() {
			CommA.PortName = "com9";
			CommA.BaudRate = 57600;
			CommA.ReadTimeout = 100;
			CommB.PortName = "com1";
			CommB.BaudRate = 57600;
			pgdSetting.SelectedObject = CommA;
			Settings cfg = new Settings();
			cfg.commA = CommA;
			cfg.commB = CommB;
			pgdSetting.SelectedObject = cfg;
		}
		void CommDataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e) {
			SerialPort comm = (SerialPort)sender;
			byte[] data = new byte[comm.BytesToRead];
			comm.Read(data, 0, data.Length);
			showMsg(comm.PortName, "<==", data.Length, Bytes2hexString(data));

			try {
				SerialPort comm2 = (sender == CommA ? CommB : CommA);
				if (comm2.IsOpen) comm2.Write(data, 0, data.Length);
			} catch (Exception ex) {
				showMsg("CommB", "Err.", 0, ex.Message);
			}
		}
		void Run(object sender, EventArgs e) {
			try {
				if (!CommA.IsOpen) CommA.Open();
				showMsg(CommA.PortName, "Open", 0, "");
			} catch (Exception ex) {
				showMsg(CommA.PortName, "Err.", 0, ex.Message);
			}
			try {
				if (!CommB.IsOpen) CommB.Open();
				showMsg(CommB.PortName, "Open", 0, "");
			} catch (Exception ex) {
				showMsg(CommB.PortName, "Err.", 0, ex.Message);
			}
		}
		void Stop(object sender, EventArgs e) {
			if (CommA.IsOpen) CommA.Close();
			if (CommB.IsOpen) CommB.Close();
			showMsg(CommA.PortName, "Close", 0, "");
			showMsg(CommB.PortName, "Close", 0, "");
		}

		void showMsg(string sender, string Types, int Bytes, string Msg) {
			ListViewItem lv;
			if (lvwLog.Items.Count > 0) {
				lv = lvwLog.Items[lvwLog.Items.Count - 1];
				if (sender == lv.SubItems[1].Text && Types == lv.SubItems[2].Text) {
					lv.SubItems[3].Text = (Bytes + Convert.ToInt32(lv.SubItems[3].Text)).ToString();
					lv.SubItems[4].Text = lv.SubItems[4].Text + " " + Msg;
					return;
				}
			}
			lv = lvwLog.Items.Add(System.DateTime.Now.ToString());
			lv.SubItems.Add(sender);
			lv.SubItems.Add(Types);
			lv.SubItems.Add(Bytes.ToString());
			lv.SubItems.Add(Msg);
			lv.EnsureVisible();
		}

		void BtnSendClick(object sender, EventArgs e) {
			if (chkHex.Checked) {
				byte[] data = hexString2Bytes(txtSend.Text);
				SendData(CommA, data);
				SendData(CommB, data);
			} else {
				if (CommA.IsOpen) {
					CommA.Write(txtSend.Text);
					showMsg(CommA.PortName, "==>", txtSend.TextLength, txtSend.Text);
				}
				if (CommB.IsOpen) {
					CommB.Write(txtSend.Text);
					showMsg(CommB.PortName, "==>", txtSend.TextLength, txtSend.Text);
				}
			}
		}
		void SendData(SerialPort comm, byte[] data) {
			if (comm.IsOpen) {
				comm.Write(data, 0, data.Length);
				showMsg(comm.PortName, "==>", data.Length, Bytes2hexString(data));
			}
		}
		void LvwLogDoubleClick(object sender, EventArgs e) {
			txtSend.Text = lvwLog.SelectedItems[0].SubItems[4].Text;
		}

		byte[] hexString2Bytes(string hexString) {
			int p = 0, val = 0, odd = 0;
			byte[] res = new byte[hexString.Length];
			hexString = hexString.ToUpper();
			for (int i = 0; i < hexString.Length; i++) {
				int t = "0123456789ABCDEF".IndexOf(hexString.Substring(i, 1));
				if (odd > 0) {
					res[p++] = (byte)((t < 0) ? val : (val << 4) + t);
					odd = 0;
					val = 0;
				} else if (t >= 0) {
					val = t;
					odd++;
				}
			}
			Array.Resize(ref res, p);
			return res;
		}

		string Bytes2hexString(byte[] data) {
			string[] hex = new string[data.Length];
			for (int i = 0; i < data.Length; i++) {
				hex[i] = data[i].ToString("X2");
			}
			return string.Join(" ", hex);
		}
		void MarkLine(object sender, EventArgs e) {
			showMsg("====", "Mark", 80, new string('=', 80));
		}
		void ClearLog(object sender, EventArgs e) {
			lvwLog.Items.Clear();
		}

		void OpenDeviceToolStripMenuItemClick(object sender, EventArgs e) {
			int res = DeviceWrapper.FPDeviceOpen(Encoding.ASCII.GetBytes("com9"),
				CommA.BaudRate, Convert.ToByte(CommA.Parity), Convert.ToByte(CommA.DataBits),
				Convert.ToByte(CommA.StopBits), CommA.ReadTimeout);
			sbrNotes.Text = res.ToString();
		}
		void GetIDToolStripMenuItemClick(object sender, EventArgs e) {
			byte[] strID = new byte[256];
			if (0 == (errCode = DeviceWrapper.getDeviceModuleID(strID))) {
				sbrNotes.Text = Encoding.ASCII.GetString(strID);
			}
		}
		void EnrollToolStripMenuItemClick(object sender, EventArgs e) {
			DeviceWrapper.enroll();
		}
		void MatchToolStripMenuItemClick(object sender, EventArgs e) {
			int matchID = -1, matchScore = -1;
			if (0 == (errCode = DeviceWrapper.match(ref matchID, ref matchScore))) {
				sbrNotes.Text = matchID.ToString() + " : " + matchScore.ToString();
			}
		}
		void DeleteToolStripMenuItemClick(object sender, EventArgs e) {
			if (0 == (errCode = DeviceWrapper.deleteFp(-1))) {
				sbrNotes.Text = "Deleted ALL ID.";
			}
		}
		void CloseDeviceToolStripMenuItemClick(object sender, EventArgs e) {
			int res = DeviceWrapper.FPDeviceClose();
			sbrNotes.Text = res.ToString();
		}
		void TestLEDToolStripMenuItemClick(object sender, EventArgs e) {
			foreach (int i in new int[] { 1, 3, 4 }) {
				for (int k = 0; k < 8; k++) {
					errCode = DeviceWrapper.setLed(i, k);
					System.Threading.Thread.Sleep(3000);
				}
			}
			errCode = DeviceWrapper.setLed(0, 0);
		}

		private void WebSocketSvr_Handshaked(object sender, AxWebSocketLite.__WebSocketSvr_HandshakedEvent e) {
			WebSocketSvr.SendMsg(e.idxFrom, "Hello.");
		}

		private void WebSocketSvr_GetStaticRes(object sender, AxWebSocketLite.__WebSocketSvr_GetStaticResEvent e) {
			sbrNotes.Text = e.idxFrom.ToString() + " : " + e.uRL;
		}

		private void WebSocketSvr_RecvMsg(object sender, AxWebSocketLite.__WebSocketSvr_RecvMsgEvent e) {
			sbrNotes.Text = e.idxFrom.ToString() + " : " + e.req;
		}

		private void WebSocketSvr_RecvData(object sender, AxWebSocketLite.__WebSocketSvr_RecvDataEvent e) {
			sbrNotes.Text = e.idxFrom.ToString() + " : " + e.req;
		}
	}
}
