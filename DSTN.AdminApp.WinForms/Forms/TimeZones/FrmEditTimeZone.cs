
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
            set
            {
                txtColor.Text = value;
                try
                {
                    txtColor.BackColor = System.Drawing.ColorTranslator.FromHtml(value);
                }
                catch
                {

                }
            }
        }
        public string DisplayName
        {
            get { return txtDisplayName.Text; }
            set { txtDisplayName.Text = value; }

        }
        public string Comments
        {
            get { return txtComments.Text; }
            set { txtComments.Text = value; }

        }
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

        private List<string> _emailList;

        public List<string> EmailList
        {
            get { return _emailList; }
            set
            {
                _emailList = value;
                dgvEmailList.DataSource = null;
                dgvEmailList.DataSource = _emailList
                    .Select(t => new { Email = t })
                    .ToList();

                if(dgvEmailList.Columns.Count > 0)
                    dgvEmailList.Columns[0].Width = 350;
            }
        }

        public string EmailToAdd
        {
            get { return txtAddEmail.Text; }
            set { txtAddEmail.Text = value; }
        }

        private bool _closeOnSave = false;
        public bool CloseOnSave
        {
            get { return _closeOnSave; }
            set
            {
                _closeOnSave = value;
                if (_closeOnSave)
                {
                    tsbCloseOnSave.Checked = true;
                    tsbCloseOnSave.Text = "Close on save";
                    tsbCloseOnSave.ToolTipText = "Close form on save is enabled";
                }
                else
                {
                    tsbCloseOnSave.Text = "Don't close on save";
                    tsbCloseOnSave.Checked = false;
                    tsbCloseOnSave.ToolTipText = "Close form on save is disabled";
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
            this.Close();
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            await _timeZonePresenter.CreateNewAsync();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await _timeZonePresenter.SaveAsync();
            tbMain.SelectedTab = tbpMain;

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            await _timeZonePresenter.DeleteFromEditorAsync();
        }

        private void txtColor_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsbCloseOnSave_Click(object sender, EventArgs e)
        {
            CloseOnSave = !_closeOnSave;
        }

        private void txtColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                txtColor.Text = System.Drawing.ColorTranslator.ToHtml(this.colorDialog1.Color);
                txtColor.BackColor = this.colorDialog1.Color;
            }
        }

        private void btnAddEmail_Click(object sender, EventArgs e)
        {
            this._timeZonePresenter.AddEmail();
        }

        private void btnRemoveEmail_Click(object sender, EventArgs e)
        {
            if (dgvEmailList.CurrentRow is not null)
            {
                var email = dgvEmailList.CurrentRow.Cells[0].Value.ToString();
                this._timeZonePresenter.RemoveEmail(email);
            }
        }
    }
}
