using DSTN.AdminApp.WinForms.Forms.Notifications;
using DSTN.AdminApp.WinForms.Properties;
using DSTN.AdminApp.WinForms.Repository.Notifications;
using DSTN.AdminApp.WinForms.Repository.TimeZoneConfigurator;
using DSTN.AdminApp.WinForms.TimeZones;
using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.Notifications;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;


namespace DSTN.AdminApp.WinForms.Notifications
{
    public partial class FrmListNotifications : Form, IListNotifications
    {
        IEnumerable<Notification> _list;
        private readonly NotificationPresenter _presenter;
        private FrmEditNotification _frmEditor;
        private int _recordsPerPage = Settings.Default.RecordsPerPage;
        private int _currentPage = 1;
        private int _pageCount = 1;
        private int _totalRecords = 0;

        public string SearchKeyWord { set; get; }

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
                    tsbTotalRecords.Text = $"{_totalRecords} notifications found";
                }
                else
                {
                    tsbTotalRecords.Text = "No notifications found";
                }
            }
        }


        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }


        public IEnumerable<Notification> Notifications
        {
            get { return _list; }
            set
            {
                _list = value;
                dataGridView1.DataSource = _list;
                UpdatePageCount();
            }
        }
        public IEditNotification EditorForm { get; set; }
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

        private IEnumerable<ItemForCombo> _observedTimeZones;
        public IEnumerable<ItemForCombo> ObservedTimeZones
        {
            get { return _observedTimeZones; }
            set
            {
                _observedTimeZones = value;
                cboTimeZones.DataSource = null;
                cboTimeZones.DataSource = _observedTimeZones.ToList();
            }
        }

        private int _selectedTimeZoneId;
        public int SelectedTimeZoneId
        {
            get
            {
                if (cboTimeZones.SelectedValue != null)
                    _selectedTimeZoneId = (int)cboTimeZones.SelectedValue;

                return _selectedTimeZoneId;
            }
            set { _selectedTimeZoneId = value; }
        }


        public bool ShowAll { set { rbAll.Checked = value; } get { return rbAll.Checked; } }
        public bool ShowSeen { set { rbSeen.Checked = value; } get { return rbSeen.Checked; } }
        public bool ShowNotSeen { set { rbNotSeen.Checked = value; } get { return rbNotSeen.Checked; } }
        private int _notSeenNotificationsCount;
        public int NotSeenNotificationsCount
        {
            get { return _notSeenNotificationsCount; }
            set
            {
                _notSeenNotificationsCount = value;
                if (_notSeenNotificationsCount > 0)
                {

                    tsblNotifications.Text = $"New notifications {_notSeenNotificationsCount}";
                    tsblNotifications.Visible = true;
                }
                else
                {
                    tsblNotifications.Visible = false;
                }
            }
        }

        private void CreateEditor()
        {
            _frmEditor = new FrmEditNotification(_presenter);

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


        public FrmListNotifications(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<INotficationsService>();
            var tzConfigSrv = serviceProvider.GetRequiredService<ITimeZoneConfiguratorService>();

            _presenter = new NotificationPresenter(this, service, tzConfigSrv);


            InitializeComponent();

            cboTimeZones.DisplayMember = "DisplayMember";
            cboTimeZones.ValueMember = "ValueMember";

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
                DataPropertyName = "TimeZoneDisplayName",
                HeaderText = "Time Zone Display Name",
                Width = 200,
                ReadOnly = true

            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DSTTransition",
                HeaderText = "DST Transition",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True, Format = "dddd, d MMMM, yyyy" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NotifyDate",
                HeaderText = "Notify Date",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True, Format = "dddd, d MMMM, yyyy" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Message",
                HeaderText = "Message",
                Width = 200,
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

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "WasRead",
                HeaderText = "Was Read",
                Width = 80,
                ReadOnly = true
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReadAt",
                HeaderText = "Read At",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True, Format = "dddd, d MMMM, yyyy" }
            });


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;


        }

        private async void FrmListTimeZones_Load(object sender, EventArgs e)
        {
            await _presenter.IntializeListForm();
           // cboTimeZones.SelectedIndex = 0;
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
            if (CurrentPage != PageCount)
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


        private async void btnLoadNotifications_Click(object sender, EventArgs e)
        {
            await _presenter.LoadListAsync();
        }

        private async void cboTimeZones_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
    }
}
