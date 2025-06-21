
namespace RED.UI
{
    partial class UCFilterList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tsFilterCommands = new System.Windows.Forms.ToolStrip();
			this.tsbFilterAdd = new System.Windows.Forms.ToolStripButton();
			this.tsbFilterDelete = new System.Windows.Forms.ToolStripButton();
			this.tsbFilterEdit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbFilterHelp = new System.Windows.Forms.ToolStripButton();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tsbCancelEdit = new System.Windows.Forms.ToolStripButton();
			this.cmFilterMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsiFilterAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiFilterDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiFilterEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.cmFilterMethodMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.grdFilter = new NotBob.UI.NBDataGridViewEx1();
			this.colBlank = new System.Windows.Forms.DataGridViewImageColumn();
			this.colMatchEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colMatchMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMatchText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tsFilterCommands.SuspendLayout();
			this.cmFilterMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdFilter)).BeginInit();
			this.SuspendLayout();
			// 
			// tsFilterCommands
			// 
			this.tsFilterCommands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsFilterCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFilterAdd,
            this.tsbFilterDelete,
            this.tsbFilterEdit,
            this.toolStripSeparator1,
            this.tsbFilterHelp,
            this.toolStripLabel1,
            this.tsbCancelEdit});
			this.tsFilterCommands.Location = new System.Drawing.Point(0, 0);
			this.tsFilterCommands.Name = "tsFilterCommands";
			this.tsFilterCommands.Size = new System.Drawing.Size(474, 25);
			this.tsFilterCommands.TabIndex = 0;
			this.tsFilterCommands.Text = "toolStrip1";
			// 
			// tsbFilterAdd
			// 
			this.tsbFilterAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFilterAdd.Image = global::RED.Properties.Resources.x16_add2;
			this.tsbFilterAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFilterAdd.Name = "tsbFilterAdd";
			this.tsbFilterAdd.Size = new System.Drawing.Size(23, 22);
			this.tsbFilterAdd.Text = "&Add New Filter";
			this.tsbFilterAdd.Click += new System.EventHandler(this.tsbFilterAdd_Click);
			// 
			// tsbFilterDelete
			// 
			this.tsbFilterDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFilterDelete.Image = global::RED.Properties.Resources.x16_delete2;
			this.tsbFilterDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFilterDelete.Name = "tsbFilterDelete";
			this.tsbFilterDelete.Size = new System.Drawing.Size(23, 22);
			this.tsbFilterDelete.Text = "&Delete Selected Filter";
			this.tsbFilterDelete.Click += new System.EventHandler(this.tsbFilterDelete_Click);
			// 
			// tsbFilterEdit
			// 
			this.tsbFilterEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFilterEdit.Image = global::RED.Properties.Resources.x16_edit1;
			this.tsbFilterEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFilterEdit.Name = "tsbFilterEdit";
			this.tsbFilterEdit.Size = new System.Drawing.Size(23, 22);
			this.tsbFilterEdit.Text = "&Edit Selected Filter";
			this.tsbFilterEdit.Click += new System.EventHandler(this.tsbFilterEdit_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbFilterHelp
			// 
			this.tsbFilterHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbFilterHelp.Image = global::RED.Properties.Resources.x16_help1;
			this.tsbFilterHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFilterHelp.Name = "tsbFilterHelp";
			this.tsbFilterHelp.Size = new System.Drawing.Size(52, 22);
			this.tsbFilterHelp.Text = "&Help";
			this.tsbFilterHelp.Click += new System.EventHandler(this.tsbFilterHelp_Click);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(55, 22);
			this.toolStripLabel1.Text = "                ";
			// 
			// tsbCancelEdit
			// 
			this.tsbCancelEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbCancelEdit.Image = global::RED.Properties.Resources.x16_undo1;
			this.tsbCancelEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCancelEdit.Name = "tsbCancelEdit";
			this.tsbCancelEdit.Size = new System.Drawing.Size(23, 22);
			this.tsbCancelEdit.Text = "Cancel Edit";
			this.tsbCancelEdit.Click += new System.EventHandler(this.tsbCancelEdit_Click);
			// 
			// cmFilterMenu
			// 
			this.cmFilterMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiFilterAdd,
            this.tsiFilterDelete,
            this.tsiFilterEdit});
			this.cmFilterMenu.Name = "cmFilterMenu";
			this.cmFilterMenu.Size = new System.Drawing.Size(184, 92);
			// 
			// tsiFilterAdd
			// 
			this.tsiFilterAdd.Image = global::RED.Properties.Resources.x16_add2;
			this.tsiFilterAdd.Name = "tsiFilterAdd";
			this.tsiFilterAdd.Size = new System.Drawing.Size(183, 22);
			this.tsiFilterAdd.Text = "&Add New Filter";
			this.tsiFilterAdd.Click += new System.EventHandler(this.tsbFilterAdd_Click);
			// 
			// tsiFilterDelete
			// 
			this.tsiFilterDelete.Image = global::RED.Properties.Resources.x16_delete2;
			this.tsiFilterDelete.Name = "tsiFilterDelete";
			this.tsiFilterDelete.Size = new System.Drawing.Size(183, 22);
			this.tsiFilterDelete.Text = "&Delete Selected Filter";
			this.tsiFilterDelete.Click += new System.EventHandler(this.tsbFilterDelete_Click);
			// 
			// tsiFilterEdit
			// 
			this.tsiFilterEdit.Image = global::RED.Properties.Resources.x16_edit1;
			this.tsiFilterEdit.Name = "tsiFilterEdit";
			this.tsiFilterEdit.Size = new System.Drawing.Size(183, 22);
			this.tsiFilterEdit.Text = "&Edit Selected Filter";
			this.tsiFilterEdit.Click += new System.EventHandler(this.tsbFilterEdit_Click);
			// 
			// cmFilterMethodMenu
			// 
			this.cmFilterMethodMenu.Name = "cmFilterMenu";
			this.cmFilterMethodMenu.Size = new System.Drawing.Size(61, 4);
			this.cmFilterMethodMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmFilterMethodMenu_ItemClicked);
			// 
			// grdFilter
			// 
			this.grdFilter.AllowUserToAddRows = false;
			this.grdFilter.AllowUserToDeleteRows = false;
			this.grdFilter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.grdFilter.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.grdFilter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grdFilter.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.grdFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdFilter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBlank,
            this.colMatchEnabled,
            this.colMatchMethod,
            this.colMatchText});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.grdFilter.DefaultCellStyle = dataGridViewCellStyle3;
			this.grdFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdFilter.EnableHeadersVisualStyles = false;
			this.grdFilter.ExHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent10;
			this.grdFilter.Location = new System.Drawing.Point(0, 25);
			this.grdFilter.MultiSelect = false;
			this.grdFilter.Name = "grdFilter";
			this.grdFilter.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grdFilter.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.grdFilter.RowHeadersVisible = false;
			this.grdFilter.RowHeadersWidth = 16;
			this.grdFilter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.grdFilter.Size = new System.Drawing.Size(474, 264);
			this.grdFilter.TabIndex = 1;
			this.grdFilter.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grdFilter_CellBeginEdit);
			this.grdFilter.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFilter_CellClick);
			this.grdFilter.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFilter_CellContentDoubleClick);
			this.grdFilter.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFilter_CellEndEdit);
			this.grdFilter.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdFilter_CellMouseDown);
			this.grdFilter.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdFilter_DataError);
			this.grdFilter.SelectionChanged += new System.EventHandler(this.grdFilter_SelectionChanged);
			this.grdFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdFilter_KeyDown);
			// 
			// colBlank
			// 
			this.colBlank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.colBlank.ContextMenuStrip = this.cmFilterMenu;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.NullValue = "System.Drawing.Bitmap";
			this.colBlank.DefaultCellStyle = dataGridViewCellStyle2;
			this.colBlank.HeaderText = "";
			this.colBlank.Image = global::RED.Properties.Resources.x16_filter1;
			this.colBlank.MinimumWidth = 16;
			this.colBlank.Name = "colBlank";
			this.colBlank.ReadOnly = true;
			this.colBlank.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colBlank.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.colBlank.Width = 18;
			// 
			// colMatchEnabled
			// 
			this.colMatchEnabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colMatchEnabled.FalseValue = "-";
			this.colMatchEnabled.HeaderText = "Active";
			this.colMatchEnabled.MinimumWidth = 32;
			this.colMatchEnabled.Name = "colMatchEnabled";
			this.colMatchEnabled.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colMatchEnabled.ToolTipText = "Check this to enable the filter rule";
			this.colMatchEnabled.TrueValue = "+";
			this.colMatchEnabled.Width = 37;
			// 
			// colMatchMethod
			// 
			this.colMatchMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colMatchMethod.ContextMenuStrip = this.cmFilterMethodMenu;
			this.colMatchMethod.FillWeight = 25F;
			this.colMatchMethod.HeaderText = "Method";
			this.colMatchMethod.MinimumWidth = 60;
			this.colMatchMethod.Name = "colMatchMethod";
			this.colMatchMethod.ReadOnly = true;
			this.colMatchMethod.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colMatchMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colMatchMethod.ToolTipText = "How the match text will be processed";
			this.colMatchMethod.Width = 60;
			// 
			// colMatchText
			// 
			this.colMatchText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colMatchText.ContextMenuStrip = this.cmFilterMenu;
			this.colMatchText.FillWeight = 75F;
			this.colMatchText.HeaderText = "Match Text";
			this.colMatchText.MinimumWidth = 100;
			this.colMatchText.Name = "colMatchText";
			this.colMatchText.ToolTipText = "The text used to match against the directory or file";
			// 
			// UCFilterList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grdFilter);
			this.Controls.Add(this.tsFilterCommands);
			this.DoubleBuffered = true;
			this.Name = "UCFilterList";
			this.Size = new System.Drawing.Size(474, 289);
			this.tsFilterCommands.ResumeLayout(false);
			this.tsFilterCommands.PerformLayout();
			this.cmFilterMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdFilter)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsFilterCommands;
        private System.Windows.Forms.ToolStripButton tsbFilterAdd;
        private System.Windows.Forms.ToolStripButton tsbFilterDelete;
        private System.Windows.Forms.ToolStripButton tsbFilterEdit;
        private System.Windows.Forms.ContextMenuStrip cmFilterMenu;
        private System.Windows.Forms.ToolStripButton tsbFilterHelp;
        private NotBob.UI.NBDataGridViewEx1 grdFilter;
        private System.Windows.Forms.ToolStripMenuItem tsiFilterAdd;
        private System.Windows.Forms.ToolStripMenuItem tsiFilterDelete;
        private System.Windows.Forms.ToolStripMenuItem tsiFilterEdit;
        private System.Windows.Forms.ContextMenuStrip cmFilterMethodMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCancelEdit;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridViewImageColumn colBlank;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMatchEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatchMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatchText;
    }
}
