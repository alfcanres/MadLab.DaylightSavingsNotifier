namespace DSTN.AdminApp.WinForms.Forms.EmailConfigurator
{
    partial class FrmListEmailConfigurator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip1 = new StatusStrip();
            tsLblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            tsbTotalRecords = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            txtSearch = new ToolStripTextBox();
            toolStripLabel1 = new ToolStripLabel();
            cboFilter = new ToolStripComboBox();
            tsbSearch = new ToolStripButton();
            tsbRefresh = new ToolStripButton();
            tsbPrevious = new ToolStripButton();
            lblPageCount = new ToolStripLabel();
            tsbNext = new ToolStripButton();
            tsbAddNew = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsbEdit = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            dataGridView1 = new DataGridView();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsLblStatus, toolStripStatusLabel2, tsbTotalRecords });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1102, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsLblStatus
            // 
            tsLblStatus.Name = "tsLblStatus";
            tsLblStatus.Size = new Size(82, 17);
            tsLblStatus.Text = "Current Status";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 17);
            // 
            // tsbTotalRecords
            // 
            tsbTotalRecords.Name = "tsbTotalRecords";
            tsbTotalRecords.Size = new Size(58, 17);
            tsbTotalRecords.Text = "0 Records";
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { txtSearch, toolStripLabel1, cboFilter, tsbSearch, tsbRefresh, tsbPrevious, lblPageCount, tsbNext, tsbAddNew, toolStripSeparator1, tsbEdit, tsbDelete });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1102, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // txtSearch
            // 
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(250, 25);
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(49, 22);
            toolStripLabel1.Text = "Filter by";
            // 
            // cboFilter
            // 
            cboFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilter.Name = "cboFilter";
            cboFilter.Size = new Size(121, 25);
            // 
            // tsbSearch
            // 
            tsbSearch.Image = Properties.Resources.magnifier;
            tsbSearch.ImageTransparentColor = Color.Magenta;
            tsbSearch.Name = "tsbSearch";
            tsbSearch.Size = new Size(62, 22);
            tsbSearch.Text = "Search";
            tsbSearch.Click += tsbSearch_Click;
            // 
            // tsbRefresh
            // 
            tsbRefresh.Image = Properties.Resources.sync;
            tsbRefresh.ImageTransparentColor = Color.Magenta;
            tsbRefresh.Name = "tsbRefresh";
            tsbRefresh.Size = new Size(87, 22);
            tsbRefresh.Text = "Refresh List";
            tsbRefresh.Click += tsbRefresh_Click;
            // 
            // tsbPrevious
            // 
            tsbPrevious.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbPrevious.ImageTransparentColor = Color.Magenta;
            tsbPrevious.Name = "tsbPrevious";
            tsbPrevious.Size = new Size(72, 22);
            tsbPrevious.Text = "<<Previous";
            tsbPrevious.Click += tsbPrevious_Click;
            // 
            // lblPageCount
            // 
            lblPageCount.Name = "lblPageCount";
            lblPageCount.Size = new Size(65, 22);
            lblPageCount.Text = "Page 1 of 1";
            // 
            // tsbNext
            // 
            tsbNext.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbNext.ImageTransparentColor = Color.Magenta;
            tsbNext.Name = "tsbNext";
            tsbNext.Size = new Size(51, 22);
            tsbNext.Text = "Next>>";
            tsbNext.Click += tsbNext_Click;
            // 
            // tsbAddNew
            // 
            tsbAddNew.Image = Properties.Resources.new_document;
            tsbAddNew.ImageTransparentColor = Color.Magenta;
            tsbAddNew.Name = "tsbAddNew";
            tsbAddNew.Size = new Size(162, 22);
            tsbAddNew.Text = "New Email Configuration";
            tsbAddNew.Click += tsbAddNew_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // tsbEdit
            // 
            tsbEdit.Image = Properties.Resources.editing;
            tsbEdit.ImageTransparentColor = Color.Magenta;
            tsbEdit.Name = "tsbEdit";
            tsbEdit.Size = new Size(94, 22);
            tsbEdit.Text = "Edit Selected";
            tsbEdit.Click += tsbEdit_Click;
            // 
            // tsbDelete
            // 
            tsbDelete.Image = Properties.Resources.delete;
            tsbDelete.ImageTransparentColor = Color.Magenta;
            tsbDelete.Name = "tsbDelete";
            tsbDelete.Size = new Size(107, 22);
            tsbDelete.Text = "Delete Selected";
            tsbDelete.Click += tsbDelete_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 25);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(1102, 403);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellMouseDoubleClick += dataGridView1_CellMouseDoubleClick;
            // 
            // FrmListEmailConfigurator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 450);
            Controls.Add(dataGridView1);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Name = "FrmListEmailConfigurator";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Email Configurations";
            Load += FrmListEmailConfigurator_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsLblStatus;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel tsbTotalRecords;
        private ToolStrip toolStrip1;
        private ToolStripTextBox txtSearch;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox cboFilter;
        private ToolStripButton tsbSearch;
        private ToolStripButton tsbRefresh;
        private ToolStripButton tsbPrevious;
        private ToolStripLabel lblPageCount;
        private ToolStripButton tsbNext;
        private ToolStripButton tsbAddNew;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbEdit;
        private ToolStripButton tsbDelete;
        private DataGridView dataGridView1;
    }
}
