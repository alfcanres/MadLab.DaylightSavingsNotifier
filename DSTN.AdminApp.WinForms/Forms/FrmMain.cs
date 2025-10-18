using DSTN.AdminApp.WinForms.Forms.Help;
using DSTN.AdminApp.WinForms.Notifications;
using DSTN.AdminApp.WinForms.Properties;
using DSTN.AdminApp.WinForms.Repository.Notifications;
using DSTN.AdminApp.WinForms.TimeZones;

namespace DSTN.AdminApp.WinForms
{
    public partial class FrmMain : Form
    {
        private FrmListTimeZones _frmTimeZones;
        private FrmListNotifications _frmListNotifications;
        private readonly IServiceProvider _serviceProvider;
        private readonly INotficationsService _notficationsService;
        private int _countPendingNotifications = 0;

        public FrmMain(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void tsmTimeZones_Click(object sender, EventArgs e)
        {
            if (_frmTimeZones == null || _frmTimeZones.IsDisposed)
            {
                _frmTimeZones = new FrmListTimeZones(_serviceProvider);
                _frmTimeZones.Show();
            }
            else
            {
                _frmTimeZones.BringToFront();
            }
        }

        private void tsmNotifications_Click(object sender, EventArgs e)
        {
            if (_frmListNotifications == null || _frmListNotifications.IsDisposed)
            {
                _frmListNotifications = new FrmListNotifications(_serviceProvider);
                _frmListNotifications.Show();
            }
            else
            {
                _frmListNotifications.BringToFront();
            }
        }

        private async void timerNotifications_Tick(object sender, EventArgs e)
        {
            var res = await _notficationsService.CountUnread();
            if (res.Status == ViewModels.ResultStatus.Success)
            {
                if (res.Data != this._countPendingNotifications)
                {
                    int totalNewNotifications = res.Data - _countPendingNotifications;
                    _countPendingNotifications = res.Data;
                    tsbNotificationsLabel.Visible = true;
                    tsbNotificationsLabel.Text = $"{totalNewNotifications} new notifications pending to read!";
                }
                else
                {
                    tsbNotificationsLabel.Visible = false;
                    tsbNotificationsLabel.Text = string.Empty;
                }
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //timerNotifications.Interval = Settings.Default.ScanForNotificationsIntervalMlsc;
            //timerNotifications.Start();
        }

        private void tsmAbout_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog();
        }

        private void tsmRepo_Click(object sender, EventArgs e)
        {

            const string repoUrl = "https://github.com/alfcanres/MadLab.DaylightSavingsNotifier";
            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = repoUrl,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open browser: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
