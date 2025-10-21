using DSTN.AdminApp.WinForms.Forms;
using DSTN.AdminApp.WinForms.Forms.Help;
using DSTN.AdminApp.WinForms.Notifications;
using DSTN.AdminApp.WinForms.Repository.Notifications;
using DSTN.AdminApp.WinForms.TimeZones;
using Microsoft.Extensions.DependencyInjection;

namespace DSTN.AdminApp.WinForms
{
    public partial class FrmMain : Form, IMain
    {
        private FrmListTimeZones _frmTimeZones;
        private FrmListNotifications _frmListNotifications;
        private readonly IServiceProvider _serviceProvider;
        private readonly MainPresenter _mainPresenter;

        public string NotificationsText { get => tsbNotificationsLabel.Text; set => tsbNotificationsLabel.Text = value; }
        public string NotificationsTitleMenu { get => tsmNotifications.Text; set => tsmNotifications.Text = value; }

        private int _pendingNotifications = 0;
        public int PendingNotifications { get => _pendingNotifications; set => _pendingNotifications = value; }


        public FrmMain(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var service = _serviceProvider.GetRequiredService<INotficationsService>();
            _mainPresenter = new MainPresenter(this, service);
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

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            await _mainPresenter.InitNotifier();
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
