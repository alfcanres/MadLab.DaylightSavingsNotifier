using DSTN.Application.DTO;
using DSTN.Application.Helpers;
namespace DSTN.Application.Services.TimeZoneConfigurator
{
    public interface ITimeZoneConfiguratorService
    {

        Task<OperationResult<ObservedTimeZoneDTO>> AddTimeZoneToObserveAsync(AddTimeZoneToObserveDTO model);
        Task<OperationResult<ObservedTimeZoneDTO>> EditTimeZoneToObserveAsync(EditTimeZoneToObserveDTO model);
        Task<OperationResult<ObservedTimeZoneDTO>> GetByTimeZoneToObserveIdAsync(int timeZoneId);
        Task<OperationResult<EmptyOperationResult>> DeleteZoneToObserveAsync(int id);
        Task<OperationResult<PagedList<ObservedTimeZoneForListDTO>>> ListObservedTimeZones(ObservedTimeZoneForListParamsDTO listParametersDTO);
        Task<OperationResult<IEnumerable<ObservedTimeZoneDTO>>> GetAllActive();
    }
}
