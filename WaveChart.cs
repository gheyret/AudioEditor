/*
 * Created by SharpDevelop.
 * User: gheyret
 * Time: 8:36
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AudioEditor
{
	public partial class WaveChart : UserControl
	{
		private SolidBrush gbackBrush=new SolidBrush(Color.Black);
		// Keep only a maximum MAX_VALUE_COUNT amount of values; This will allow
		private int MAX_VALUE_COUNT = 512;
		private float xZoom = 1.0f;
		private int yBottomPos=10;
		const int    yTopPos=5;
		private int _height;
		private int _width;
		public  bool needPrecal=false;
		private Pen  katek=new Pen(Color.FromArgb(50,50,50),0.1f);
		private Pen  xOq=new Pen(Color.Cyan);
		private Brush strTm=new SolidBrush(Color.Cyan);
		private int  gStIndex=0;
		const int    gcntXblok=10;
		private int  gViewStIndex=0;
		private int  gViewEndIndex=0;
		private Pen  gViewLineStPen=new Pen(Color.Red);
		private Pen  gViewLineEndPen=new Pen(Color.HotPink);
		private bool gIspainting=false;
		private bool gMouseDown=false;
		private int  gStEnd=-1;  // Mouse besilghanda qaysigha yeqin
		private bool gspDisOK=false;
		private int[] gTmShosPosX=new int[gcntXblok];
		private int   gTmShowPosY;
		
		
		const float       gWavMin= -1.0f;
		const float       gWavMax= 1.0f;
		List<float> gWavDataBuf= new List<float>();
		Pen         gWavePen = new Pen(Color.Yellow,1.0f);
		
		public WaveChart() {
			InitializeComponent();
			this.gAudioPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.LineChartPaint);
			this.gAudioPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataPanelMouseMove);
			this.gAudioPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataPanelMouseDown);
			this.gAudioPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DataPanelMouseUp);
			
			Init();
			gDataBar.LargeChange=5;
			gDataBar.SmallChange=1;
			gViewLineEndPen.DashStyle= DashStyle.Dash;
		}
		
		public Color ChartBackground{
			set{
				this.gbackBrush=new SolidBrush(value);
				if(Color.White.Equals(value)){
					katek=new Pen(Color.LightGray,0.1f);
				}
//				katek=new Pen(Color.FromArgb(255-value.B, 255-value.G,255-value.B),0.1f);
				this.needPrecal=true;
			}
			get{
				return gbackBrush.Color;
			}
		}
		
		public int SamplingRate{
			get;
			set;
		}
		
		public void Init()
		{
			this.gViewEndIndex=-1;
			this.gViewStIndex=-1;
			gAudioPanel.Cursor=System.Windows.Forms.Cursors.Default;
			needPrecal=true;
			yBottomPos=gDataBar.Height;
			LineChartSizeChanged(null,null);
		}
		
		/// <summary>
		/// Clears the whole chart
		/// </summary>
		public void Clear() {
			gWavDataBuf.Clear();
			needPrecal=true;
			gAudioPanel.Invalidate();
		}
		
		
		public float[] Audio
		{
			set{
				Clear();
				gWavDataBuf.AddRange(value);
				gStIndex=0;
				if(gWavDataBuf.Count>0){
					Zoom=(float)_width/(float)gWavDataBuf.Count;
					Invoke((MethodInvoker)delegate(){
					       	gDataBar.Maximum=gWavDataBuf.Count;
					       	gDataBar.Value=gStIndex;
					       	//gDataBar.SmallChange=(int)(0.5f*gWavDataBuf.Count/(gWavDataBuf[gWavDataBuf.Count-1]));
					       	//gDataBar.LargeChange=(int)(0.5f*gWavDataBuf.Count/(gWavDataBuf[gWavDataBuf.Count-1]));
					       });
				}
				else{
					Zoom = 1.0f;
				}
				
			}
			get{
				return gWavDataBuf.ToArray();
			}
		}
		
		
		/// <summary>
		/// Calculates the vertical Position of a value in relation the chart size,
		/// Scale Mode and, if ScaleMode is Relative, to the current maximum value
		/// </summary>
		/// <param name="value">performance value</param>
		/// <returns>vertical Point position in Pixels</returns>
		private float CalcVerticalPosition(float val) {
			float result =0;
			result=(val-gWavMin);
			result = result * _height/ (gWavMax-gWavMin);
			result = (_height - result+3);
			return result;
		}

		void LineChartPaint(object sender, PaintEventArgs e)
		{
			if(gIspainting) return;
			gIspainting=true;
			try{
				Rectangle baseRectangle = e.ClipRectangle;
				e.Graphics.CompositingQuality=System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
				e.Graphics.PixelOffsetMode=System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
				e.Graphics.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
				e.Graphics.InterpolationMode=System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
				e.Graphics.FillRectangle(gbackBrush, baseRectangle);
				if(needPrecal==true)
				{
					PreCalculate(e.Graphics);
				}
				
				DrawKatek(e.Graphics);
				if(gWavDataBuf.Count>0)
				{
					DrawChart(e.Graphics);
				}
				if(gViewStIndex<gWavDataBuf.Count && gViewEndIndex<gWavDataBuf.Count)
				{
					DrawViewLine(e.Graphics);
				}
			}
			catch(Exception){
			}
			gIspainting=false;
		}
		
		private void DrawViewLine(Graphics g)
		{
			int y1=0;
			int y2=gAudioPanel.Height;
			int count=gWavDataBuf.Count;
			PointF[] prePoint=new PointF[1];
			PointF[] curPoint=new PointF[1];
			int St=-1;
			int End=-1;
			prePoint[0].X=0;

			for (int i =gStIndex+1; i < count; i++) {
				curPoint[0].X = prePoint[0].X +xZoom;
				if(i==gViewStIndex){
					St=(int)curPoint[0].X;
				}
				if(i==gViewEndIndex){
					End=(int)curPoint[0].X;
				}
				if(St!=-1 && End!=-1){
					break;
				}
				prePoint[0] = curPoint[0];
			}
			if(St!=-1){
				g.DrawLine(gViewLineStPen,St,y1,St,y2);
			}
			if(End!=-1){
				g.DrawLine(gViewLineEndPen,End,y1,End,y2);
			}
		}
		
		
		/// <summary>
		/// Draws the chart (w/o background or grid, but with border) to the Graphics canvas
		/// </summary>
		/// <param name="g">Graphics</param>
		private void DrawChart(Graphics g) {
			int count=gWavDataBuf.Count;
			int    tmposIndx=0;
			int    tmOffx =0;
			float  xx;
			
			string stm=(gStIndex*1.0/SamplingRate).ToString("0.0");
			
			PointF[] previousPoint=new PointF[1];
			PointF[] currentPoint=new PointF[1];
			if(count==0) return;
			for(int i=0;i<1;i++)
			{
				previousPoint[i].X=0;
				previousPoint[i].Y=CalcVerticalPosition(gWavDataBuf[gStIndex]);
			}

			g.DrawString(stm,Font,this.strTm,0,gTmShowPosY);
			
			// Connect all visible values with lines
			for (int i =gStIndex+1; i < count; i++) {
				xx=previousPoint[0].X +xZoom;
				currentPoint[0].X = xx;
				currentPoint[0].Y = CalcVerticalPosition(gWavDataBuf[i]);
				g.DrawLine(gWavePen, previousPoint[0], currentPoint[0]);
				previousPoint[0] = currentPoint[0];
				
				if(tmposIndx<gcntXblok && (int)previousPoint[0].X>=gTmShosPosX[tmposIndx])
				{
					stm=(1.0*i/SamplingRate).ToString("0.0");
					tmOffx=(int)g.MeasureString(stm,this.Font).Width;
					g.DrawString(stm,Font,this.strTm,gTmShosPosX[tmposIndx]-tmOffx+5,gTmShowPosY);
					tmposIndx++;
				}
				
				if(currentPoint[0].X>=_width){
					break;
				}
			}
		}
		
		
		private void DrawKatek(Graphics g)
		{
			float   stx;
			float   endx;
			float   sty=0;
			float   curval=0.0f;
			float   ypos;
			int     fh=this.Font.Height/4;
			float   inc=0;
			
			stx=0;
			endx=stx;
			endx=0;
			inc=(float)_width/(float)gcntXblok;
			sty=CalcVerticalPosition(gWavMin);
			
			gTmShowPosY=(int)(sty+3);
			
			for(int i=0;i<gcntXblok;i++)
			{
				endx+=inc;
				g.DrawLine(katek,endx,sty,endx,yTopPos);
				g.DrawLine(xOq,endx,sty,endx,sty-3); //X Oqining Rezmisini sizidu
				gTmShosPosX[i]=(int)endx;
			}
			
			gTmShosPosX[gcntXblok-1]-=2;
			
			g.DrawLine(xOq,0,sty,_width,sty); //X Oqini Sizidu
			
			inc=(gWavMax-gWavMin)/10.0f;
			curval=gWavMin;
			for(int i=0;i<10;i++)
			{
				curval+=inc;
				ypos=CalcVerticalPosition(curval);
				g.DrawLine(katek,0,ypos,_width,ypos);
			}
		}
		
		private void PreCalculate(Graphics g)
		{
			StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
			MAX_VALUE_COUNT=(int)Math.Round(_width/Zoom);
			if(MAX_VALUE_COUNT<0){
				MAX_VALUE_COUNT=2;
			}
			int st=gWavDataBuf.Count-MAX_VALUE_COUNT;
			if(st<=0){
				gStIndex=0;
				gDataBar.Maximum=0;
				gDataBar.Value=0;
			}
			else
			{
				gDataBar.Maximum=st+5;
			}
			needPrecal=false;
			if(gspDisOK==false)
			{
				gspDisOK=true;
			}
		}
		
		void HBarScroll(object sender, ScrollEventArgs e)
		{
			if(e.Type==ScrollEventType.EndScroll) return;
			if(e.Type== ScrollEventType.SmallIncrement && gStIndex>e.NewValue) return;
			gStIndex=this.gDataBar.Value;
			if(e.Type== ScrollEventType.SmallIncrement && e.OldValue==e.NewValue){
				gStIndex=gDataBar.Maximum;
			}
			gAudioPanel.Invalidate();
		}
		
		void LineChartSizeChanged(object sender, EventArgs e)
		{
			needPrecal=true;
			gAudioPanel.Location=new Point(0,0);
			gAudioPanel.Size=new Size(this.Size.Width,this.Size.Height-this.gDataBar.Height);
			gAudioPanel.Visible=true;

			gDataBar.Left=gAudioPanel.Left;
			gDataBar.Top=gAudioPanel.Bottom;
			gDataBar.Width=gAudioPanel.Size.Width;
			_height=gAudioPanel.Height-(Font.Height+10);
			_width=gAudioPanel.Width-5;
		}
		
		private int GetIndexFromMouseX(int mouseX)
		{
			int index=-1;
			int count=gWavDataBuf.Count;
			if(count==0) return index;
			PointF[] previousPoint=new PointF[1];
			PointF[] currentPoint=new PointF[1];
			previousPoint[0].X=0;
			for (int i =gStIndex+1; i < count; i++) {
				currentPoint[0].X = previousPoint[0].X +Zoom;
				if(mouseX>=previousPoint[0].X && mouseX<currentPoint[0].X){
					index=i;
					break;
				}
				previousPoint[0] = currentPoint[0];
			}
			if(index==-1)
			{
				if(mouseX<Zoom){
					index=0;
				}
				else if(mouseX>=currentPoint[0].X){
					index=count-1;
				}
			}
			return index;
		}
		
		void DataPanelMouseMove(object sender, MouseEventArgs e)
		{
			int index;
			int deltaSt;
			int deltaEnd;
			if(gViewStIndex!=-1 && gViewEndIndex!=-1)
			{
				if(gMouseDown==true)
				{
					index=GetIndexFromMouseX(e.X);
					if(index>=0 && index<gWavDataBuf.Count)
					{
						if(gStEnd==1)
						{
							gViewStIndex=index;
						}
						else if(gStEnd==2)
						{
							gViewEndIndex=index;
						}
						gAudioPanel.Invalidate();
					}
				}
				else
				{
					index=GetIndexFromMouseX(e.X);
					deltaSt=Math.Abs(index-gViewStIndex);
					deltaEnd=Math.Abs(index-gViewEndIndex);
					
//					System.Diagnostics.Debug.WriteLine("index =" + index+ "  St ="+gViewStIndex+ " End= "+ gViewEndIndex);
					
					if(deltaSt<(6/Zoom))
					{
						gAudioPanel.Cursor=System.Windows.Forms.Cursors.VSplit;
						gStEnd=1;
					}
					else if(deltaEnd<(6/Zoom))
					{
						gAudioPanel.Cursor=System.Windows.Forms.Cursors.VSplit;
						gStEnd=2;
					}
					else
					{
						gAudioPanel.Cursor=System.Windows.Forms.Cursors.Default;
						gStEnd=-1;
					}
				}
			}
		}
		
		void DataPanelMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Left)
			{
				gMouseDown=true;
			}
		}
		
		void DataPanelMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int index;
			int delta;
			if(e.Button== MouseButtons.Left)
			{
				gMouseDown=false;
				if(gViewStIndex==-1)
				{
					gStEnd=-1;
					gViewStIndex=GetIndexFromMouseX(e.X);
					if(gViewStIndex==-1) return;
					if(gViewStIndex==gWavDataBuf.Count-1)
					{
						gViewEndIndex=gViewStIndex;
						gViewStIndex=gViewStIndex-(int)(20.0/Zoom);
						if(gViewStIndex<0){
							gViewStIndex=1;
						}
					}
					else
					{
						gViewEndIndex=gViewStIndex+(int)(20.0/Zoom);
						if(gViewEndIndex>gWavDataBuf.Count-1)
						{
							gViewEndIndex=gWavDataBuf.Count-1;
						}
					}
					gAudioPanel.Invalidate();
				}
			}
			else if(e.Button== MouseButtons.Right)
			{
				if(gViewStIndex==-1 ||  gViewEndIndex==-1) return;
				index=GetIndexFromMouseX(e.X);
				if(gViewEndIndex>gViewStIndex)
				{
					if(index>=gViewStIndex && index<=gViewEndIndex)
					{
						gViewStIndex=-1;
						gViewEndIndex=-1;
						LineChartSizeChanged(null,null);
						gAudioPanel.Invalidate();
						return;
					}
					delta=gViewEndIndex-gViewStIndex;
					if(index>gViewEndIndex){
						gViewEndIndex=index;
						gViewStIndex=gViewEndIndex-delta;
					}else{
						gViewStIndex=index;
						gViewEndIndex=gViewStIndex+delta;
					}
					gAudioPanel.Invalidate();
				}
				else{
					if(index>=gViewEndIndex && index<=gViewStIndex)
					{
						gViewStIndex=-1;
						gViewEndIndex=-1;
						LineChartSizeChanged(null,null);
						gAudioPanel.Invalidate();
						return;
					}
					else{
						delta=gViewStIndex-gViewEndIndex;
						if(index<gViewEndIndex){
							gViewEndIndex=index;
							gViewStIndex=gViewEndIndex+delta;
						}else{
							gViewStIndex=index;
							gViewEndIndex=gViewStIndex-delta;
						}
						gAudioPanel.Invalidate();
					}
				}
			}
		}

		void LineChartMouseWheel(object sender, MouseEventArgs e)
		{
			float limlo=Zoom;
			if(e.Delta>0)
			{
				this.Zoom+=this.Zoom*0.05f;
			}
			else
			{
				this.Zoom-=this.Zoom*0.05f;
			}
			needPrecal=true;
			gAudioPanel.Invalidate();
			System.Diagnostics.Debug.WriteLine("Current zoom = "+ Zoom);
		}
		
		public float Zoom{
			get{
				return this.xZoom;
			}
			set{
				needPrecal=true;
				xZoom=value;
				gAudioPanel.Invalidate();
			}
		}

		
		public void ZoomIn()
		{
			this.Zoom+=this.Zoom*0.05f;
			needPrecal=true;
			gAudioPanel.Invalidate();
		}
		public void ZoomOut()
		{
			this.Zoom-=this.Zoom*0.05f;
			needPrecal=true;
			gAudioPanel.Invalidate();
		}
		public void ZoomFill()
		{
			Zoom=(float)_width/(float)gWavDataBuf.Count;
			needPrecal=true;
			gAudioPanel.Invalidate();
		}
		
		public void Delete(){
			if(gViewStIndex==-1 ||  gViewEndIndex==-1) return;
			int st = Math.Min(gViewStIndex, gViewEndIndex);
			int cnt = Math.Abs(gViewStIndex-gViewEndIndex);
			gWavDataBuf.RemoveRange(st,cnt);
			gViewStIndex=-1;
			gViewEndIndex=-1;
			needPrecal=true;
			gAudioPanel.Invalidate();
		}
	}
}
