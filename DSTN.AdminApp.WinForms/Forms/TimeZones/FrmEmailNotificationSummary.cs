using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSTN.AdminApp.WinForms.Forms.TimeZones
{
    public partial class FrmEmailNotificationSummary : Form, IEmailNotificationSummary
    {
        private readonly TimeZonePresenter _presenter;
        public FrmEmailNotificationSummary(TimeZonePresenter presenter)
        {
            _presenter = presenter;
            InitializeComponent();
        }

        public string Progress
        {
            get { return lblProgress.Text; }
            set { lblProgress.Text = value; }
        }

        public bool IsEnabled {
            get { return btnSend.Enabled; }
            set { btnSend.Enabled = value; }
        }
        public string SendButtonText 
        {
            get { return btnSend.Text; }
            set { btnSend.Text = value; }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {

            await _presenter.SendNotificationSummary();
        }

        public void ClearMessages()
        {
            lbMessages.Items.Clear();
        }

        public void AddMessage(string message)
        {
            lbMessages.Items.Add(message);

            // Auto-scroll to the latest message
            if (lbMessages.Items.Count > 0)
            {
                lbMessages.TopIndex = lbMessages.Items.Count - 1;
            }
        }
    }
}
