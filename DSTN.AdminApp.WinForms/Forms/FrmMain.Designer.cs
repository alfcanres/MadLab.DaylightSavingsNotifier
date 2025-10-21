namespace DSTN.AdminApp.WinForms
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            menuMain = new MenuStrip();
            tsmTimeZones = new ToolStripMenuItem();
            tsmNotifications = new ToolStripMenuItem();
            tsmHelp = new ToolStripMenuItem();
            tsmAbout = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            tsmRepo = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            tsbNotificationsLabel = new ToolStripStatusLabel();
            menuMain.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuMain
            // 
            menuMain.Items.AddRange(new ToolStripItem[] { tsmTimeZones, tsmNotifications, tsmHelp });
            menuMain.Location = new Point(0, 0);
            menuMain.Name = "menuMain";
            menuMain.Size = new Size(819, 40);
            menuMain.TabIndex = 0;
            menuMain.Text = "menuStrip1";
            // 
            // tsmTimeZones
            // 
            tsmTimeZones.Image = Properties.Resources.world;
            tsmTimeZones.ImageScaling = ToolStripItemImageScaling.None;
            tsmTimeZones.Name = "tsmTimeZones";
            tsmTimeZones.Size = new Size(111, 36);
            tsmTimeZones.Text = "Time zones";
            tsmTimeZones.Click += tsmTimeZones_Click;
            // 
            // tsmNotifications
            // 
            tsmNotifications.Image = Properties.Resources.notification;
            tsmNotifications.ImageScaling = ToolStripItemImageScaling.None;
            tsmNotifications.Name = "tsmNotifications";
            tsmNotifications.Size = new Size(136, 36);
            tsmNotifications.Text = "Notifications (0)";
            tsmNotifications.Click += tsmNotifications_Click;
            // 
            // tsmHelp
            // 
            tsmHelp.DropDownItems.AddRange(new ToolStripItem[] { tsmAbout, toolStripSeparator1, tsmRepo });
            tsmHelp.Image = Properties.Resources.question;
            tsmHelp.ImageScaling = ToolStripItemImageScaling.None;
            tsmHelp.Name = "tsmHelp";
            tsmHelp.Size = new Size(76, 36);
            tsmHelp.Text = "Help";
            // 
            // tsmAbout
            // 
            tsmAbout.Image = Properties.Resources.info;
            tsmAbout.ImageScaling = ToolStripItemImageScaling.None;
            tsmAbout.Name = "tsmAbout";
            tsmAbout.Size = new Size(158, 38);
            tsmAbout.Text = "About";
            tsmAbout.Click += tsmAbout_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(155, 6);
            // 
            // tsmRepo
            // 
            tsmRepo.Image = Properties.Resources.github;
            tsmRepo.ImageScaling = ToolStripItemImageScaling.None;
            tsmRepo.Name = "tsmRepo";
            tsmRepo.Size = new Size(158, 38);
            tsmRepo.Text = "GitHub Repo";
            tsmRepo.Click += tsmRepo_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsbNotificationsLabel });
            statusStrip1.Location = new Point(0, 305);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(819, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsbNotificationsLabel
            // 
            tsbNotificationsLabel.Image = Properties.Resources.notification;
            tsbNotificationsLabel.Name = "tsbNotificationsLabel";
            tsbNotificationsLabel.Size = new Size(16, 17);
            tsbNotificationsLabel.Visible = false;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(819, 327);
            Controls.Add(statusStrip1);
            Controls.Add(menuMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuMain;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MadLab Awesome DST Notifier";
            Load += FrmMain_Load;
            menuMain.ResumeLayout(false);
            menuMain.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuMain;
        private ToolStripMenuItem tsmTimeZones;
        private ToolStripMenuItem tsmNotifications;
        private ToolStripMenuItem tsmHelp;
        private ToolStripMenuItem tsmAbout;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmRepo;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsbNotificationsLabel;
    }
}