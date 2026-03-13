using DSTN.Application.DTO;
using DSTN.Application.Helpers;

namespace DSTN.Application.Services.EmailConfigurator
{
    public interface IEmailConfiguratorService
    {
        Task<OperationResult<EmailConfigurationDTO>> AddEmailConfigurationAsync(AddEmailConfigurationDTO model);
        Task<OperationResult<EmailConfigurationDTO>> EditEmailConfigurationAsync(EditEmailConfigurationDTO model);
        Task<OperationResult<EmailConfigurationDTO>> GetEmailConfigurationByIdAsync(int id);
        Task<OperationResult<EmptyOperationResult>> DeleteEmailConfigurationAsync(int id);
        Task<OperationResult<EmailConfigurationDTO>> GetActiveEmailConfigurationAsync();

    }
}
