using DSTN.AdminApp.WinForms.Forms.TimeZones;
using DSTN.AdminApp.WinForms.Repository.TimeZoneConfigurator;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;
using Microsoft.Extensions.DependencyInjection;


namespace DSTN.AdminApp.WinForms.TimeZones
{
    public partial class FrmListTimeZones : Form, IListTimeZones
    {
        IEnumerable<ObservedTimeZoneForListVM> _timeZones;
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


        public IEnumerable<ObservedTimeZoneForListVM> TimeZones
        {
            get { return _timeZones; }
            set
            {
                _timeZones = value;
                dataGridView1.DataSource = _timeZones;
            }
        }
        public ObservedTimeZoneForListParamsVM FilterParams { get; set; }
        public IEditTimeZone EditorForm { get; set; }
        public int SelectedId { get; set; } = 0;
        public List<string> ValidationErrors { get; set; }

        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }


        public FrmListTimeZones(IServiceProvider serviceProvider)
        {
            var _timeZoneConfiguratorService = serviceProvider.GetRequiredService<ITimeZoneConfiguratorService>();
            _presenter = new TimeZonePresenter(this, _timeZoneConfiguratorService);
            _frmEditor = new FrmEditTimeZone(_presenter);
            _presenter.SetEditor(_frmEditor);
            InitializeComponent();


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

        public async void ShowEditor(int id = 0)
        {
            if (id == 0)
            {
                await _presenter.CreateNewAsync();
            }
            else
            {
                SelectedId = id;
                await _presenter.EditSelectedAsync();
            }
        }

        public void ShowError()
        {
            string errors = string.Join(Environment.NewLine, ValidationErrors);
            MessageBox.Show(errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
