using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RED.Match;
using TXT = RED.RedGetText;

namespace RED.UI
{
    public partial class UCFilterList : UserControl
    {
        public UCFilterList()
        {
            InitializeComponent();
            MatchList = new RedMatchItemList();
        }

        private void grdFilter_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            _ = MessageBox.Show($"Row={e.RowIndex}, Col={e.ColumnIndex}, {e.Exception.Message}");
        }

        private RedMatchItemList MatchList { get; set; }

        private void tsbFilterAdd_Click(object sender, EventArgs e)
        {
            AddNewFilterRule();
        }

        private void tsbFilterEdit_Click(object sender, EventArgs e)
        {
            EditFilterRule(GetSelectedRow());
        }

        private void tsbFilterDelete_Click(object sender, EventArgs e)
        {
            DeleteFilterRule(GetSelectedRow());
        }

        private void tsbFilterHelp_Click(object sender, EventArgs e)
        {
            using (FormRtfHelp frm = new FormRtfHelp())
            {
                frm.Title = $"{MatchList.FilterType} Filter";
                frm.HelpText = GetHelpText();
                frm.ShowDialog(this);
            }
        }

        private void tsbCancelEdit_Click(object sender, EventArgs e)
        {
            grdFilter.CancelEdit();
        }

        private void grdFilter_SelectionChanged(object sender, EventArgs e)
        {
            UiUpdateContext();
        }

