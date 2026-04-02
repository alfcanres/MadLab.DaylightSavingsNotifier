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
            toolStripSeparator2 = new ToolStripSeparator();
            tsbCloseOnSave = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            lblLoadingStatus = new ToolStripStatusLabel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            txtId = new TextBox();
            label2 = new Label();
            txtColor = new TextBox();
            lblTimeZoneObservesDST = new Label();
            label3 = new Label();
            txtDisplayName = new TextBox();
            label4 = new Label();
            cboTimeZoneId = new ComboBox();
            chkIsActive = new CheckBox();
            label10 = new Label();
            nudNotifyDaysBefore = new NumericUpDown();
            label5 = new Label();
            txtComments = new TextBox();
            label7 = new Label();
            txtDSTStarts = new TextBox();
            label8 = new Label();
            txtDSTEnds = new TextBox();
            label6 = new Label();
            txtLastChanged = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            colorDialog1 = new ColorDialog();
            tbMain = new TabControl();
            tbpMain = new TabPage();
            tbpEmailList = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel1 = new Panel();
            btnRemoveEmail = new Button();
            txtAddEmail = new TextBox();
            lblAddEmail = new Label();
            btnAddEmail = new Button();
            dgvEmailList = new DataGridView();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNotifyDaysBefore).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tbMain.SuspendLayout();
            tbpMain.SuspendLayout();
            tbpEmailList.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmailList).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnNew, toolStripSeparator1, btnSave, btnDelete, toolStripSeparator2, tsbCloseOnSave });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(590, 25);
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
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // tsbCloseOnSave
            // 
            tsbCloseOnSave.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbCloseOnSave.Name = "tsbCloseOnSave";
            tsbCloseOnSave.Size = new Size(83, 22);
            tsbCloseOnSave.Text = "Close on save";
            tsbCloseOnSave.Click += tsbCloseOnSave_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblLoadingStatus });
            statusStrip1.Location = new Point(0, 567);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(590, 22);
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
            flowLayoutPanel1.Controls.Add(lblTimeZoneObservesDST);
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(txtDisplayName);
            flowLayoutPanel1.Controls.Add(label4);
            flowLayoutPanel1.Controls.Add(cboTimeZoneId);
            flowLayoutPanel1.Controls.Add(chkIsActive);
            flowLayoutPanel1.Controls.Add(label10);
            flowLayoutPanel1.Controls.Add(nudNotifyDaysBefore);
            flowLayoutPanel1.Controls.Add(label5);
            flowLayoutPanel1.Controls.Add(txtComments);
            flowLayoutPanel1.Controls.Add(label7);
            flowLayoutPanel1.Controls.Add(txtDSTStarts);
            flowLayoutPanel1.Controls.Add(label8);
            flowLayoutPanel1.Controls.Add(txtDSTEnds);
            flowLayoutPanel1.Controls.Add(label6);
            flowLayoutPanel1.Controls.Add(txtLastChanged);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(570, 502);
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
            txtColor.Cursor = Cursors.Hand;
            txtColor.Location = new Point(3, 62);
            txtColor.Name = "txtColor";
            txtColor.ReadOnly = true;
            txtColor.Size = new Size(141, 23);
            txtColor.TabIndex = 3;
            txtColor.Click += txtColor_Click;
            // 
            // lblTimeZoneObservesDST
            // 
            lblTimeZoneObservesDST.AutoSize = true;
            lblTimeZoneObservesDST.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTimeZoneObservesDST.Location = new Point(3, 88);
            lblTimeZoneObservesDST.Name = "lblTimeZoneObservesDST";
            lblTimeZoneObservesDST.Size = new Size(222, 15);
            lblTimeZoneObservesDST.TabIndex = 0;
            lblTimeZoneObservesDST.Text = "This time zone does not observes DST!";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 103);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 4;
            label3.Text = "Display Name";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Location = new Point(3, 121);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(434, 23);
            txtDisplayName.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 147);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 6;
            label4.Text = "Time Zone ID";
            // 
            // cboTimeZoneId
            // 
            cboTimeZoneId.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTimeZoneId.FormattingEnabled = true;
            cboTimeZoneId.Location = new Point(3, 165);
            cboTimeZoneId.Name = "cboTimeZoneId";
            cboTimeZoneId.Size = new Size(434, 23);
            cboTimeZoneId.TabIndex = 7;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(3, 194);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(75, 19);
            chkIsActive.TabIndex = 8;
            chkIsActive.Text = "Is Active?";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(3, 216);
            label10.Name = "label10";
            label10.Size = new Size(104, 15);
            label10.TabIndex = 9;
            label10.Text = "Notify days before";
            // 
            // nudNotifyDaysBefore
            // 
            nudNotifyDaysBefore.Location = new Point(3, 234);
            nudNotifyDaysBefore.Name = "nudNotifyDaysBefore";
            nudNotifyDaysBefore.Size = new Size(120, 23);
            nudNotifyDaysBefore.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 260);
            label5.Name = "label5";
            label5.Size = new Size(66, 15);
            label5.TabIndex = 12;
            label5.Text = "Comments";
            // 
            // txtComments
            // 
            txtComments.Location = new Point(3, 278);
            txtComments.Multiline = true;
            txtComments.Name = "txtComments";
            txtComments.Size = new Size(458, 76);
            txtComments.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 357);
            label7.Name = "label7";
            label7.Size = new Size(40, 15);
            label7.TabIndex = 4;
            label7.Text = "Start:  ";
            // 
            // txtDSTStarts
            // 
            txtDSTStarts.Location = new Point(3, 375);
            txtDSTStarts.Name = "txtDSTStarts";
            txtDSTStarts.ReadOnly = true;
            txtDSTStarts.Size = new Size(469, 23);
            txtDSTStarts.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 401);
            label8.Name = "label8";
            label8.Size = new Size(35, 15);
            label8.TabIndex = 6;
            label8.Text = "Ends:";
            // 
            // txtDSTEnds
            // 
            txtDSTEnds.Location = new Point(3, 419);
            txtDSTEnds.Name = "txtDSTEnds";
            txtDSTEnds.ReadOnly = true;
            txtDSTEnds.Size = new Size(469, 23);
            txtDSTEnds.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 445);
            label6.Name = "label6";
            label6.Size = new Size(77, 15);
            label6.TabIndex = 9;
            label6.Text = "Last changed";
            // 
            // txtLastChanged
            // 
            txtLastChanged.Location = new Point(3, 463);
            txtLastChanged.Name = "txtLastChanged";
            txtLastChanged.ReadOnly = true;
            txtLastChanged.Size = new Size(469, 23);
            txtLastChanged.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.7451F));
            tableLayoutPanel1.Size = new Size(576, 508);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // tbMain
            // 
            tbMain.Controls.Add(tbpMain);
            tbMain.Controls.Add(tbpEmailList);
            tbMain.Dock = DockStyle.Fill;
            tbMain.Location = new Point(0, 25);
            tbMain.Name = "tbMain";
            tbMain.SelectedIndex = 0;
            tbMain.Size = new Size(590, 542);
            tbMain.TabIndex = 4;
            // 
            // tbpMain
            // 
            tbpMain.Controls.Add(tableLayoutPanel1);
            tbpMain.Location = new Point(4, 24);
            tbpMain.Name = "tbpMain";
            tbpMain.Padding = new Padding(3);
            tbpMain.Size = new Size(582, 514);
            tbpMain.TabIndex = 0;
            tbpMain.Text = "Main Info";
            tbpMain.UseVisualStyleBackColor = true;
            // 
            // tbpEmailList
            // 
            tbpEmailList.Controls.Add(tableLayoutPanel2);
            tbpEmailList.Location = new Point(4, 24);
            tbpEmailList.Name = "tbpEmailList";
            tbpEmailList.Padding = new Padding(3);
            tbpEmailList.Size = new Size(582, 514);
            tbpEmailList.TabIndex = 1;
            tbpEmailList.Text = "Email Forward List";
            tbpEmailList.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(panel1, 0, 0);
            tableLayoutPanel2.Controls.Add(dgvEmailList, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 11.2204723F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 88.7795258F));
            tableLayoutPanel2.Size = new Size(576, 508);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRemoveEmail);
            panel1.Controls.Add(txtAddEmail);
            panel1.Controls.Add(lblAddEmail);
            panel1.Controls.Add(btnAddEmail);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(570, 51);
            panel1.TabIndex = 0;
            // 
            // btnRemoveEmail
            // 
            btnRemoveEmail.Location = new Point(501, 9);
            btnRemoveEmail.Name = "btnRemoveEmail";
            btnRemoveEmail.Size = new Size(66, 23);
            btnRemoveEmail.TabIndex = 3;
            btnRemoveEmail.Text = "Remove";
            btnRemoveEmail.UseVisualStyleBackColor = true;
            btnRemoveEmail.Click += btnRemoveEmail_Click;
            // 
            // txtAddEmail
            // 
            txtAddEmail.Location = new Point(57, 9);
            txtAddEmail.Name = "txtAddEmail";
            txtAddEmail.Size = new Size(370, 23);
            txtAddEmail.TabIndex = 2;
            // 
            // lblAddEmail
            // 
            lblAddEmail.AutoSize = true;
            lblAddEmail.Location = new Point(13, 12);
            lblAddEmail.Name = "lblAddEmail";
            lblAddEmail.Size = new Size(36, 15);
            lblAddEmail.TabIndex = 1;
            lblAddEmail.Text = "Email";
            // 
            // btnAddEmail
            // 
            btnAddEmail.Location = new Point(433, 9);
            btnAddEmail.Name = "btnAddEmail";
            btnAddEmail.Size = new Size(62, 23);
            btnAddEmail.TabIndex = 0;
            btnAddEmail.Text = "Add";
            btnAddEmail.UseVisualStyleBackColor = true;
            btnAddEmail.Click += btnAddEmail_Click;
            // 
            // dgvEmailList
            // 
            dgvEmailList.AllowUserToAddRows = false;
            dgvEmailList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmailList.Dock = DockStyle.Fill;
            dgvEmailList.Location = new Point(3, 60);
            dgvEmailList.MultiSelect = false;
            dgvEmailList.Name = "dgvEmailList";
            dgvEmailList.ReadOnly = true;
            dgvEmailList.Size = new Size(570, 445);
            dgvEmailList.TabIndex = 1;
            // 
            // FrmEditTimeZone
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(590, 589);
            Controls.Add(tbMain);
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
            tbMain.ResumeLayout(false);
            tbpMain.ResumeLayout(false);
            tbpEmailList.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmailList).EndInit();
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
        private Label lblTimeZoneObservesDST;
        private Label label7;
        private Label label8;
        private TextBox txtDSTStarts;
        private TextBox txtDSTEnds;
        private Label label6;
        private TextBox txtLastChanged;
        private CheckBox chkIsActive;
        private Label label10;
        private NumericUpDown nudNotifyDaysBefore;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbCloseOnSave;
        private ColorDialog colorDialog1;
        private TextBox txtComments;
        private Label label5;
        private TabControl tbMain;
        private TabPage tbpMain;
        private TabPage tbpEmailList;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel1;
        private TextBox txtAddEmail;
        private Label lblAddEmail;
        private Button btnAddEmail;
        private DataGridView dgvEmailList;
        private Button btnRemoveEmail;
    }
}