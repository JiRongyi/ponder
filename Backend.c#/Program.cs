/*
 * Created by SharpDevelop.
 * User: Jerry
 * Date: 2020/11/4
 * Time: 3:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace FPServer {
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program {
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args) {
            var wsFPS = new FPSWebSocket("http://localhost:1016/");
            wsFPS.Start();

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());

		}
		static async Task Main() {
			var wsFPS= new FPSWebSocket("http://localhost:1016/");
			await wsFPS.Start();
		}
	}
}
