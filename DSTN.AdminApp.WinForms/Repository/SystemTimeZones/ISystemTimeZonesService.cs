using DSTN.AdminApp.WinForms.ViewModels;

namespace DSTN.AdminApp.WinForms.Repository.SystemTimeZones
{
    public interface ISystemTimeZonesService
    {
        Task<ServiceResult<bool>> SupportsDaylightSavingTime(string id, int year);
        Task<ServiceResult<string>> FindSystemTimeZoneById(string id);
        Task<ServiceResult<bool>> IsValidTimeZoneId(string id);
        Task<ServiceResult<IEnumerable<string>>> GetSystemTimeZones();
        Task<ServiceResult<DateTime?>> GetDSTTransitionDate(int year, string systemTimeZoneId, bool isStart);
        Task<ServiceResult<DateTime?>> GetNextTransitionDate(DateTime currentDate, string timeZoneId);
    }
}
