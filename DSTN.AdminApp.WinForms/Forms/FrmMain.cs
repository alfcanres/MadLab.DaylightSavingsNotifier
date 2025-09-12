using DSTN.AdminApp.WinForms.TimeZones;

namespace DSTN.AdminApp.WinForms
{
    public partial class FrmMain : Form
    {
        private FrmListTimeZones frmTimeZones;
        private readonly IServiceProvider _serviceProvider;

        public FrmMain(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void tsmTimeZones_Click(object sender, EventArgs e)
        {
            if (frmTimeZones == null || frmTimeZones.IsDisposed)
            {
                frmTimeZones = new FrmListTimeZones(_serviceProvider);
                frmTimeZones.Show();
            }
            else
            {
                frmTimeZones.BringToFront();
            }
        }
    }
}
