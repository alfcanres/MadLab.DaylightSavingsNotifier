
using DSTN.AdminApp.WinForms.Forms.Notifications;
using DSTN.AdminApp.WinForms.TimeZones;

namespace DSTN.AdminApp.WinForms.Notifications
{
    public partial class FrmEditNotification : Form, IEditNotification
    {
        private readonly NotificationPresenter _presenter;
        public FrmEditNotification(NotificationPresenter presenter)
        {
            _presenter = presenter;

            InitializeComponent();
        }

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public int Id
        {
            get { return Convert.ToInt32(txtId.Text); }
            set { txtId.Text = value.ToString(); }
        }
        

        public bool ShowSaveButtom { set; get; }

        public bool ShowDeleteButton { set; get; }

        public bool CloseOnSave { set; get; }
        public int TimeZoneId { set; get; }
  
        public string TimeZoneDisplayName {
            get { return txtTimeZoneDisplayName.Text; }
            set { txtTimeZoneDisplayName.Text = value; }
        }
        public string DSTTransition
        {
            get { return txtDSTTransition.Text; }
            set { txtDSTTransition.Text = value.ToString(); }            
        }
        public string NotifyDate
        {
            get { return txtNotifyDate.Text; }
            set { txtNotifyDate.Text = value.ToString(); }
        }
        public string Message
        {
            get { return txtMessage.Text; }
            set { txtMessage.Text = value.ToString(); }
        }
        public string CreatedAt
        {
            get { return txtCreatedAt.Text; }
            set { txtCreatedAt.Text = value.ToString(); }
        }
        public bool WasRead
        {
            get { return chkWasRead.Checked; }
            set { chkWasRead.Checked = value; }
        }
        public string ReadAt
        {
            get { return txtReadAt.Text; }
            set { txtReadAt.Text = value.ToString(); }
        }

        public bool ConfirmDelete(string alert)
        {
            return MessageBox.Show(alert, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        public void HideLoading()
        {
            Cursor.Current = Cursors.Default;
            lblLoadingStatus.Visible = false;

        }

        public void ShowAlert(string alert)
        {
            MessageBox.Show(alert, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowErrors(IEnumerable<string> errors)
        {
            MessageBox.Show(string.Join(Environment.NewLine, errors), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowErrors(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public void ShowForm()
        {
            this.ShowDialog();
        }

        public void ShowLoading(string message = "")
        {
            Cursor.Current = Cursors.WaitCursor;
            lblLoadingStatus.Text = message;
            lblLoadingStatus.Visible = true;
        }

        public void CloseForm()
        {
            this.Close();
        }

    }
}
