/*
 * Created by SharpDevelop.
 * User: Gheyret
 * Date: 2021/06/28
 * Time: 17:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AudioEditor
{
	partial class WavForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private WaveChart wavChart;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton ZoomP;
		private System.Windows.Forms.ToolStripButton ZoomM;
		private System.Windows.Forms.ToolStripButton ZoomF;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolPlay;
		private System.Windows.Forms.ToolStripButton toolStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolDel;
		private System.Windows.Forms.ToolStripButton toolSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WavForm));
			this.wavChart = new AudioEditor.WaveChart();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolPlay = new System.Windows.Forms.ToolStripButton();
			this.toolStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ZoomP = new System.Windows.Forms.ToolStripButton();
			this.ZoomM = new System.Windows.Forms.ToolStripButton();
			this.ZoomF = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolDel = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// wavChart
			// 
			this.wavChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.wavChart.Audio = new float[0];
			this.wavChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.wavChart.ChartBackground = System.Drawing.Color.DarkBlue;
			this.wavChart.Location = new System.Drawing.Point(2, 1);
			this.wavChart.Name = "wavChart";
			this.wavChart.SamplingRate = 22050;
			this.wavChart.Size = new System.Drawing.Size(661, 328);
			this.wavChart.TabIndex = 0;
			this.wavChart.Zoom = 1F;
			// 
			// toolStrip1
			// 
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolSave,
			this.toolStripSeparator3,
			this.toolPlay,
			this.toolStop,
			this.toolStripSeparator1,
			this.ZoomP,
			this.ZoomM,
			this.ZoomF,
			this.toolStripSeparator2,
			this.toolDel});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(666, 32);
			this.toolStrip1.TabIndex = 27;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolSave
			// 
			this.toolSave.AutoSize = false;
			this.toolSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSave.Image = ((System.Drawing.Image)(resources.GetObject("toolSave.Image")));
			this.toolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSave.Name = "toolSave";
			this.toolSave.Size = new System.Drawing.Size(32, 32);
			this.toolSave.ToolTipText = "Save";
			this.toolSave.Click += new System.EventHandler(this.ToolSaveClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
			// 
			// toolPlay
			// 
			this.toolPlay.AutoSize = false;
			this.toolPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolPlay.Image = ((System.Drawing.Image)(resources.GetObject("toolPlay.Image")));
			this.toolPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolPlay.Name = "toolPlay";
			this.toolPlay.Size = new System.Drawing.Size(32, 32);
			this.toolPlay.Text = "toolStripButton1";
			this.toolPlay.ToolTipText = "Play";
			this.toolPlay.Click += new System.EventHandler(this.ToolStripButton1Click);
			// 
			// toolStop
			// 
			this.toolStop.AutoSize = false;
			this.toolStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStop.Image")));
			this.toolStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStop.Name = "toolStop";
			this.toolStop.Size = new System.Drawing.Size(32, 32);
			this.toolStop.Text = "toolStripButton2";
			this.toolStop.ToolTipText = "Stop";
			this.toolStop.Click += new System.EventHandler(this.ToolStripButton2Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
			// 
			// ZoomP
			// 
			this.ZoomP.AutoSize = false;
			this.ZoomP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomP.Image = ((System.Drawing.Image)(resources.GetObject("ZoomP.Image")));
			this.ZoomP.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoomP.Name = "ZoomP";
			this.ZoomP.Size = new System.Drawing.Size(32, 32);
			this.ZoomP.Text = "toolStripButton1";
			this.ZoomP.ToolTipText = "Zoom in";
			this.ZoomP.Click += new System.EventHandler(this.ZoomPClick);
			// 
			// ZoomM
			// 
			this.ZoomM.AutoSize = false;
			this.ZoomM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomM.Image = ((System.Drawing.Image)(resources.GetObject("ZoomM.Image")));
			this.ZoomM.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoomM.Name = "ZoomM";
			this.ZoomM.Size = new System.Drawing.Size(32, 32);
			this.ZoomM.Text = "toolStripButton1";
			this.ZoomM.ToolTipText = "Zoom Out";
			this.ZoomM.Click += new System.EventHandler(this.ZoomMClick);
			// 
			// ZoomF
			// 
			this.ZoomF.AutoSize = false;
			this.ZoomF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomF.Image = ((System.Drawing.Image)(resources.GetObject("ZoomF.Image")));
			this.ZoomF.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoomF.Name = "ZoomF";
			this.ZoomF.Size = new System.Drawing.Size(32, 32);
			this.ZoomF.Text = "toolStripButton1";
			this.ZoomF.ToolTipText = "Zoom Fit";
			this.ZoomF.Click += new System.EventHandler(this.ZoomFClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
			// 
			// toolDel
			// 
			this.toolDel.AutoSize = false;
			this.toolDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolDel.Image = ((System.Drawing.Image)(resources.GetObject("toolDel.Image")));
			this.toolDel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolDel.Name = "toolDel";
			this.toolDel.Size = new System.Drawing.Size(32, 32);
			this.toolDel.Text = "toolStripButton1";
			this.toolDel.ToolTipText = "Delete";
			this.toolDel.Click += new System.EventHandler(this.ToolDelClick);
			// 
			// WavForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(666, 332);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.wavChart);
			this.Name = "WavForm";
			this.Text = "Simple Audio Editor";
			this.Shown += new System.EventHandler(this.WavFormShown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.WavFormDragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.WavFormDragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.WavFormPaint);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
