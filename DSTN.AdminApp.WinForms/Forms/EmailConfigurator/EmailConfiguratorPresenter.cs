using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.AdminApp.WinForms.Repository.EmailConfigurator;
using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.EmailConfigurator;

namespace DSTN.AdminApp.WinForms.Forms.EmailConfigurator;

public class EmailConfiguratorPresenter : IGenericPresenter
{
    private IEditEmailConfigurator _editView;
    private readonly IListEmailConfigurator _listView;
    private readonly IEmailConfiguratorService _emailConfiguratorService;

    public EmailConfiguratorPresenter(
        IListEmailConfigurator listView,
        IEmailConfiguratorService emailConfiguratorService)
    {
        _listView = listView;
        _emailConfiguratorService = emailConfiguratorService;
    }

    public void SetEditor(IEditEmailConfigurator editView)
    {
        _editView = editView;
    }

    internal async Task IntializeListForm()
    {
        _listView.ShowLoading("Loading email configurations...");
        _listView.Title = "Email Configurations";

        List<string> filters = new List<string>
        {
            "[SELECT]",
            "Name"
        };

        _listView.Filters = filters;
        _listView.SelectedFilter = "[SELECT]";

        await LoadListAsync();
        _listView.HideLoading();
    }

    public async Task CreateNewAsync()
    {
        _editView.Title = "Add New Email Configuration";
        _listView.HideLoading();
        _editView.ShowDeleteButton = false;
        _editView.ShowSaveButtom = true;

        _editView.Id = 0;
        _editView.ConfigName = string.Empty;
        _editView.SmtpHost = string.Empty;
        _editView.SmtpPort = 587;
        _editView.UseSsl = false;
        _editView.UseStartTls = true;
        _editView.SenderName = string.Empty;
        _editView.SenderEmail = string.Empty;
        _editView.Username = string.Empty;
        _editView.Password = string.Empty;
        _editView.IsActive = true;
        _editView.IsDefault = false;
        _editView.CloseOnSave = false;

        _editView.Show();
    }

    public async Task EditSelectedAsync()
    {
        int configId = _listView.SelectedId;
        if (configId <= 0)
        {
            _listView.ShowAlert("No email configuration was selected.");
            return;
        }

        _editView.ShowLoading("Loading email configuration details...");

        var response = await _emailConfiguratorService.GetEmailConfigurationByIdAsync(configId);

        if (response.Status == ResultStatus.Success)
        {
            _editView.ShowDeleteButton = true;
            _editView.ShowSaveButtom = true;
            var config = response.Data;
            _editView.Title = $"Edit Email Configuration - {config.Name}";
            _editView.Id = config.Id;
            _editView.ConfigName = config.Name;
            _editView.SmtpHost = config.SmtpHost;
            _editView.SmtpPort = config.SmtpPort;
            _editView.UseSsl = config.UseSsl;
            _editView.UseStartTls = config.UseStartTls;
            _editView.SenderName = config.SenderName;
            _editView.SenderEmail = config.SenderEmail;
            _editView.Username = config.Username;
            _editView.Password = string.Empty;
            _editView.IsActive = config.IsActive;
            _editView.IsDefault = config.IsDefault;
            _editView.CloseOnSave = false;

            _editView.HideLoading();
            _editView.Show();
        }
        else
        {
            _editView.HideLoading();
            _editView.ShowErrors(response.Messages);
        }
    }

