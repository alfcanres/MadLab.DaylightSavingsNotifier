using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;

namespace DSTN.AdminApp.WinForms.Repository.TimeZoneConfigurator
{
    public interface ITimeZoneConfiguratorService
    {
        Task<OperationResultVM<ObservedTimeZoneVM>> AddTimeZoneToObserveAsync(AddTimeZoneToObserveVM model);
        Task<OperationResultVM<ObservedTimeZoneVM>> EditTimeZoneToObserveAsync(EditTimeZoneToObserveVM model);
        Task<OperationResultVM<ObservedTimeZoneVM>> GetByTimeZoneToObserveIdAsync(int timeZoneId);
        Task<OperationResultVM<EmptyOperationResultVM>> DeleteZoneToObserveAsync(int id);
        Task<OperationResultVM<PagedListVM<ObservedTimeZoneForListVM>>> ListObservedTimeZones(ObservedTimeZoneForListParamsVM listParametersDTO);

    }
}
