using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NotBob.UI
{
	// Extended DataGridView
	// 2017.03.16 + Fill empty grid with blank shaded 'rows'
	// 2018.10.17 @ Allow for Column & Row headers not being visible
	// 2018.10.18 + Allow for alternating row styles if no hatchstyle is set
	// 2019.01.06 + Provide OnPostPaint event to provide PostPaint info
	// 2024.07.12 @ Add DoubleBuffering to prevent flickering during resize

	public class NBDataGridViewEx1 : DataGridView
	{
		public NBDataGridViewEx1()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
			AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			ExHatchStyle = null;
		}

		public event OnPostPaintHandler OnPostPaint;

		public delegate void OnPostPaintHandler(object sender, int DisplayedHeight);

		public HatchStyle? ExHatchStyle { get; set; }

		// http://social.msdn.microsoft.com/Forums/en-US/winformsdatacontrols/thread/a44622c0-74e1-463b-97b9-27b87513747e#faq13
		// By default, the DataGridView leaves a gray background if the DataGridView size is larger than the data display area size which is needed.
		// To avoid this, we can derive from the DataGridView and override its OnPaint method to draw extra lines in the non-data area.
		protected override void OnPaint(PaintEventArgs e)
		{
			DoPaintPre(e);
			DoPaint(e);
			DoPaintPost(e);
		}

		private void DoPaintPre(PaintEventArgs e)
		{
			// Nothing to do here yet
		}

		private void DoPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			// Create an image for a 'blank' row

			int rowHeight = RowTemplate.Height;
			int imgWidth = Width - 2;
			Rectangle rFrame = new Rectangle(0, 0, imgWidth, rowHeight);
			Rectangle rFill = new Rectangle(1, 1, imgWidth - 2, rowHeight);
			Rectangle rRowHeader = new Rectangle(2, 2, RowHeadersWidth - 3, rowHeight);
			Pen pen = new Pen(GridColor, 1);
			Bitmap rowImg0 = new Bitmap(imgWidth, rowHeight);
			Graphics g0 = Graphics.FromImage(rowImg0);
			g0.DrawRectangle(pen, rFrame);
			// Placeholders for 'alt' row if needed
			Graphics g1 = null;
			Bitmap rowImg1 = null;
			// Fill in the blank row image with the required background details
			if (ExHatchStyle != null)
			{
				g0.FillRectangle(new HatchBrush(ExHatchStyle.Value, DefaultCellStyle.ForeColor, DefaultCellStyle.BackColor), rFill);
				if (RowHeadersVisible)
				{
					g0.FillRectangle(new HatchBrush(ExHatchStyle.Value, DefaultCellStyle.ForeColor, RowHeadersDefaultCellStyle.BackColor), rRowHeader);
				}
			}
			else
			{
				// Create 'alt' row image
				rowImg1 = new Bitmap(imgWidth, rowHeight);
				g1 = Graphics.FromImage(rowImg1);
				g1.DrawRectangle(pen, rFrame);
				// create default and alt row images
				g0.FillRectangle(new SolidBrush(DefaultCellStyle.BackColor), rFill);
				g1.FillRectangle(new SolidBrush(AlternatingRowsDefaultCellStyle.BackColor), rFill);
				if (RowHeadersVisible)
				{
					g0.FillRectangle(new SolidBrush(RowHeadersDefaultCellStyle.BackColor), rRowHeader);
					g1.FillRectangle(new SolidBrush(RowHeadersDefaultCellStyle.BackColor), rRowHeader);
				}
			}
			// Draw the column divider lines onto the blank row
			int w = RowHeadersVisible ? RowHeadersWidth - 1 : 0;
			for (int i = 0; i < ColumnCount; i++)
			{
				g0.DrawLine(pen, new Point(w, 0), new Point(w, rowHeight));
				if (g1 != null)
				{
					g1.DrawLine(pen, new Point(w, 0), new Point(w, rowHeight));
				}
				w += Columns[i].Width;
			}

			// Get the height of the 'real' rows that have already been drawn
			int h = 0;
			foreach (DataGridViewRow row in Rows)
			{
				h += row.Height;
			}
			if (ColumnHeadersVisible)
			{
				h += ColumnHeadersHeight;
			}
			// for each 'missing' row copy the blank row image
			// into place on the remainder of the grid (the non-data area)
			int loop = (Height - h) / rowHeight;
			if (ExHatchStyle != null)
			{
				for (int i = 0; i < loop + 1; i++)
				{
					e.Graphics.DrawImage(rowImg0, 1, (i * rowHeight) + h);
				}
			}
			else
			{
				// Alternate between 'default' and 'alt' blank empty row images
				bool alt = !(RowCount % 2 == 0);
				for (int i = 0; i < loop + 1; i++)
				{
					if (alt)
					{
						e.Graphics.DrawImage(rowImg1, 1, (i * rowHeight) + h);
					}
					else
					{
						e.Graphics.DrawImage(rowImg0, 1, (i * rowHeight) + h);
					}
					alt = !alt;
				}
			}
		}

		private void DoPaintPost(PaintEventArgs e)
		{
			if (OnPostPaint != null)
			{
				OnPostPaint(this, GetDisplayedHeightEx());
			}
		}

		private int GetDisplayedHeightEx()
		{
			int h = 0;
			if (Rows.Count > 0)
			{
				h = Rows.GetRowsHeight(DataGridViewElementStates.None);
				if (ColumnHeadersVisible)
				{
					h += ColumnHeadersHeight;
				}
				// Fudge factor for borders, padding etc
				h += 3;
			}
			return h;
		}
	}
}