using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.EmailConfigurator;

namespace DSTN.AdminApp.WinForms.Repository.EmailConfigurator;

public interface IEmailConfiguratorService
{
    Task<ServiceResult<EmailConfiguration>> GetActiveEmailConfigurationAsync();
    Task<ServiceResult<EmailConfiguration>> GetEmailConfigurationByIdAsync(int id);
    Task<ServiceResult<EmailConfiguration>> AddEmailConfigurationAsync(AddEmailConfiguration model);
    Task<ServiceResult<EmailConfiguration>> EditEmailConfigurationAsync(EditEmailConfiguration model);
    Task<ServiceResult<EmptyAPIResponse>> DeleteEmailConfigurationAsync(int id);
}
