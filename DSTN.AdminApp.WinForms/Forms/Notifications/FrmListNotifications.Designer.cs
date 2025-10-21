namespace DSTN.AdminApp.WinForms.Notifications
{
    partial class FrmListNotifications
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListNotifications));
            statusStrip1 = new StatusStrip();
            tsLblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            tsbTotalRecords = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            tsblNotifications = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            tsbRefresh = new ToolStripButton();
            tsbPrevious = new ToolStripButton();
            lblPageCount = new ToolStripLabel();
            tsbNext = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsbEdit = new ToolStripButton();
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            rbNotSeen = new RadioButton();
            btnLoadNotifications = new Button();
            rbSeen = new RadioButton();
            rbAll = new RadioButton();
            cboTimeZones = new ComboBox();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsLblStatus, toolStripStatusLabel2, tsbTotalRecords, toolStripStatusLabel1, tsblNotifications });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1212, 22);
            statusStrip1.TabIndex = 5;
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
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(10, 17);
            toolStripStatusLabel1.Text = "|";
            // 
            // tsblNotifications
            // 
            tsblNotifications.Image = Properties.Resources.notification;
            tsblNotifications.Name = "tsblNotifications";
            tsblNotifications.Size = new Size(16, 17);
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbRefresh, tsbPrevious, lblPageCount, tsbNext, toolStripSeparator1, tsbEdit });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1212, 25);
            toolStrip1.TabIndex = 6;
            toolStrip1.Text = "toolStrip1";
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
            tsbPrevious.Image = (Image)resources.GetObject("tsbPrevious.Image");
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
            tsbNext.Image = (Image)resources.GetObject("tsbNext.Image");
            tsbNext.ImageTransparentColor = Color.Magenta;
            tsbNext.Name = "tsbNext";
            tsbNext.Size = new Size(51, 22);
            tsbNext.Text = "Next>>";
            tsbNext.Click += tsbNext_Click;
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
            tsbEdit.Size = new Size(99, 22);
            tsbEdit.Text = "View Selected";
            tsbEdit.Click += tsbEdit_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 48);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(1206, 352);
            dataGridView1.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.Controls.Add(rbNotSeen);
            panel1.Controls.Add(btnLoadNotifications);
            panel1.Controls.Add(rbSeen);
            panel1.Controls.Add(rbAll);
            panel1.Controls.Add(cboTimeZones);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1206, 39);
            panel1.TabIndex = 8;
            // 
            // rbNotSeen
            // 
            rbNotSeen.AutoSize = true;
            rbNotSeen.Location = new Point(355, 13);
            rbNotSeen.Name = "rbNotSeen";
            rbNotSeen.Size = new Size(73, 19);
            rbNotSeen.TabIndex = 6;
            rbNotSeen.TabStop = true;
            rbNotSeen.Text = "Not Seen";
            rbNotSeen.UseVisualStyleBackColor = true;
            // 
            // btnLoadNotifications
            // 
            btnLoadNotifications.Location = new Point(477, 9);
            btnLoadNotifications.Name = "btnLoadNotifications";
            btnLoadNotifications.Size = new Size(145, 23);
            btnLoadNotifications.TabIndex = 5;
            btnLoadNotifications.Text = "Load Notifications";
            btnLoadNotifications.UseVisualStyleBackColor = true;
            btnLoadNotifications.Click += btnLoadNotifications_Click;
            // 
            // rbSeen
            // 
            rbSeen.AutoSize = true;
            rbSeen.Location = new Point(421, 13);
            rbSeen.Name = "rbSeen";
            rbSeen.Size = new Size(50, 19);
            rbSeen.TabIndex = 4;
            rbSeen.TabStop = true;
            rbSeen.Text = "Seen";
            rbSeen.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            rbAll.AutoSize = true;
            rbAll.Location = new Point(297, 13);
            rbAll.Name = "rbAll";
            rbAll.Size = new Size(39, 19);
            rbAll.TabIndex = 2;
            rbAll.TabStop = true;
            rbAll.Text = "All";
            rbAll.UseVisualStyleBackColor = true;
            // 
            // cboTimeZones
            // 
            cboTimeZones.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTimeZones.FormattingEnabled = true;
            cboTimeZones.Location = new Point(89, 9);
            cboTimeZones.Name = "cboTimeZones";
            cboTimeZones.Size = new Size(202, 23);
            cboTimeZones.TabIndex = 1;
            cboTimeZones.SelectedIndexChanged += cboTimeZones_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 9);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 0;
            label1.Text = "Time Zones";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1662531F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 88.83375F));
            tableLayoutPanel1.Size = new Size(1212, 403);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // FrmListNotifications
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1212, 450);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmListNotifications";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmListNotifications";
            Load += FrmListTimeZones_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbRefresh;
        private DataGridView dataGridView1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbEdit;
        private ToolStripStatusLabel tsLblStatus;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripButton tsbPrevious;
        private ToolStripButton tsbNext;
        private ToolStripLabel lblPageCount;
        private ToolStripStatusLabel tsbTotalRecords;
        private Panel panel1;
        private RadioButton rbSeen;
        private RadioButton radioButton2;
        private RadioButton rbAll;
        private ComboBox cboTimeZones;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnLoadNotifications;
        private RadioButton rbNotSeen;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel tsblNotifications;
    }
}