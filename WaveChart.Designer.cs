namespace AudioEditor
{
	partial class WaveChart
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent() {
			this.gDataBar = new System.Windows.Forms.HScrollBar();
			this.gAudioPanel = new AudioEditor.GPanel();
			this.SuspendLayout();
			// 
			// gDataBar
			// 
			this.gDataBar.CausesValidation = false;
			this.gDataBar.Location = new System.Drawing.Point(0, 212);
			this.gDataBar.Name = "gDataBar";
			this.gDataBar.Size = new System.Drawing.Size(390, 17);
			this.gDataBar.TabIndex = 0;
			this.gDataBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HBarScroll);
			// 
			// gAudioPanel
			// 
			this.gAudioPanel.Location = new System.Drawing.Point(0, 28);
			this.gAudioPanel.Name = "gAudioPanel";
			this.gAudioPanel.Size = new System.Drawing.Size(390, 167);
			this.gAudioPanel.TabIndex = 24;
			this.gAudioPanel.Resize += new System.EventHandler(this.LineChartSizeChanged);
			// 
			// WaveChart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Controls.Add(this.gAudioPanel);
			this.Controls.Add(this.gDataBar);
			this.DoubleBuffered = true;
			this.Name = "WaveChart";
			this.Size = new System.Drawing.Size(390, 229);
			this.SizeChanged += new System.EventHandler(this.LineChartSizeChanged);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.LineChartPaint);
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.LineChartMouseWheel);
			this.ResumeLayout(false);

		}
		private GPanel  gAudioPanel;
		private System.Windows.Forms.HScrollBar gDataBar;
	}
}
