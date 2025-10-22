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
            statusStrip1 = new StatusStrip();
            lblLoadingStatus = new ToolStripStatusLabel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            lblTimeZoneDisplayName = new Label();
            label2 = new Label();
            label5 = new Label();
            txtMessage = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtNextTransition = new TextBox();
            btnColor = new Button();
            statusStrip1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblLoadingStatus });
            statusStrip1.Location = new Point(0, 381);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(622, 22);
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
            flowLayoutPanel1.Controls.Add(panel1);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(txtNextTransition);
            flowLayoutPanel1.Controls.Add(label5);
            flowLayoutPanel1.Controls.Add(txtMessage);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(616, 375);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnColor);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(lblTimeZoneDisplayName);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(603, 37);
            panel1.TabIndex = 22;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.notification;
            pictureBox1.Location = new Point(570, 7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(30, 26);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 22;
            pictureBox1.TabStop = false;
            // 
            // lblTimeZoneDisplayName
            // 
            lblTimeZoneDisplayName.AutoSize = true;
            lblTimeZoneDisplayName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTimeZoneDisplayName.Location = new Point(59, 7);
            lblTimeZoneDisplayName.Name = "lblTimeZoneDisplayName";
            lblTimeZoneDisplayName.Size = new Size(263, 21);
            lblTimeZoneDisplayName.TabIndex = 21;
            lblTimeZoneDisplayName.Text = "Easter Island Time Zone (Mexico)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(3, 43);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 13;
            label2.Text = "Next Transition:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 87);
            label5.Name = "label5";
            label5.Size = new Size(53, 15);
            label5.TabIndex = 12;
            label5.Text = "Message";
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(3, 105);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.ReadOnly = true;
            txtMessage.Size = new Size(600, 235);
            txtMessage.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.7451F));
            tableLayoutPanel1.Size = new Size(622, 381);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // txtNextTransition
            // 
            txtNextTransition.Location = new Point(3, 61);
            txtNextTransition.Name = "txtNextTransition";
            txtNextTransition.ReadOnly = true;
            txtNextTransition.Size = new Size(477, 23);
            txtNextTransition.TabIndex = 23;
            // 
            // btnColor
            // 
            btnColor.BackColor = Color.IndianRed;
            btnColor.Enabled = false;
            btnColor.Location = new Point(6, 8);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(47, 23);
            btnColor.TabIndex = 23;
            btnColor.UseVisualStyleBackColor = false;
            // 
            // FrmEditNotification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 403);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmEditNotification";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmEditNotification";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblLoadingStatus;
        private FlowLayoutPanel flowLayoutPanel1;
        private TextBox txtTimeZoneDisplayName;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtMessage;
        private Label label5;
        private Label label2;
        private Label lblTimeZoneDisplayName;
        private Panel panel1;
        private PictureBox pictureBox1;
        private TextBox txtNextTransition;
        private Button btnColor;
    }
}