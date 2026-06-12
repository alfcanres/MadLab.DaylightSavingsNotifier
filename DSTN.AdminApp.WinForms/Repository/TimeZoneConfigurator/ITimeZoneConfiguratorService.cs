using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;

namespace DSTN.AdminApp.WinForms.Repository.TimeZoneConfigurator
{
    public interface ITimeZoneConfiguratorService
    {
        Task<ServiceResult<ObservedTimeZone>> AddTimeZoneToObserveAsync(AddTimeZoneToObserve model);
        Task<ServiceResult<ObservedTimeZone>> EditTimeZoneToObserveAsync(EditTimeZoneToObserve model);
        Task<ServiceResult<ObservedTimeZone>> GetByTimeZoneToObserveIdAsync(int timeZoneId);
        Task<ServiceResult<EmptyAPIResponse>> DeleteZoneToObserveAsync(int id);
        Task<ServiceResult<PagedListResponse<ObservedTimeZoneForList>>> ListObservedTimeZones(ObservedTimeZoneForListParams listParametersDTO);
        Task<ServiceResult<IEnumerable<ItemForCombo>>> GetAllForCombo();
        Task<ServiceResult<IEnumerable<EmailTimeZoneNotification>>> GetEmailsToNotifyAsync();
        Task<ServiceResult<EmptyAPIResponse>> SendEmailForTimeZoneSummaryAsync(EmailTimeZoneNotification emailToNotify);
    }
}
