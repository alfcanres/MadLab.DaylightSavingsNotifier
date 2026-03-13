namespace DSTN.AdminApp.WinForms.Forms.EmailConfigurator;

public partial class FrmEditEmailConfigurator : Form, IEditEmailConfigurator
{
    private readonly EmailConfiguratorPresenter _presenter;

    public FrmEditEmailConfigurator(EmailConfiguratorPresenter presenter)
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

    public string ConfigName
    {
        get { return txtName.Text; }
        set { txtName.Text = value; }
    }

    public string SmtpHost
    {
        get { return txtSmtpHost.Text; }
        set { txtSmtpHost.Text = value; }
    }

    public int SmtpPort
    {
        get { return Convert.ToInt32(nudSmtpPort.Value); }
        set { nudSmtpPort.Value = value; }
    }

    public bool UseSsl
    {
        get { return chkUseSsl.Checked; }
        set { chkUseSsl.Checked = value; }
    }

    public bool UseStartTls
    {
        get { return chkUseStartTls.Checked; }
        set { chkUseStartTls.Checked = value; }
    }

    public string SenderName
    {
        get { return txtSenderName.Text; }
        set { txtSenderName.Text = value; }
    }

    public string SenderEmail
    {
        get { return txtSenderEmail.Text; }
        set { txtSenderEmail.Text = value; }
    }

    public string Username
    {
        get { return txtUsername.Text; }
        set { txtUsername.Text = value; }
    }

    public string Password
    {
        get { return txtPassword.Text; }
        set { txtPassword.Text = value; }
    }

    public bool IsActive
    {
        get { return chkIsActive.Checked; }
        set { chkIsActive.Checked = value; }
    }

    public bool IsDefault
    {
        get { return chkIsDefault.Checked; }
        set { chkIsDefault.Checked = value; }
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

    private async void btnSave_Click(object sender, EventArgs e)
    {
        await _presenter.SaveAsync();
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        await _presenter.DeleteFromEditorAsync();
    }

    private void tsbCloseOnSave_Click(object sender, EventArgs e)
    {
        CloseOnSave = !_closeOnSave;
    }
}