        private void grdFilter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                grdFilter.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            }
        }

        private void cmFilterMethodMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RedMatchMethodType mm = (RedMatchMethodType)e.ClickedItem.Tag;
            DataGridViewRow row = GetSelectedRow();
            if (row != null)
            {
                row.Cells[colMatchMethod.Index].Value = RedGetText.MatchMethodDescription(mm);
                row.Cells[colMatchMethod.Index].Tag = mm;
            }
        }

        private void grdFilter_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    grdFilter.CurrentCell = grdFilter.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            }
        }

        private void grdFilter_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            UiUpdateContext();
        }

        private void grdFilter_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UiClearSelection();
            UiUpdateContext();
        }

        private void grdFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (grdFilter.IsCurrentCellInEditMode)
                {
                    grdFilter.CancelEdit();
                    e.Handled = true;
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                e.Handled = true;
                DataGridViewCell cell = grdFilter.CurrentCell;
                if (cell != null && cell.ColumnIndex == colMatchMethod.Index)
                {
                    System.Drawing.Point point = grdFilter.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true).Location;
                    point = PointToScreen(point);
                    cmFilterMethodMenu.Show(point);
                }
            }
        }

        private void grdFilter_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colMatchMethod.Index)
            {
                cmFilterMethodMenu.Show(new System.Drawing.Point(MousePosition.X, MousePosition.Y));
            }
        }

        private void UiUpdateContext()
        {
            bool enabledState = GetSelectedRow() != null;
            tsbFilterDelete.Enabled = enabledState;
            tsbFilterEdit.Enabled = enabledState;
            tsbCancelEdit.Enabled = grdFilter.CurrentCell != null && grdFilter.IsCurrentCellInEditMode;
        }

        private void UiClearSelection()
        {
            if (grdFilter.CurrentRow != null)
            {
                grdFilter.CurrentRow.Selected = false;
            }

            if (grdFilter.CurrentCell != null)
            {
                grdFilter.CurrentCell.Selected = false;
            }

            grdFilter.ClearSelection();
        }

        private void BuildMatchMethodContextMenu()
        {
            cmFilterMethodMenu.Items.Clear();

            ToolStripItem tsi;

            // Note - There is an assumption that the 1st letter is a useful shortcut

            tsi = cmFilterMethodMenu.Items.Add("&" + RedGetText.MatchMethodDescription(RedMatchMethodType.Contains));
            tsi.Image = Properties.Resources.x16_FilterContains;
            tsi.Tag = RedMatchMethodType.Contains;

            tsi = cmFilterMethodMenu.Items.Add("&" + RedGetText.MatchMethodDescription(RedMatchMethodType.Endswith));
            tsi.Image = Properties.Resources.x16_FilterEndswith;
            tsi.Tag = RedMatchMethodType.Endswith;

            tsi = cmFilterMethodMenu.Items.Add("&" + RedGetText.MatchMethodDescription(RedMatchMethodType.Startwith));
            tsi.Image = Properties.Resources.x16_FilterStartswith;
            tsi.Tag = RedMatchMethodType.Startwith;

            tsi = cmFilterMethodMenu.Items.Add("&" + RedGetText.MatchMethodDescription(RedMatchMethodType.NameExact));
            tsi.Image = Properties.Resources.x16_FilterName;
            tsi.Tag = RedMatchMethodType.NameExact;

            if (MatchList.FilterType == RedMatchFilterType.Directory)
            {
                tsi = cmFilterMethodMenu.Items.Add("&" + RedGetText.MatchMethodDescription(RedMatchMethodType.NameExactWithPath));
                tsi.Image = Properties.Resources.x16_FilterPath;
                tsi.Tag = RedMatchMethodType.NameExactWithPath;
            }

            tsi = cmFilterMethodMenu.Items.Add("&" + RedGetText.MatchMethodDescription(RedMatchMethodType.RegExName));
            tsi.Image = Properties.Resources.x16_FilterRegExName;
            tsi.Tag = RedMatchMethodType.RegExName;

            if (MatchList.FilterType == RedMatchFilterType.Directory)
            {
                tsi = cmFilterMethodMenu.Items.Add("&" + RedGetText.MatchMethodDescription(RedMatchMethodType.RegExPath));
                tsi.Image = Properties.Resources.x16_FilterRegExPath;
                tsi.Tag = RedMatchMethodType.RegExPath;
            }
        }

        public List<string> GetStringList()
        {
            RedMatchItemList rml = new RedMatchItemList(MatchList.FilterType);

            foreach (DataGridViewRow row in grdFilter.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[colMatchEnabled.Index];
                bool enabled = chk.Value == chk.TrueValue ? true : false;
                RedMatchMethodType matchMethod = (RedMatchMethodType)row.Cells[colMatchMethod.Index].Tag;
                string matchText = row.Cells[colMatchText.Index].Value.ToString();
                rml.AddItem(enabled, matchMethod, matchText);
            }

            return rml.ToStringList();
        }

        public void Populate(List<string> filterList, RedMatchFilterType filterType)
        {
            MatchList.Transform(filterList, filterType);

            BuildMatchMethodContextMenu();

            grdFilter.Rows.Clear();
            foreach (RedMatchItem item in MatchList)
            {
                int rowno = grdFilter.Rows.Add();
                DataGridViewRow row = grdFilter.Rows[rowno];
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[colMatchEnabled.Index];
                chk.Value = item.Enabled ? chk.TrueValue : chk.FalseValue;
                row.Cells[colMatchMethod.Index].Value = RedGetText.MatchMethodDescription(item.MatchMethod);
                row.Cells[colMatchMethod.Index].Tag = item.MatchMethod;
                row.Cells[colMatchText.Index].Value = item.MatchText;
            }
            grdFilter.AutoResizeColumns();
            UiClearSelection();
        }

        private DataGridViewRow GetSelectedRow()
        {
            DataGridViewRow row = null;
            if (grdFilter.SelectedRows.Count > 0)
            {
                row = grdFilter.SelectedRows[0];
            }
            else
            {
                if (grdFilter.SelectedCells.Count > 0)
                {
                    row = grdFilter.SelectedCells[0].OwningRow;
                }
            }
            return row;
        }

        private void EditFilterRule(DataGridViewRow row)
        {
            if (row != null)
            {
                row.Selected = true;
                grdFilter.CurrentCell = row.Cells[colMatchText.Index];
                grdFilter.BeginEdit(true);
            }
        }

        private void DeleteFilterRule(DataGridViewRow row)
        {
            if (row != null)
            {
                grdFilter.Rows.Remove(row);
            }
        }

        private void AddNewFilterRule()
        {
            int rowno = grdFilter.Rows.Add();
            DataGridViewRow row = grdFilter.Rows[rowno];
            RedMatchItem item = new RedMatchItem(RedMatchMethodType.NameExact, "matchtext", true);
            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[colMatchEnabled.Index];
            chk.Value = item.Enabled ? chk.TrueValue : chk.FalseValue;
            row.Cells[colMatchMethod.Index].Value = RedGetText.MatchMethodDescription(item.MatchMethod);
            row.Cells[colMatchMethod.Index].Tag = item.MatchMethod;
            row.Cells[colMatchMethod.Index].ReadOnly = true;
            row.Cells[colMatchText.Index].Value = item.MatchText;
            EditFilterRule(row);
        }

        private string GetHelpText()
        {
            StringBuilder helptxt = new StringBuilder();

            helptxt.AppendLine(@"{\rtf1{");

            helptxt.AppendLine(@"{");
            // x16_filter1 image (really ought to build this at runtime from Properties.Resources.x16_filter1)
            helptxt.AppendLine(@"{\pict\wmetafile8\picw423\pich423\picwgoal240\pichgoal240 ");
            helptxt.AppendLine(@"010009000003ca0200000000a102000000000400000003010800050000000b0200000000050000");
            helptxt.AppendLine(@"000c0210001000030000001e0004000000070104000400000007010400a1020000410b2000cc00");
            helptxt.AppendLine(@"100010000000000010001000000000002800000010000000100000000100080000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000ffffff00fefefe00f5f5f500d6d6d600bdbd");
            helptxt.AppendLine(@"bd00b4b4b400b3b3b300b2b2b200b0b0b000cbcbcb00fdfdfd00fafafa00bababa007a7a7a0078");
            helptxt.AppendLine(@"78780093939300adadad00c9c9c900d7d7d700e7e7e700f2f2f200aaaaaa00d5d5d5009a9a9a00");
            helptxt.AppendLine(@"3c3c3c005e5e5e00777777008e8e8e00a7a7a700c5c5c500d3d3d300e1e1e100ececec00f8f8f8");
            helptxt.AppendLine(@"0098989800ababab00303030004d4d4d006969690085858500a2a2a200c3c3c300d4d4d400e5e5");
            helptxt.AppendLine(@"e5008c8c8c00f6f6f600a8a8a8006b6b6b00575757006d6d6d0097979700e0e0e000aeaeae005f");
            helptxt.AppendLine(@"5f5f00eaeaea00e3e3e300b7b7b700a1a1a1009e9e9e00868686006666660051515100d2d2d200");
            helptxt.AppendLine(@"d0d0d000f7f7f700eeeeee00dbdbdb00b9b9b90091919100727272005d5d5d00b6b6b600dcdcdc");
            helptxt.AppendLine(@"00b1b1b1008484840064646400f9f9f900acacac007373730076767600e8e8e800909090008888");
            helptxt.AppendLine(@"8800d1d1d10068686800e9e9e9006e6e6e00bfbfbf00a6a6a600a3a3a3007f7f7f000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000000000000000000000000000");
            helptxt.AppendLine(@"000000000000000000000000000000000000000000000000000000010101010101010101010101");
            helptxt.AppendLine(@"01010101010101010101415a5b520101010101010101010101015821594f010101010101010101");
            helptxt.AppendLine(@"01010107563b570101010101010101010101014a544555010101010101010101010151524a5336");
            helptxt.AppendLine(@"370101010101010101014d0903134e4f504d01010101010101020d2e49404a4b4c100101010101");
            helptxt.AppendLine(@"0101404142432b444546474801010101012c3738393a3b233c3d3e263f0101012e2f303132332a");
            helptxt.AppendLine(@"342e010c35361401012425262728292a2b2c150101012d010118191a1b1c1d1e1f202122010123");
            helptxt.AppendLine(@"01010c0d0e0f10111213140301151617010101020304050607080708090a0b0101010101010101");
            helptxt.AppendLine(@"01010101010101010101040000002701ffff030000000000}");

            helptxt.AppendFormat(" {0}}}", TXT.Translate("Right-Click on the Filter image at the start of a row to display a context menu"));

            AppendRtfHorzLine(helptxt);
            AppendRtfSubHeader(helptxt, TXT.Translate("Match Method"));
            AppendRtfParagraph(helptxt, TXT.Translate("Right-Click or Double-Click on the Method to change its value. A menu will be displayed from which you can select the required method."));

            AppendRtfHorzLine(helptxt);
            AppendRtfSubHeader(helptxt, TXT.Translate("Match Text"));

            if (MatchList.FilterType == RedMatchFilterType.Files)
            {
                AppendRtfParagraph(helptxt, TXT.Translate("Files: The text is ONLY checked against the filename.No path details are included"));
            }
            if (MatchList.FilterType == RedMatchFilterType.Directory)
            {
                AppendRtfParagraph(helptxt, TXT.Translate("Directories: Contains, Endswith and Startswith\r\nIf the text includes the path seperator the match will be against the entire path. Otherwise it will be against the name only.\r\neg Method=Contains with Text=ab\\\\cd would match D:\\\\xyzab\\\\cdefg"));
                helptxt.AppendLine("\\par ");
                AppendRtfParagraph(helptxt, TXT.Translate("Directories: Path (Exact)\r\nThe text must match the FULL pathname exactly, including the drive letter"));
            }
            helptxt.AppendLine(@"}");

            AppendRtfHorzLine(helptxt);
            AppendRtfSubHeader(helptxt, TXT.Translate("Regular Expressions"));
            AppendRtfParagraph(helptxt, TXT.Translate("Simple Wildcard expressions (asterisk only)\r\n *.tmp will match all files with the ending .tmp like dummy.tmp or empty.tmp etc"));
            helptxt.AppendLine("{\\par }");
            AppendRtfParagraph(helptxt, TXT.Translate("RegEx\r\n ^tmp\\.[0-9]+ will match all files that are named like tmp.001 or tmp.002 etc"));
            helptxt.AppendLine("\\par}");
            if (MatchList.FilterType == RedMatchFilterType.Directory)
            {
                AppendRtfParagraph(helptxt, TXT.Translate("RegEx Name matches against the directory name only"));
                AppendRtfParagraph(helptxt, TXT.Translate("RegEx Path matches against the full pathname"));
            }

            helptxt.AppendLine(@"}}");

            return helptxt.ToString();
        }

        private void AppendRtfParagraph(StringBuilder sb, string text)
        {
            sb.AppendFormat("{{\\par {0} }}", text.Replace("\r\n", "\\par "));
        }

        private void AppendRtfSubHeader(StringBuilder sb, string text)
        {
            sb.AppendFormat("{{\\b {0} \\b0:}}", text);
        }

        private void AppendRtfHorzLine(StringBuilder sb)
        {
            //sb.AppendLine(@"{\pict\wmetafile8\picw26\pich26\picwgoal16000\pichgoal15 ");
            sb.AppendLine(@"{\pict\wmetafile8\picw8\pich8\picwgoal16000\pichgoal8 ");
            sb.AppendLine(@"0100090000035000000000002700000000000400000003010800050000000b0200000000050000");
            sb.AppendLine(@"000c0202000200030000001e000400000007010400040000000701040027000000410b2000cc00");
            sb.AppendLine(@"010001000000000001000100000000002800000001000000010000000100010000000000000000");
            sb.AppendLine(@"000000000000000000000000000000000000000000ffffff00000000ff040000002701ffff0300");
            sb.AppendLine(@"00000000}\par");
        }
    }
}