using DSTN.AdminApp.WinForms.Notifications;
using DSTN.AdminApp.WinForms.Properties;
using DSTN.AdminApp.WinForms.TimeZones;

namespace DSTN.AdminApp.WinForms
{
    public partial class FrmMain : Form
    {
        private FrmListTimeZones _frmTimeZones;
        private FrmListNotifications _frmListNotifications;
        private readonly IServiceProvider _serviceProvider;
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

        private void timerNotifications_Tick(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            timerNotifications.Interval = Settings.Default.ScanForNotificationsIntervalMlsc;
            timerNotifications.Start();



        }
    }
}
