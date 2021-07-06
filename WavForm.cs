/*
 * Created by SharpDevelop.
 * User: Gheyret
 * Date: 2021/06/28
 * Time: 17:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using NWaves.Audio;
using NWaves.Signals;
using NWaves.Audio.Interfaces;
using NWaves.Audio.Mci;

namespace AudioEditor
{
	/// <summary>
	/// Description of WavForm.
	/// </summary>
	public partial class WavForm : Form
	{
		IAudioPlayer gPlayer = new MciAudioPlayer();
		string gFileName = null;
		public WavForm(string filename = null)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			gFileName = filename;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void WavFormShown(object sender, EventArgs e)
		{
			wavChart.Init();
			DiscreteSignal left;
			if(gFileName==null){
				gFileName = Path.Combine(Application.StartupPath, "zam0010012.wav");
			}
			using (var stream = new FileStream(gFileName, FileMode.Open))
			{
				var waveFile = new WaveFile(stream);
				left = waveFile[Channels.Left];
			}
			
			float[] wav = left.Samples;
			wavChart.SamplingRate = left.SamplingRate;
			wavChart.Audio = wav;
		}
		
		void ZoomPClick(object sender, EventArgs e)
		{
			wavChart.ZoomIn();
		}
		void ZoomMClick(object sender, EventArgs e)
		{
			wavChart.ZoomOut();
		}
		
		void ZoomFClick(object sender, EventArgs e)
		{
			wavChart.ZoomFill();
		}

		async void ToolStripButton1Click(object sender, EventArgs e)
		{
			toolPlay.Enabled = false;
			toolStop.Enabled = true;
			DiscreteSignal wav = new DiscreteSignal(wavChart.SamplingRate,wavChart.Audio);
			using (var stream = new FileStream("temp.wav", FileMode.Create))
			{
				var waveFile = new WaveFile(wav);
				waveFile.SaveTo(stream);
			}
			await gPlayer.PlayAsync("temp.wav");
			toolPlay.Enabled = true;
			toolStop.Enabled = false;
		}

		void ToolStripButton2Click(object sender, EventArgs e)
		{
			toolPlay.Enabled = true;
			toolStop.Enabled = false;
			gPlayer.Stop();
		}
		
		void ToolDelClick(object sender, EventArgs e)
		{
			wavChart.Delete();
		}
		
		void WavFormDragEnter(object sender, DragEventArgs e)
		{
			String[] file=(String[])e.Data.GetData(DataFormats.FileDrop);
			if(file[0].EndsWith(".wav",StringComparison.OrdinalIgnoreCase))
			{
				e.Effect= DragDropEffects.All;
			}
		}
		void WavFormDragDrop(object sender, DragEventArgs e)
		{
			String[] file=(String[])e.Data.GetData(DataFormats.FileDrop);
			string 	wavFile=file[0];
			DiscreteSignal left;
			using (var stream = new FileStream(wavFile, FileMode.Open))
			{
				var waveFile = new WaveFile(stream);
				left = waveFile[Channels.Left];
			}
			
			float[] wav = left.Samples;
			wavChart.SamplingRate = left.SamplingRate;
			wavChart.Audio = wav;
		}
		void WavFormPaint(object sender, PaintEventArgs e)
		{
			if(gFileName==null){
				this.Text = "Simple Audio Editor";
			}
			else{
				this.Text = gFileName + " - Simple Audio Editor";
			}
		}
		
		void ToolSaveClick(object sender, EventArgs e)
		{
			DiscreteSignal wav = new DiscreteSignal(wavChart.SamplingRate,wavChart.Audio);
			using (var stream = new FileStream(gFileName, FileMode.Create))
			{
				var waveFile = new WaveFile(wav);
				waveFile.SaveTo(stream);
			}
		}
	}
}
