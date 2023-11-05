/*
 * Created by SharpDevelop.
 * User: Jerry
 * Date: 23-10-19
 * Time: 22:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;

namespace FPServer {
	/// <summary>
	/// Description of DeviceWrapper.
	/// </summary>
	public static class DeviceWrapper {
		
		private const string DeviceDll = "D:\\SystemFolder\\Desktop\\JobTask\\IDEMIA\\Src\\Device.c\\FPReader.dll";
		[DllImport(DeviceDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern float version(byte[] strVer) ;
		[DllImport(DeviceDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte[] getBuffer() ;
		[DllImport(DeviceDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern int FPDeviceOpen(byte[] portname, int baudrate, byte parity, byte databit, byte stopbit, int timeout);
		[DllImport(DeviceDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern int FPDeviceClose();
		[DllImport(DeviceDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None)]
		public static extern int getDeviceModuleID(byte[] strID);
		[DllImport(DeviceDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None)]
		public static extern int setLed(int crtl, int color);
		[DllImport(DeviceDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None)]
		public static extern int enroll();
		[DllImport(DeviceDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None)]
		public static extern int match(ref int matchID, ref int matchScore);
		[DllImport(DeviceDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None)]
		public static extern int deleteFp(int delete_id) ;
	}
}
