namespace DSTN.AdminApp.WinForms.Forms.TimeZones
{
    partial class FrmEmailNotificationSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmailNotificationSummary));
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblProgress = new Label();
            lbMessages = new ListBox();
            btnSend = new Button();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(lblProgress);
            flowLayoutPanel1.Controls.Add(lbMessages);
            flowLayoutPanel1.Controls.Add(btnSend);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(376, 445);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(3, 0);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(74, 15);
            lblProgress.TabIndex = 0;
            lblProgress.Text = "Progress 0 %";
            // 
            // lbMessages
            // 
            lbMessages.FormattingEnabled = true;
            lbMessages.ItemHeight = 15;
            lbMessages.Location = new Point(3, 18);
            lbMessages.Name = "lbMessages";
            lbMessages.Size = new Size(361, 379);
            lbMessages.TabIndex = 1;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(3, 403);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(361, 30);
            btnSend.TabIndex = 2;
            btnSend.Text = "Send Email";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // FrmEmailNotificationSummary
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(376, 445);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmEmailNotificationSummary";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Email Notification Summary";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblProgress;
        private ListBox lbMessages;
        private Button btnSend;
    }
}