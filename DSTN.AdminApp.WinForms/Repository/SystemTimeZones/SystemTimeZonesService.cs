using System.Net.Http.Json;
using DSTN.AdminApp.WinForms.Properties;
using DSTN.AdminApp.WinForms.ViewModels;
using Microsoft.Extensions.Logging;

namespace DSTN.AdminApp.WinForms.Repository.SystemTimeZones
{
    public class SystemTimeZonesService : ISystemTimeZonesService
    {
        private const string _baseEndPoint = "api/SystemTimeZones";
        private readonly HttpClient _httpClient;
        private readonly ILogger<SystemTimeZonesService> _logger;

        public SystemTimeZonesService(IHttpClientFactory httpClientFactory, ILogger<SystemTimeZonesService> logger)
        {
            _httpClient = httpClientFactory.CreateClient(Settings.Default.ClientName);
            _logger = logger;
        }

        public async Task<ServiceResult<string>> FindSystemTimeZoneById(string id)
        {
            try
            {
                var url = $"{_baseEndPoint}/find-system-timezone-by-id?id={Uri.EscapeDataString(id)}";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<string>>();
                return new ServiceResult<string>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FindSystemTimeZoneById");
                return new ServiceResult<string>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<DateTime?>> GetDSTTransitionDate(int year, string systemTimeZoneId, bool isStart)
        {
            try
            {
                var url = $"{_baseEndPoint}/get-dst-transition-date?year={year}&systemTimeZoneId={Uri.EscapeDataString(systemTimeZoneId)}&isStart={isStart}";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<DateTime?>>();
                return new ServiceResult<DateTime?>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDSTTransitionDate");
                return new ServiceResult<DateTime?>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<DateTime?>> GetNextTransitionDate(DateTime currentDate, string timeZoneId)
        {
            try
            {
                var url = $"{_baseEndPoint}/get-next-transition-date?currentDate={Uri.EscapeDataString(currentDate.ToString("o"))}&timeZoneId={Uri.EscapeDataString(timeZoneId)}";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<DateTime?>>();
                return new ServiceResult<DateTime?>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetNextTransitionDate");
                return new ServiceResult<DateTime?>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<DateTime?>> GetNotificationDate(DateTime? DSTStartOrEnds, int notifyDaysBefore)
        {
            try
            {
                var dateParam = DSTStartOrEnds.HasValue ? Uri.EscapeDataString(DSTStartOrEnds.Value.ToString("o")) : string.Empty;
                var url = $"{_baseEndPoint}/get-notification-date?DSTStartOrEnds={dateParam}&notifyDaysBefore={notifyDaysBefore}";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<DateTime?>>();
                return new ServiceResult<DateTime?>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetNotificationDate");
                return new ServiceResult<DateTime?>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<IEnumerable<string>>> GetSystemTimeZones()
        {
            try
            {
                var url = $"{_baseEndPoint}/get-system-timezones";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<IEnumerable<string>>>();
                return new ServiceResult<IEnumerable<string>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSystemTimeZones");
                return new ServiceResult<IEnumerable<string>>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<bool>> IsValidTimeZoneId(string id)
        {
            try
            {
                var url = $"{_baseEndPoint}/is-valid-timezone-id?id={Uri.EscapeDataString(id)}";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
                return new ServiceResult<bool>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in IsValidTimeZoneId");
                return new ServiceResult<bool>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<bool>> SupportsDaylightSavingTime(string id, int year)
        {
            try
            {
                var url = $"{_baseEndPoint}/supports-daylight-saving-time?id={Uri.EscapeDataString(id)}&year={year}";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
                return new ServiceResult<bool>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SupportsDaylightSavingTime");
                return new ServiceResult<bool>("An error occurred while processing your request.");
            }
        }
    }
}
