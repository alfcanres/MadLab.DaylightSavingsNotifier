namespace DSTN.AdminApp.WinForms.Forms.EmailConfigurator
{
    partial class FrmEditEmailConfigurator
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
            lblLoadingStatus = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            btnSave = new ToolStripButton();
            btnDelete = new ToolStripButton();
            tsbCloseOnSave = new ToolStripButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblName = new Label();
            txtName = new TextBox();
            lblSmtpHost = new Label();
            txtSmtpHost = new TextBox();
            lblSmtpPort = new Label();
            nudSmtpPort = new NumericUpDown();
            chkUseSsl = new CheckBox();
            chkUseStartTls = new CheckBox();
            lblSenderName = new Label();
            txtSenderName = new TextBox();
            lblSenderEmail = new Label();
            txtSenderEmail = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            chkIsActive = new CheckBox();
            chkIsDefault = new CheckBox();
            txtId = new TextBox();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSmtpPort).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblLoadingStatus });
            statusStrip1.Location = new Point(0, 539);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(484, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblLoadingStatus
            // 
            lblLoadingStatus.Name = "lblLoadingStatus";
            lblLoadingStatus.Size = new Size(59, 17);
            lblLoadingStatus.Text = "Loading...";
            lblLoadingStatus.Visible = false;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnSave, btnDelete, tsbCloseOnSave });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(484, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            btnSave.Image = Properties.Resources.diskette;
            btnSave.ImageTransparentColor = Color.Magenta;
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(51, 22);
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Image = Properties.Resources.delete;
            btnDelete.ImageTransparentColor = Color.Magenta;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(60, 22);
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // tsbCloseOnSave
            // 
            tsbCloseOnSave.CheckOnClick = true;
            tsbCloseOnSave.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbCloseOnSave.Name = "tsbCloseOnSave";
            tsbCloseOnSave.Size = new Size(114, 22);
            tsbCloseOnSave.Text = "Don't close on save";
            tsbCloseOnSave.Click += tsbCloseOnSave_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(484, 514);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(lblName);
            flowLayoutPanel1.Controls.Add(txtName);
            flowLayoutPanel1.Controls.Add(lblSmtpHost);
            flowLayoutPanel1.Controls.Add(txtSmtpHost);
            flowLayoutPanel1.Controls.Add(lblSmtpPort);
            flowLayoutPanel1.Controls.Add(nudSmtpPort);
            flowLayoutPanel1.Controls.Add(chkUseSsl);
            flowLayoutPanel1.Controls.Add(chkUseStartTls);
            flowLayoutPanel1.Controls.Add(lblSenderName);
            flowLayoutPanel1.Controls.Add(txtSenderName);
            flowLayoutPanel1.Controls.Add(lblSenderEmail);
            flowLayoutPanel1.Controls.Add(txtSenderEmail);
            flowLayoutPanel1.Controls.Add(lblUsername);
            flowLayoutPanel1.Controls.Add(txtUsername);
            flowLayoutPanel1.Controls.Add(lblPassword);
            flowLayoutPanel1.Controls.Add(txtPassword);
            flowLayoutPanel1.Controls.Add(chkIsActive);
            flowLayoutPanel1.Controls.Add(chkIsDefault);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.Size = new Size(478, 508);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblName.Location = new Point(13, 10);
            lblName.Name = "lblName";
            lblName.Size = new Size(42, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            // 
            // txtName
            // 
            txtName.Location = new Point(13, 28);
            txtName.Name = "txtName";
            txtName.Size = new Size(440, 23);
            txtName.TabIndex = 1;
            // 
            // lblSmtpHost
            // 
            lblSmtpHost.AutoSize = true;
            lblSmtpHost.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSmtpHost.Location = new Point(13, 54);
            lblSmtpHost.Name = "lblSmtpHost";
            lblSmtpHost.Size = new Size(70, 15);
            lblSmtpHost.TabIndex = 2;
            lblSmtpHost.Text = "SMTP Host:";
            // 
            // txtSmtpHost
            // 
            txtSmtpHost.Location = new Point(13, 72);
            txtSmtpHost.Name = "txtSmtpHost";
            txtSmtpHost.Size = new Size(440, 23);
            txtSmtpHost.TabIndex = 3;
            // 
            // lblSmtpPort
            // 
            lblSmtpPort.AutoSize = true;
            lblSmtpPort.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSmtpPort.Location = new Point(13, 98);
            lblSmtpPort.Name = "lblSmtpPort";
            lblSmtpPort.Size = new Size(67, 15);
            lblSmtpPort.TabIndex = 4;
            lblSmtpPort.Text = "SMTP Port:";
            // 
            // nudSmtpPort
            // 
            nudSmtpPort.Location = new Point(13, 116);
            nudSmtpPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            nudSmtpPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSmtpPort.Name = "nudSmtpPort";
            nudSmtpPort.Size = new Size(120, 23);
            nudSmtpPort.TabIndex = 5;
            nudSmtpPort.Value = new decimal(new int[] { 587, 0, 0, 0 });
            // 
            // chkUseSsl
            // 
            chkUseSsl.AutoSize = true;
            chkUseSsl.Location = new Point(13, 145);
            chkUseSsl.Name = "chkUseSsl";
            chkUseSsl.Size = new Size(65, 19);
            chkUseSsl.TabIndex = 6;
            chkUseSsl.Text = "Use SSL";
            chkUseSsl.UseVisualStyleBackColor = true;
            // 
            // chkUseStartTls
            // 
            chkUseStartTls.AutoSize = true;
            chkUseStartTls.Location = new Point(13, 170);
            chkUseStartTls.Name = "chkUseStartTls";
            chkUseStartTls.Size = new Size(95, 19);
            chkUseStartTls.TabIndex = 7;
            chkUseStartTls.Text = "Use Start TLS";
            chkUseStartTls.UseVisualStyleBackColor = true;
            // 
            // lblSenderName
            // 
            lblSenderName.AutoSize = true;
            lblSenderName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSenderName.Location = new Point(13, 192);
            lblSenderName.Name = "lblSenderName";
            lblSenderName.Size = new Size(84, 15);
            lblSenderName.TabIndex = 8;
            lblSenderName.Text = "Sender Name:";
            // 
            // txtSenderName
            // 
            txtSenderName.Location = new Point(13, 210);
            txtSenderName.Name = "txtSenderName";
            txtSenderName.Size = new Size(440, 23);
            txtSenderName.TabIndex = 9;
            // 
            // lblSenderEmail
            // 
            lblSenderEmail.AutoSize = true;
            lblSenderEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSenderEmail.Location = new Point(13, 236);
            lblSenderEmail.Name = "lblSenderEmail";
            lblSenderEmail.Size = new Size(82, 15);
            lblSenderEmail.TabIndex = 10;
            lblSenderEmail.Text = "Sender Email:";
            // 
            // txtSenderEmail
            // 
            txtSenderEmail.Location = new Point(13, 254);
            txtSenderEmail.Name = "txtSenderEmail";
            txtSenderEmail.Size = new Size(440, 23);
            txtSenderEmail.TabIndex = 11;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsername.Location = new Point(13, 280);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(65, 15);
            lblUsername.TabIndex = 12;
            lblUsername.Text = "Username:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(13, 298);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(440, 23);
            txtUsername.TabIndex = 13;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.Location = new Point(13, 324);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(62, 15);
            lblPassword.TabIndex = 14;
            lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(13, 342);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(440, 23);
            txtPassword.TabIndex = 15;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(13, 371);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(69, 19);
            chkIsActive.TabIndex = 16;
            chkIsActive.Text = "Is Active";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // chkIsDefault
            // 
            chkIsDefault.AutoSize = true;
            chkIsDefault.Location = new Point(13, 396);
            chkIsDefault.Name = "chkIsDefault";
            chkIsDefault.Size = new Size(77, 19);
            chkIsDefault.TabIndex = 17;
            chkIsDefault.Text = "Is Default";
            chkIsDefault.UseVisualStyleBackColor = true;
            // 
            // txtId
            // 
            txtId.Location = new Point(0, 0);
            txtId.Name = "txtId";
            txtId.Size = new Size(0, 23);
            txtId.TabIndex = 99;
            txtId.Visible = false;
            txtId.Text = "0";
            // 
            // FrmEditEmailConfigurator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 561);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Controls.Add(txtId);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FrmEditEmailConfigurator";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Email Configuration";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSmtpPort).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblLoadingStatus;
        private ToolStrip toolStrip1;
        private ToolStripButton btnSave;
        private ToolStripButton btnDelete;
        private ToolStripButton tsbCloseOnSave;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblName;
        private TextBox txtName;
        private Label lblSmtpHost;
        private TextBox txtSmtpHost;
        private Label lblSmtpPort;
        private NumericUpDown nudSmtpPort;
        private CheckBox chkUseSsl;
        private CheckBox chkUseStartTls;
        private Label lblSenderName;
        private TextBox txtSenderName;
        private Label lblSenderEmail;
        private TextBox txtSenderEmail;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkIsActive;
        private CheckBox chkIsDefault;
        private TextBox txtId;
    }
}
