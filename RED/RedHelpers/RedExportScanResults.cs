using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using RED.Match;

namespace RED.Helper
{
	class RedExportScanResults : IDisposable
    {
        public RedExportScanResults() { }


        public void Dispose() { }

        public void ExportToFile(RedScanResultItemList v)
        {
            Export(v, toClipboard: false);
        }

        public void ExportToCliboard(RedScanResultItemList v)
        {
            Export(v, toClipboard: true);
        }

        public void Export(RedScanResultItemList v, bool toClipboard)
        {
            try
            {
                if (v != null && v.Count > 0)
                {
                    List<string> exportData = GetExportText(v);
                    if (toClipboard)
                    {
                        Clipboard.SetText(string.Join(Environment.NewLine, exportData.ToArray()), TextDataFormat.UnicodeText);
                    }
                    else
                    {
                        string filename = RedAssist.BrowseForSaveAsFilename("RED_EmptyDirectoryList+.txt");
                        if (!string.IsNullOrWhiteSpace(filename))
                        {
                            File.WriteAllLines(filename, exportData, Encoding.UTF8);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Data To Export", "RED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to export treeview data:" + Environment.NewLine + ex.Message, "RED error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> GetExportText(RedScanResultItemList v)
        {
            List<string> respx = new List<string>();
            for (int i = 0; i < v.Count; i++)
            {
                respx.Add(v[i].FullPath);
                //respx.Add($"{v[i].FullPath} |{{{v[i].SearchStatusOriginal}}}");
                //respx.Add(string.Format("{0}{1}", v[i].FullPath, v[i].FileCount > 0 ? $" |Files={v[i].FileCount}" : ""));
            }
            return respx;
        }

        public void ExportToFile(List<string> v)
        {
            Export(v, toClipboard: false);
        }

        public void ExportToClipboard(List<string> v)
        {
            Export(v, toClipboard: true);
        }

        private void Export(List<string> v, bool toClipboard)
        {
            try
            {
                if (v != null && v.Count > 0)
                {
                    v.Sort();
                    if (toClipboard)
                    {
                        Clipboard.SetText(string.Join(Environment.NewLine, v.ToArray()), TextDataFormat.UnicodeText);
                    }
                    else
                    {
                        string filename = RedAssist.BrowseForSaveAsFilename("RED_EmptyDirectoryList.txt");
                        if (!string.IsNullOrWhiteSpace(filename))
                        {
                            File.WriteAllLines(filename, v);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Directories To Export", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to export data:" + Environment.NewLine + ex.Message, "RED error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportToFile(TreeView tv)
        {
            Export(tv, toClipboard: false);
        }

        public void ExportToClipboard(TreeView tv)
        {
            Export(tv, toClipboard: false);
        }

        private void Export(TreeView tv, bool toClipboard)
        {
            try
            {
                List<string> exportData = BuildExportData(tv);
                if (exportData.Count > 0)
                {
                    if (toClipboard)
                    {

                    }
                    else
                    {
                        string filename = RedAssist.BrowseForSaveAsFilename("RED_EmptyDirectoryList+.txt");
                        if (!string.IsNullOrWhiteSpace(filename))
                        {
                            File.WriteAllLines(filename, exportData, Encoding.UTF8);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Data To Export", "RED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to export treeview data:" + Environment.NewLine + ex.Message, "RED error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> BuildExportData(TreeView tv)
        {
            List<string> respx = new List<string>();
            if (tv != null && tv.Nodes.Count > 0)
            {
                WalkTreeviewNodes(tv.Nodes, respx);
            }
            return respx;
        }

        private void WalkTreeviewNodes(TreeNodeCollection nodes, List<string> exportData)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                TreeNode node = nodes[i];
                string npath = GetFullPathText(node);
                if (!string.IsNullOrWhiteSpace(npath))
                {
                    exportData.Add(npath);
                }
                if (node.Nodes.Count > 0)
                {
                    WalkTreeviewNodes(node.Nodes, exportData);
                }
            }
        }

        private string GetFullPathText(TreeNode node)
        {
            string npath = string.Empty;
            if (node != null && node.ForeColor == TreeManager.ColortoBeDeleted)
            {
                TreeNode pnode = node;
                do
                {
                    npath = pnode.Text + Path.DirectorySeparatorChar + npath;
                    pnode = pnode.Parent;
                } while (pnode != null);
            }
            return npath;
        }
    }
}
