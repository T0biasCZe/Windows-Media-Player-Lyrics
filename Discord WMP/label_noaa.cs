using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Discord_WMP {
	public class LabelNoAa : Label{
		protected override void OnPaint(PaintEventArgs e) {
			var g = e.Graphics;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			//base.OnPaint(e);
			//TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor, BackColor);
			g.DrawString(Text, Font, new System.Drawing.SolidBrush(ForeColor), ClientRectangle);

		}
	}
}
