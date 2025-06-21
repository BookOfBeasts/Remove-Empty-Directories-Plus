using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RED.UI
{
	public class UCMenuButton : Button
	{
		[DefaultValue(null), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public ContextMenuStrip Menu { get; set; }

		[DefaultValue(20), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int SplitWidth { get; set; }

		//protected override void OnMouseDown(MouseEventArgs mevent)
		protected override void OnClick(EventArgs e)
		{
			if (Menu != null)
			{
				Point menuLocation;
				Point screenPoint = this.PointToScreen(new Point(this.Left, this.Bottom));
				if (screenPoint.Y + Menu.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
				{
					menuLocation = new Point(0, -Menu.Size.Height);
				}
				else
				{
					menuLocation = new Point(0, this.Height);
				}
				Menu.Show(this, menuLocation);
			}
		}

		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);

			if (Menu != null)
			{
				// Draw the arrow glyph on the right side of the button
				int arrowX = ClientRectangle.Width - 14;
				int arrowY = ClientRectangle.Height / 2 - 1;

				Brush arrowBrush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow;
				Point[] arrows = new[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
				pevent.Graphics.FillPolygon(arrowBrush, arrows);

				// Draw a dashed separator on the left of the arrow
				int lineX = ClientRectangle.Width - this.SplitWidth;
				int lineYFrom = arrowY - 4;
				int lineYTo = arrowY + 8;
				using (Pen separatorPen = new Pen(Brushes.DarkGray) { DashStyle = DashStyle.Dot })
				{
					pevent.Graphics.DrawLine(separatorPen, lineX, lineYFrom, lineX, lineYTo);
				}
			}
		}
	}
}