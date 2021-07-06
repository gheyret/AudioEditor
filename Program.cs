/*
 * Created by SharpDevelop.
 * User: Gheyret
 * Time: 15:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace AudioEditor
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
//			MDFReader rd=new MDFReader();
//			rd.read(@"F:\workspace\NikkiResampler\testdata\OneDG_MultiChn\0028_APS_tanmau_to BATT_&_FRAME_GND_to batt_H_40_M1.mdf");
//			rd.read(@"F:\workspace\NikkiResampler\testdata\一方井\(101)700P TEMCELL 40Vm 20150923 OK DATA .dat");
//			rd.read(@"F:\workspace\NikkiResampler\testdata\bad_MDF\0028_APS_tanmau_to BATT_&_FRAME_GND_to batt_H_40_M1.mdf");
//			rd.read(@"F:\workspace\NikkiResampler\testdata\Data2_taki\20150529_005_Y377失火データ.dat");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new WavForm());
		}
		
	}
}
