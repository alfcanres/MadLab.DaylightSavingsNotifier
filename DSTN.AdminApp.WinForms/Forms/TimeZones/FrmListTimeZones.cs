using DSTN.AdminApp.WinForms.Forms.TimeZones;
using DSTN.AdminApp.WinForms.Repository.SystemTimeZones;
using DSTN.AdminApp.WinForms.Repository.TimeZoneConfigurator;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;
using Microsoft.Extensions.DependencyInjection;


namespace DSTN.AdminApp.WinForms.TimeZones
{
    public partial class FrmListTimeZones : Form, IListTimeZones
    {
        IEnumerable<ObservedTimeZoneForList> _timeZones;
        private readonly TimeZonePresenter _presenter;
        private readonly FrmEditTimeZone _frmEditor;

        public string SearchKeyWord
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        public string SelectedFilter
        {
            get { return cboFilter.SelectedItem?.ToString() ?? string.Empty; }
            set { cboFilter.SelectedItem = value; }
        }
        public IEnumerable<string> Filters
        {
            get { return cboFilter.Items.Cast<string>(); }
            set
            {
                cboFilter.Items.Clear();
                cboFilter.Items.AddRange(value.ToArray());
                if (cboFilter.Items.Count > 0)
                {
                    cboFilter.SelectedIndex = 0;
                }
            }
        }

        public string PageCount
        {
            get { return lblPageCount.Text; }
            set { lblPageCount.Text = value; }
        }
        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }


        public IEnumerable<ObservedTimeZoneForList> TimeZones
        {
            get { return _timeZones; }
            set
            {
                _timeZones = value;
                dataGridView1.DataSource = _timeZones;
            }
        }
        public ObservedTimeZoneForListParams FilterParams { get; set; }
        public IEditTimeZone EditorForm { get; set; }
        public int SelectedId
        {
            get { return (int)dataGridView1.SelectedRows[0].Cells["Id"].Value; }
        }
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }


        public FrmListTimeZones(IServiceProvider serviceProvider)
        {
            var timeZoneConfiguratorService = serviceProvider.GetRequiredService<ITimeZoneConfiguratorService>();
            var systemTimeZoneService = serviceProvider.GetRequiredService<ISystemTimeZonesService>();
            _presenter = new TimeZonePresenter(this, timeZoneConfiguratorService, systemTimeZoneService);
            _frmEditor = new FrmEditTimeZone(_presenter);
            _presenter.SetEditor(_frmEditor);
            InitializeComponent();

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
                DataPropertyName = "Color",
                HeaderText = "Color",
                Width = 100,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DisplayName",
                HeaderText = "Display Name",
                Width = 200,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Comments",
                HeaderText = "Comments",
                Width = 200,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DSTStarts",
                HeaderText = "DST Starts",
                Width = 150,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DSTEnds",
                HeaderText = "DST Ends",
                Width = 150,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastChanged",
                HeaderText = "Last Changed",
                Width = 150,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "TimeZoneObservesDST",
                HeaderText = "Observes DST",
                Width = 100,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NextTransitionDate",
                HeaderText = "Next Transition",
                Width = 150,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsActive",
                HeaderText = "Is Active",
                Width = 100,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NotificationSchedule",
                HeaderText = "Notification Schedule",
                Width = 200,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NotificationsCount",
                HeaderText = "Notifications Count",
                Width = 150,
                ReadOnly = true
            });

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private async void FrmListTimeZones_Load(object sender, EventArgs e)
        {
            await _presenter.IntializeListForm();
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


        private async void tsbSearch_Click(object sender, EventArgs e)
        {
            await _presenter.LoadListAsync();
        }

        private async void tsbRefresh_Click(object sender, EventArgs e)
        {
            await _presenter.LoadListAsync();
        }

        private async void tsbPrevious_Click(object sender, EventArgs e)
        {
            await _presenter.LoadListAsync();
        }

        private async void tsbNext_Click(object sender, EventArgs e)
        {
            await _presenter.LoadListAsync();
        }

        private async void tsbAddNew_Click(object sender, EventArgs e)
        {
            await _presenter.CreateNewAsync();
        }

        private async void tsbEdit_Click(object sender, EventArgs e)
        {
            await _presenter.EditSelectedAsync();
        }

        private async void tsbDelete_Click(object sender, EventArgs e)
        {
            await _presenter.DeleteFromListAsync();
        }

        public void ShowPager()
        {
            tsbPrevious.Visible = true;
            tsbNext.Visible = true;
            lblPageCount.Visible = true;

        }

        public void HidePager()
        {
            tsbPrevious.Visible = false;
            tsbNext.Visible = false;
            lblPageCount.Visible = false;
        }
    }
}
