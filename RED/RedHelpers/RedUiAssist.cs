using System;
using System.Drawing;
using System.Windows.Forms;

using TXT = RED.RedGetText;

namespace RED.Helper
{
    internal class UiAssist
    {
        internal static readonly string CrLf1 = "\r\n";
        internal static readonly string CrLf2 = "\r\n\r\n";

        internal static Point GetScreenValidLocation(Point location)
        {
            Point respx = location;
            if (!IsOnScreen(location))
            {
                Rectangle screenRect = Screen.GetWorkingArea(location);
                respx = screenRect.Location;
            }
            return respx;
        }

        internal static bool IsOnScreen(Point location)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen item in screens)
            {
                if (item.WorkingArea.Contains(location))
                {
                    return true;
                }
            }
            return false;
        }

        internal static void ListViewAdjustLastColumnToFill(ListView lvw)
        {
            ListViewAdjustLastColumnToFill(lvw, ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        internal static void ListViewAdjustLastColumnToFill(ListView lvw, ColumnHeaderAutoResizeStyle resizeStyle)
        {
            // Set all columns to AutoSize
            lvw.AutoResizeColumns(resizeStyle);

            // The .Tag property may contain additional formatting info
            ListViewAdjustColumnMinWidths(lvw);

            // If there is any width remaining, that will be the width of the last column.
            int width = ListViewCalculateLastColumnWidth(lvw);
            if (width > 0)
            {
                int indx = ListViewGetLastDisplayedColumn(lvw);
                lvw.Columns[indx].Width = width;
            }
        }

        private static void ListViewAdjustColumnMinWidths(ListView lvw)
        {
            for (int i = 0; i < lvw.Columns.Count; i++)
            {
                ColumnHeader colhdr = lvw.Columns[i];
                if (colhdr.Tag != null)
                {
                    int minwidth = ConvertToInt(colhdr.Tag);

                    if (minwidth > 0)
                    {
                        if (colhdr.Width < minwidth)
                        {
                            colhdr.Width = minwidth;
                        }
                    }
                }
            }
        }

        private static int ListViewCalculateLastColumnWidth(ListView lvw)
        {
            // Get the width of the listview
            lvw.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            int width = lvw.ClientSize.Width;
            // If AutoResizeColumns has been called the vscroll width will already have been taken into account
            //if (lvw.Scrollable && IsVScrollbarVisible(lvw.Handle))
            //{
            //   width -= SystemInformation.VerticalScrollBarWidth;
            //}
            switch (lvw.BorderStyle)
            {
                case BorderStyle.None:
                    break;
                default:
                    width -= 2;
                    break;
            }
            // Add up the widths of all but the last (displayed) columns.
            for (int i = 0; i < lvw.Columns.Count; i++)
            {
                if (lvw.Columns[i].DisplayIndex != lvw.Columns.Count - 1)
                {
                    // Subtract width of the column from the width of the client area
                    width -= lvw.Columns[i].Width;
                    // If the width goes below 1, then no need to keep going
                    // because the last column can't be sized to fit
                    // due to the widths of the columns before it.
                    if (width < 1)
                    {
                        break;
                    }
                }
            }
            ;
            return width;
        }

        internal static int ListViewGetLastDisplayedColumn(ListView lvw)
        {
            int indx = lvw.Columns.Count - 1;
            for (int i = 0; i < lvw.Columns.Count; i++)
            {
                if (lvw.Columns[i].DisplayIndex == lvw.Columns.Count - 1)
                {
                    indx = i;
                    break;
                }
            }
            return indx;
        }

        private static int ConvertToInt(object obj)
        {
            int respx;
            int.TryParse(obj.ToString(), out respx);
            return respx;
        }

        internal static void ToDo(string text)
        {
            ToDo(text, "");
        }

        internal static void ToDo(string text, string caption)
        {
            MessageBox.Show(text, string.Format("{0}:TODO: {1}", Application.ProductName, caption), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static bool BAskYesNo(string text, MessageBoxDefaultButton defaultButton)
        {
            if (MessageBox.Show(text, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static DialogResult MsgBoxError(string emsg)
        {
            return MessageBox.Show(emsg, TXT.Red.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult MsgBoxError(IWin32Window owner, string emsg)
        {
            return MessageBox.Show(owner, emsg, TXT.Red.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult MsgBoxException(string emsg, Exception ex)
        {
            return MessageBox.Show(string.Format("{0}:{1}{2}", emsg, CrLf2, ex.Message), TXT.Red.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult MsgBoxException(IWin32Window owner, string emsg, Exception ex)
        {
            return MessageBox.Show(owner, string.Format("{0}:{1}{2}", emsg, CrLf2, ex.Message), TXT.Red.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult MsgBoxYesNo(IWin32Window owner, string msg, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(owner, msg, TXT.Red.CaptionInfo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton);
        }

        internal static DialogResult MsgBoxInfo(string msg)
        {
            return MessageBox.Show(msg, TXT.Red.CaptionInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static DialogResult MsgBoxInfo(IWin32Window owner, string msg)
        {
            return MessageBox.Show(owner, msg, TXT.Red.CaptionInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static DialogResult MsgBoxWarning(IWin32Window owner, string msg)
        {
            return MessageBox.Show(owner, msg, TXT.Red.CaptionInfo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}