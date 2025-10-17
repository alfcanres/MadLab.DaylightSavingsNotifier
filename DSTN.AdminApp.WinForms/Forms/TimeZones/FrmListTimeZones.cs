using DSTN.AdminApp.WinForms.Forms.TimeZones;
using DSTN.AdminApp.WinForms.Properties;
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
        private FrmEditTimeZone _frmEditor;
        private int _recordsPerPage = Settings.Default.RecordsPerPage;
        private int _currentPage = 1;
        private string _searchKeyWord = string.Empty;
        private int _pageCount = 1;
        private int _totalRecords = 0;

        public string SearchKeyWord
        {
            get { return _searchKeyWord; }
            set
            {
                _searchKeyWord = value;
                txtSearch.Text = _searchKeyWord;
            }
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

        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        public int RecordsPerPage
        {
            get { return _recordsPerPage; }
            set { _recordsPerPage = value; }

        }
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; }
        }

        public int TotalRecords
        {
            get { return _totalRecords; }
            set
            {
                _totalRecords = value;
                if (_totalRecords != 0)
                {
                    tsbTotalRecords.Text = $"{_totalRecords} time zones found";
                }
                else
                {
                    tsbTotalRecords.Text = "No time zones found";
                }
            }
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
                UpdatePageCount();
            }
        }
        public IEditTimeZone EditorForm { get; set; }
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
            _frmEditor = new FrmEditTimeZone(_presenter);

            _presenter.SetEditor(_frmEditor);

        }

        private void UpdatePageCount()
        {
            if (_currentPage != 0)
            {
                lblPageCount.Text = $"Page {_currentPage} of {_pageCount}";
            }
            else
            {
                lblPageCount.Text = string.Empty;
            }
        }


        public FrmListTimeZones(IServiceProvider serviceProvider)
        {
            var timeZoneConfiguratorService = serviceProvider.GetRequiredService<ITimeZoneConfiguratorService>();
            var systemTimeZoneService = serviceProvider.GetRequiredService<ISystemTimeZonesService>();
            _presenter = new TimeZonePresenter(this, timeZoneConfiguratorService, systemTimeZoneService);


            InitializeComponent();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 25,
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Color",
                HeaderText = "Color",
                Width = 80,
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
                DataPropertyName = "DisplayName",
                HeaderText = "Display Name",
                Width = 200,
                ReadOnly = true

            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SystemTimeZoneId",
                HeaderText = "System Time Zone",
                Width = 200,
                ReadOnly = true

            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NextTransitionDate",
                HeaderText = "Next Transition",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True, Format = "dddd, d MMMM, yyyy" }
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
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True, Format = "dddd, d MMMM, yyyy" }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DSTEnds",
                HeaderText = "DST Ends",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True, Format = "dddd, d MMMM, yyyy" }
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

            // Subscribe to CellFormatting to set per-cell BackColor based on the bound "Color" value.
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }


        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            var grid = (DataGridView)sender;
            var column = grid.Columns[e.ColumnIndex];
            if (string.Equals(column.DataPropertyName, "Color", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is string colorString && !string.IsNullOrWhiteSpace(colorString))
                {
                    Color parsed;
                    try
                    {
                        parsed = ColorTranslator.FromHtml(colorString.Trim());
                    }
                    catch
                    {
                        if (!Enum.TryParse<KnownColor>(colorString.Trim(), true, out var known) ||
                            (parsed = Color.FromKnownColor(known)) == Color.Empty)
                        {
                            return;
                        }
                    }

                    e.CellStyle.BackColor = parsed;
                }
            }

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

        public bool ConfirmDelete(string alert)
        {
            return MessageBox.Show(alert, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
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
            PreviousClick();
            await _presenter.LoadListAsync();
        }

        private void PreviousClick()
        {
            if (CurrentPage != 1)
            {
                CurrentPage = CurrentPage - 1;
            }
        }

        private async void tsbNext_Click(object sender, EventArgs e)
        {
            NextClick();
            await _presenter.LoadListAsync();
        }

        private void NextClick()
        {
            if(CurrentPage != PageCount)
            {
                CurrentPage = CurrentPage + 1;
            }
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.SearchKeyWord = txtSearch.Text;
        }
    }
}
