/*
 * Created by SharpDevelop.
 * User: Jerry
 * Date: 23-10-26
 * Time: 02:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.ComponentModel;
using System.IO.Ports;

namespace FPServer {
	public class Settings {
		[Category("Data Source")]
		[DisplayName("Source")]
		public EnumSource Source { get; set; }
		
		[Category("Serial")]
		[DisplayName("Port A")]
		public SerialPort commA { get; set; }
		[Category("Serial")]
		[DisplayName("Port B")]
		public SerialPort commB { get; set; }		
		
//		[Category("Serial A")]
//		[DisplayName("PortA Name")]
//		public string commA { get; set; }
//        
//		[Category("Serial A")]
//		[DisplayName("PortA Setting")]
//		public string commASetting { get; set; }
//        
//		[Category("Serial B")]
//		[DisplayName("PortB Name")]
//		public string commB { get; set; }
//        
//		[Category("Serial B")]
//		[DisplayName("PortB Setting")]
//		public string commBSetting { get; set; }
		

	}
	public enum EnumSource {
		DLL = 0,
		Serial,
	};
}