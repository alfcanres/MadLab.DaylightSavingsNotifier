using DSTN.AdminApp.WinForms.Repository.EmailConfigurator;
using DSTN.AdminApp.WinForms.ViewModels.EmailConfigurator;
using Microsoft.Extensions.DependencyInjection;

namespace DSTN.AdminApp.WinForms.Forms.EmailConfigurator;

public partial class FrmListEmailConfigurator : Form, IListEmailConfigurator
{
    IEnumerable<EmailConfiguration> _emailConfigurations;
    private readonly EmailConfiguratorPresenter _presenter;
    private FrmEditEmailConfigurator _frmEditor;

    public string Title
    {
        get { return this.Text; }
        set { this.Text = value; }
    }

    public IEnumerable<EmailConfiguration> EmailConfigurations
    {
        get { return _emailConfigurations; }
        set
        {
            _emailConfigurations = value;
            dataGridView1.DataSource = _emailConfigurations.ToList();
        }
    }

    public IEditEmailConfigurator EditorForm { get; set; }

    public int SelectedId
    {
        get
        {
            if (dataGridView1.SelectedRows.Count != 0)
                return (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
            else
                return 0;
        }
    }

    private void CreateEditor()
    {
        _frmEditor = new FrmEditEmailConfigurator(_presenter);
        _presenter.SetEditor(_frmEditor);
    }

    public FrmListEmailConfigurator(IServiceProvider serviceProvider)
    {
        var emailConfiguratorService = serviceProvider.GetRequiredService<IEmailConfiguratorService>();
        _presenter = new EmailConfiguratorPresenter(this, emailConfiguratorService);

        InitializeComponent();
        dataGridView1.AutoGenerateColumns = false;

        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Id",
            DataPropertyName = "Id",
            HeaderText = "ID",
            Width = 50,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Name",
            HeaderText = "Name",
            Width = 150,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "SmtpHost",
            HeaderText = "SMTP Host",
            Width = 150,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "SmtpPort",
            HeaderText = "SMTP Port",
            Width = 80,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = "UseSsl",
            HeaderText = "Use SSL",
            Width = 70,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = "UseStartTls",
            HeaderText = "Start TLS",
            Width = 70,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "SenderName",
            HeaderText = "Sender Name",
            Width = 120,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "SenderEmail",
            HeaderText = "Sender Email",
            Width = 150,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Username",
            HeaderText = "Username",
            Width = 120,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = "IsActive",
            HeaderText = "Is Active",
            Width = 70,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = "IsDefault",
            HeaderText = "Is Default",
            Width = 70,
            ReadOnly = true
        });

        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "CreatedAt",
            HeaderText = "Created At",
            Width = 150,
            ReadOnly = true,
            DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True, Format = "dddd, d MMMM, yyyy" }
        });

        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.MultiSelect = false;
    }

    private async void FrmListEmailConfigurator_Load(object sender, EventArgs e)
    {
        await _presenter.IntializeListForm();
        dataGridView1.AutoGenerateColumns = false;
    }

    public bool CloseForm()
    {
        this.Close();
        return true;
    }

    public void HideEditor()
    {
        EditorForm.CloseForm();
    }

    public void HideLoading()
    {
        Cursor.Current = Cursors.Default;
        tsLblStatus.Visible = false;
    }

    public void ShowLoading(string message = "")
    {
        Cursor.Current = Cursors.WaitCursor;
        if (string.IsNullOrWhiteSpace(message))
        {
            message = "Loading...";
        }
        tsLblStatus.Text = message;
        tsLblStatus.Visible = true;
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

    public bool ConfirmDelete(string alert)
    {
        return MessageBox.Show(alert, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
    }

    public void ShowPager()
    {
        // No pagination needed for email configurations
    }

    public void HidePager()
    {
        // No pagination needed for email configurations
    }

    private async void tsbRefresh_Click(object sender, EventArgs e)
    {
        await _presenter.LoadListAsync();
    }

    private async void tsbAddNew_Click(object sender, EventArgs e)
    {
        CreateEditor();
        await _presenter.CreateNewAsync();
    }

    private async void tsbEdit_Click(object sender, EventArgs e)
    {
        CreateEditor();
        await _presenter.EditSelectedAsync();
    }

    private async void tsbDelete_Click(object sender, EventArgs e)
    {
        await _presenter.DeleteFromListAsync();
    }

    private async void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        CreateEditor();
        await _presenter.EditSelectedAsync();
    }
}
