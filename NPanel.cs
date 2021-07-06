/*
 * Created by SharpDevelop.
 * User: gheyret
 * Time: 8:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace AudioEditor
{
	/// <summary>
	/// Description of NPanel.
	/// </summary>
	public class GPanel:Panel
	{
		public GPanel()
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer|ControlStyles.ResizeRedraw, true);
			this.UpdateStyles();			
		}
	}
}
