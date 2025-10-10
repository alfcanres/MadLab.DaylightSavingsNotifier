
using DSTN.AdminApp.WinForms.Forms.TimeZones;

namespace DSTN.AdminApp.WinForms.TimeZones
{
    public partial class FrmEditTimeZone : Form, IEditTimeZone
    {
        private readonly TimeZonePresenter _timeZonePresenter;
        public FrmEditTimeZone(TimeZonePresenter timeZonePresenter)
        {
            _timeZonePresenter = timeZonePresenter;

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
        public string Color
        {
            get { return txtColor.Text; }
            set { txtColor.Text = value; }
        }
        public string DisplayName
        {
            get { return txtDisplayName.Text; }
            set { txtDisplayName.Text = value; }

        }
        public string Comments { get; set; }
        public string DSTStarts
        {
            get { return txtDSTStarts.Text; }
            set { txtDSTStarts.Text = value; }
        }
        public string DSTEnds
        {
            get { return txtDSTEnds.Text; }
            set { txtDSTEnds.Text = value; }
        }
        public string LastChanged
        {
            get { return txtLastChanged.Text; }
            set { txtLastChanged.Text = value; }
        }
        public string TimeZoneObservesDST
        {
            get { return lblTimeZoneObservesDST.Text; }
            set { lblTimeZoneObservesDST.Text = value; }
        }
        public string NextNotifyDate
        {
            get { return txtNextNotifyDate.Text; }
            set { txtNextNotifyDate.Text = value; }
        }
        public bool IsActive
        {
            get { return chkIsActive.Checked; }
            set { chkIsActive.Checked = value; }
        }
        public int NotifyDaysBefore
        {
            get { return Convert.ToInt32(nudNotifyDaysBefore.Text); }
            set { nudNotifyDaysBefore.Text = value.ToString(); }
        }

        public bool ShowSaveButtom
        {
            set { btnSave.Visible = value; }
            get { return btnSave.Visible; }
        }
        public bool ShowDeleteButton
        {
            set { btnDelete.Visible = value; }
            get { return btnDelete.Visible; }
        }

        public string SelectedTimeZoneId
        {
            get { return cboTimeZoneId.SelectedItem?.ToString() ?? string.Empty; }
            set { cboTimeZoneId.SelectedItem = value; }
        }


        private List<string> _systemTimeZones;

        public List<string> SystemTimeZones
        {
            get { return _systemTimeZones; }
            set
            {
                _systemTimeZones = value;
                cboTimeZoneId.Items.Clear();
                cboTimeZoneId.Items.AddRange(_systemTimeZones.ToArray());
                if (cboTimeZoneId.Items.Count > 0)
                {
                    cboTimeZoneId.SelectedIndex = 0;
                }
            }
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
            this.Hide();
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            await _timeZonePresenter.CreateNewAsync();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await _timeZonePresenter.SaveAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            await _timeZonePresenter.DeleteFromEditorAsync();
        }

        private void txtColor_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
