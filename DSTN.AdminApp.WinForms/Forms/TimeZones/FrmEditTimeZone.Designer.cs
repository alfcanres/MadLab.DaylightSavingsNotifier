namespace DSTN.AdminApp.WinForms.TimeZones
{
    partial class FrmEditTimeZone
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditTimeZone));
            toolStrip1 = new ToolStrip();
            btnNew = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnSave = new ToolStripButton();
            btnDelete = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            lblLoadingStatus = new ToolStripStatusLabel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            txtId = new TextBox();
            label2 = new Label();
            txtColor = new TextBox();
            label3 = new Label();
            txtDisplayName = new TextBox();
            label4 = new Label();
            cboTimeZoneId = new ComboBox();
            chkIsActive = new CheckBox();
            label10 = new Label();
            nudNotifyDaysBefore = new NumericUpDown();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            lblTimeZoneObservesDST = new Label();
            label7 = new Label();
            txtDSTStarts = new TextBox();
            label8 = new Label();
            txtDSTEnds = new TextBox();
            label6 = new Label();
            txtLastChanged = new TextBox();
            label9 = new Label();
            txtNextNotifyDate = new TextBox();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNotifyDaysBefore).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnNew, toolStripSeparator1, btnSave, btnDelete });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(499, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            btnNew.Image = Properties.Resources.new_document;
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(76, 22);
            btnNew.Text = "Add New";
            btnNew.Click += btnNew_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // btnSave
            // 
            btnSave.Image = Properties.Resources.diskette;
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(51, 22);
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Image = Properties.Resources.delete;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(60, 22);
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblLoadingStatus });
            statusStrip1.Location = new Point(0, 535);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(499, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblLoadingStatus
            // 
            lblLoadingStatus.Name = "lblLoadingStatus";
            lblLoadingStatus.Size = new Size(59, 17);
            lblLoadingStatus.Text = "Loading...";
            lblLoadingStatus.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(txtId);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(txtColor);
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(txtDisplayName);
            flowLayoutPanel1.Controls.Add(label4);
            flowLayoutPanel1.Controls.Add(cboTimeZoneId);
            flowLayoutPanel1.Controls.Add(chkIsActive);
            flowLayoutPanel1.Controls.Add(label10);
            flowLayoutPanel1.Controls.Add(nudNotifyDaysBefore);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(493, 263);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 0;
            label1.Text = "ID";
            // 
            // txtId
            // 
            txtId.Location = new Point(3, 18);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(71, 23);
            txtId.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 44);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 2;
            label2.Text = "Color";
            // 
            // txtColor
            // 
            txtColor.Location = new Point(3, 62);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(141, 23);
            txtColor.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 88);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 4;
            label3.Text = "Display Name";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Location = new Point(3, 106);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(434, 23);
            txtDisplayName.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 132);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 6;
            label4.Text = "Time Zone ID";
            // 
            // cboTimeZoneId
            // 
            cboTimeZoneId.FormattingEnabled = true;
            cboTimeZoneId.Location = new Point(3, 150);
            cboTimeZoneId.Name = "cboTimeZoneId";
            cboTimeZoneId.Size = new Size(434, 23);
            cboTimeZoneId.TabIndex = 7;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(3, 179);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(75, 19);
            chkIsActive.TabIndex = 8;
            chkIsActive.Text = "Is Active?";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(3, 201);
            label10.Name = "label10";
            label10.Size = new Size(104, 15);
            label10.TabIndex = 9;
            label10.Text = "Notify days before";
            // 
            // nudNotifyDaysBefore
            // 
            nudNotifyDaysBefore.Location = new Point(3, 219);
            nudNotifyDaysBefore.Name = "nudNotifyDaysBefore";
            nudNotifyDaysBefore.Size = new Size(120, 23);
            nudNotifyDaysBefore.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.7451F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 47.2549F));
            tableLayoutPanel1.Size = new Size(499, 510);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(lblTimeZoneObservesDST);
            flowLayoutPanel2.Controls.Add(label7);
            flowLayoutPanel2.Controls.Add(txtDSTStarts);
            flowLayoutPanel2.Controls.Add(label8);
            flowLayoutPanel2.Controls.Add(txtDSTEnds);
            flowLayoutPanel2.Controls.Add(label6);
            flowLayoutPanel2.Controls.Add(txtLastChanged);
            flowLayoutPanel2.Controls.Add(label9);
            flowLayoutPanel2.Controls.Add(txtNextNotifyDate);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(3, 272);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(493, 235);
            flowLayoutPanel2.TabIndex = 4;
            // 
            // lblTimeZoneObservesDST
            // 
            lblTimeZoneObservesDST.AutoSize = true;
            lblTimeZoneObservesDST.Location = new Point(3, 0);
            lblTimeZoneObservesDST.Name = "lblTimeZoneObservesDST";
            lblTimeZoneObservesDST.Size = new Size(209, 15);
            lblTimeZoneObservesDST.TabIndex = 0;
            lblTimeZoneObservesDST.Text = "This time zone does not observes DST!";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 15);
            label7.Name = "label7";
            label7.Size = new Size(40, 15);
            label7.TabIndex = 4;
            label7.Text = "Start:  ";
            // 
            // txtDSTStarts
            // 
            txtDSTStarts.Location = new Point(3, 33);
            txtDSTStarts.Name = "txtDSTStarts";
            txtDSTStarts.ReadOnly = true;
            txtDSTStarts.Size = new Size(469, 23);
            txtDSTStarts.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 59);
            label8.Name = "label8";
            label8.Size = new Size(35, 15);
            label8.TabIndex = 6;
            label8.Text = "Ends:";
            // 
            // txtDSTEnds
            // 
            txtDSTEnds.Location = new Point(3, 77);
            txtDSTEnds.Name = "txtDSTEnds";
            txtDSTEnds.ReadOnly = true;
            txtDSTEnds.Size = new Size(469, 23);
            txtDSTEnds.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 103);
            label6.Name = "label6";
            label6.Size = new Size(77, 15);
            label6.TabIndex = 9;
            label6.Text = "Last changed";
            // 
            // txtLastChanged
            // 
            txtLastChanged.Location = new Point(3, 121);
            txtLastChanged.Name = "txtLastChanged";
            txtLastChanged.ReadOnly = true;
            txtLastChanged.Size = new Size(469, 23);
            txtLastChanged.TabIndex = 10;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(3, 147);
            label9.Name = "label9";
            label9.Size = new Size(91, 15);
            label9.TabIndex = 11;
            label9.Text = "Next notify date";
            // 
            // txtNextNotifyDate
            // 
            txtNextNotifyDate.Location = new Point(3, 165);
            txtNextNotifyDate.Name = "txtNextNotifyDate";
            txtNextNotifyDate.ReadOnly = true;
            txtNextNotifyDate.Size = new Size(469, 23);
            txtNextNotifyDate.TabIndex = 12;
            // 
            // FrmEditTimeZone
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(499, 557);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmEditTimeZone";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmEditTimeZone";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNotifyDaysBefore).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblLoadingStatus;
        private ToolStripButton btnNew;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnSave;
        private ToolStripButton btnDelete;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private TextBox txtId;
        private Label label2;
        private TextBox txtColor;
        private Label label3;
        private TextBox txtDisplayName;
        private Label label4;
        private ComboBox cboTimeZoneId;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label lblTimeZoneObservesDST;
        private Label label7;
        private Label label8;
        private TextBox txtDSTStarts;
        private TextBox txtDSTEnds;
        private Label label6;
        private TextBox txtLastChanged;
        private Label label9;
        private TextBox txtNextNotifyDate;
        private CheckBox chkIsActive;
        private Label label10;
        private NumericUpDown nudNotifyDaysBefore;
    }
}