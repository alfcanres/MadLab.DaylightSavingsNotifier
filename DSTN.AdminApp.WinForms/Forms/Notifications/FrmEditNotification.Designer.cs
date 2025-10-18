namespace DSTN.AdminApp.WinForms.Notifications
{
    partial class FrmEditNotification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditNotification));
            toolStrip1 = new ToolStrip();
            statusStrip1 = new StatusStrip();
            lblLoadingStatus = new ToolStripStatusLabel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            txtId = new TextBox();
            label3 = new Label();
            txtTimeZoneDisplayName = new TextBox();
            chkWasRead = new CheckBox();
            label5 = new Label();
            txtMessage = new TextBox();
            label2 = new Label();
            txtDSTTransition = new TextBox();
            label4 = new Label();
            txtNotifyDate = new TextBox();
            label6 = new Label();
            txtCreatedAt = new TextBox();
            label7 = new Label();
            txtReadAt = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            statusStrip1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(488, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblLoadingStatus });
            statusStrip1.Location = new Point(0, 427);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(488, 22);
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
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(txtTimeZoneDisplayName);
            flowLayoutPanel1.Controls.Add(chkWasRead);
            flowLayoutPanel1.Controls.Add(label5);
            flowLayoutPanel1.Controls.Add(txtMessage);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(txtDSTTransition);
            flowLayoutPanel1.Controls.Add(label4);
            flowLayoutPanel1.Controls.Add(txtNotifyDate);
            flowLayoutPanel1.Controls.Add(label6);
            flowLayoutPanel1.Controls.Add(txtCreatedAt);
            flowLayoutPanel1.Controls.Add(label7);
            flowLayoutPanel1.Controls.Add(txtReadAt);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(482, 396);
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 44);
            label3.Name = "label3";
            label3.Size = new Size(137, 15);
            label3.TabIndex = 4;
            label3.Text = "Time Zone DisplayName";
            // 
            // txtTimeZoneDisplayName
            // 
            txtTimeZoneDisplayName.Location = new Point(3, 62);
            txtTimeZoneDisplayName.Name = "txtTimeZoneDisplayName";
            txtTimeZoneDisplayName.Size = new Size(434, 23);
            txtTimeZoneDisplayName.TabIndex = 5;
            // 
            // chkWasRead
            // 
            chkWasRead.AutoSize = true;
            chkWasRead.Location = new Point(3, 91);
            chkWasRead.Name = "chkWasRead";
            chkWasRead.Size = new Size(82, 19);
            chkWasRead.TabIndex = 8;
            chkWasRead.Text = "Was Read?";
            chkWasRead.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 113);
            label5.Name = "label5";
            label5.Size = new Size(53, 15);
            label5.TabIndex = 12;
            label5.Text = "Message";
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(3, 131);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(458, 76);
            txtMessage.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 210);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 13;
            label2.Text = "DST Transition";
            // 
            // txtDSTTransition
            // 
            txtDSTTransition.Location = new Point(3, 228);
            txtDSTTransition.Name = "txtDSTTransition";
            txtDSTTransition.Size = new Size(434, 23);
            txtDSTTransition.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 254);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 15;
            label4.Text = "Notify Date";
            // 
            // txtNotifyDate
            // 
            txtNotifyDate.Location = new Point(3, 272);
            txtNotifyDate.Name = "txtNotifyDate";
            txtNotifyDate.Size = new Size(434, 23);
            txtNotifyDate.TabIndex = 16;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 298);
            label6.Name = "label6";
            label6.Size = new Size(63, 15);
            label6.TabIndex = 17;
            label6.Text = "Created At";
            // 
            // txtCreatedAt
            // 
            txtCreatedAt.Location = new Point(3, 316);
            txtCreatedAt.Name = "txtCreatedAt";
            txtCreatedAt.Size = new Size(434, 23);
            txtCreatedAt.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 342);
            label7.Name = "label7";
            label7.Size = new Size(48, 15);
            label7.TabIndex = 19;
            label7.Text = "Read At";
            // 
            // txtReadAt
            // 
            txtReadAt.Location = new Point(3, 360);
            txtReadAt.Name = "txtReadAt";
            txtReadAt.Size = new Size(434, 23);
            txtReadAt.TabIndex = 20;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.7451F));
            tableLayoutPanel1.Size = new Size(488, 402);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // FrmEditNotification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(488, 449);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmEditNotification";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmEditNotification";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblLoadingStatus;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private TextBox txtId;
        private Label label3;
        private TextBox txtTimeZoneDisplayName;
        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox chkWasRead;
        private TextBox txtMessage;
        private Label label5;
        private Label label2;
        private TextBox txtDSTTransition;
        private Label label4;
        private TextBox txtNotifyDate;
        private Label label6;
        private TextBox txtCreatedAt;
        private Label label7;
        private TextBox txtReadAt;
    }
}