    public async Task SaveAsync()
    {
        _editView.ShowLoading("Saving email configuration...");

        if (_editView.Id == 0)
        {
            var addModel = new AddEmailConfiguration(
                _editView.ConfigName,
                _editView.SmtpHost,
                _editView.SmtpPort,
                _editView.UseSsl,
                _editView.UseStartTls,
                _editView.SenderName,
                _editView.SenderEmail,
                _editView.Username,
                _editView.Password,
                _editView.IsActive,
                _editView.IsDefault
            );

            var response = await _emailConfiguratorService.AddEmailConfigurationAsync(addModel);

            if (response.Status == ResultStatus.Success)
            {
                _editView.HideLoading();
                _listView.ShowAlert("Email configuration added successfully.");
                await LoadListAsync();
                if (_editView.CloseOnSave)
                {
                    _editView.CloseForm();
                }
                else
                {
                    await CreateNewAsync();
                }
            }
            else
            {
                _editView.HideLoading();
                _editView.ShowErrors(response.Messages);
            }
        }
        else
        {
            var editModel = new EditEmailConfiguration(
                _editView.Id,
                _editView.ConfigName,
                _editView.SmtpHost,
                _editView.SmtpPort,
                _editView.UseSsl,
                _editView.UseStartTls,
                _editView.SenderName,
                _editView.SenderEmail,
                _editView.Username,
                _editView.Password,
                _editView.IsActive,
                _editView.IsDefault
            );

            var response = await _emailConfiguratorService.EditEmailConfigurationAsync(editModel);

            if (response.Status == ResultStatus.Success)
            {
                _editView.HideLoading();
                _listView.ShowAlert("Email configuration updated successfully.");
                await LoadListAsync();
                if (_editView.CloseOnSave)
                {
                    _editView.CloseForm();
                }
                else
                {
                    await CreateNewAsync();
                }
            }
            else
            {
                _editView.HideLoading();
                _editView.ShowErrors(response.Messages);
            }
        }
    }

    public async Task DeleteFromListAsync()
    {
        _listView.HideLoading();
        if (_listView.ConfirmDelete("Are you sure you want to delete this email configuration?"))
        {
            var response = await _emailConfiguratorService.DeleteEmailConfigurationAsync(_listView.SelectedId);
            _listView.ShowLoading();
            if (response.Status == ResultStatus.Success)
            {
                _listView.ShowAlert("Email configuration deleted successfully.");
                await LoadListAsync();
            }
            else
            {
                _listView.ShowErrors(response.Messages);
            }
            _listView.HideLoading();
        }
    }

    public async Task DeleteFromEditorAsync()
    {
        _editView.HideLoading();
        if (_editView.ConfirmDelete("Are you sure you want to delete this email configuration?"))
        {
            var response = await _emailConfiguratorService.DeleteEmailConfigurationAsync(_editView.Id);
            _editView.ShowLoading();
            if (response.Status == ResultStatus.Success)
            {
                _editView.ShowAlert("Email configuration deleted successfully.");
                _editView.CloseForm();
                await LoadListAsync();
            }
            else
            {
                _editView.ShowErrors(response.Messages);
            }
            _editView.HideLoading();
        }
    }

    public async Task LoadListAsync()
    {
        _listView.ShowLoading("Loading email configurations...");
        _listView.HidePager();

        var filterParams = new EmailConfigurationListParams();

        ConfigureFilterParameters(filterParams);

        var response = await _emailConfiguratorService.ListEmailConfigurationsAsync(filterParams);

        if (response.Status != ResultStatus.Success)
        {
            _listView.ShowErrors(response.Messages);
            _listView.HideLoading();
            return;
        }

        _listView.EmailConfigurations = response.Data.List;
        _listView.CurrentPage = response.Data.CurrentPage;
        _listView.PageCount = response.Data.PageCount;
        _listView.TotalRecords = response.Data.RecordCount;

        if (response?.Data?.PageCount > 1)
        {
            _listView.ShowPager();
        }
        else
        {
            _listView.HidePager();
        }

        _listView.HideLoading();
    }

    public void ConfigureFilterParameters(EmailConfigurationListParams filterParameters)
    {
        if (_listView.SelectedFilter == "[SELECT]")
        {
            filterParameters.Name = null;
            filterParameters.IsActive = null;
        }
        else if (_listView.SelectedFilter == "Name")
        {
            filterParameters.Name = _listView.SearchKeyWord;
            filterParameters.IsActive = null;
        }

        filterParameters.CurrentPage = _listView.CurrentPage;
        filterParameters.RecordsPerPage = _listView.RecordsPerPage;
    }
}
