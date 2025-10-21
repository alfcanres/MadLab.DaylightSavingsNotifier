using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.Notifications;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text;

namespace DSTN.AdminApp.WinForms.Repository.Notifications
{
    public class NotficationsService : INotficationsService
    {
        private const string _baseEndPoint = "api/Notifications";
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotficationsService> _logger;

        public NotficationsService(IHttpClientFactory httpClientFactory, ILogger<NotficationsService> logger)
        {
            _httpClient = httpClientFactory.CreateClient(Properties.Settings.Default.ClientName);
            _logger = logger;
        }

        public async Task<ServiceResult<Notification>> GetNotificationByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseEndPoint + $"/{id}");
                var result = await response.Content.ReadFromJsonAsync<APIResponse<Notification>>();
                return new ServiceResult<Notification>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetNotificationByIdAsync");
                return new ServiceResult<Notification>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<PagedListResponse<Notification>>> ListNotificationsAsync(NotificationListParams listParametersVM)
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseEndPoint + listParametersVM.ToQueryString());
                var result = await response.Content.ReadFromJsonAsync<APIResponse<PagedListResponse<Notification>>>();
                return new ServiceResult<PagedListResponse<Notification>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ListNotificationsAsync");
                return new ServiceResult<PagedListResponse<Notification>>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<Notification>> MarkNotificationAsReadAsync(int id)
        {
            try
            {
                var response = await _httpClient.PostAsync(_baseEndPoint + $"/{id}/mark-as-read", null);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<Notification>>();
                return new ServiceResult<Notification>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MarkNotificationAsReadAsync");
                return new ServiceResult<Notification>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<int>> CountUnread()
        {
            try
            {

                var response = await _httpClient.GetAsync($"{_baseEndPoint}/count-unread");
                var result = await response.Content.ReadFromJsonAsync<APIResponse<int>>();
                return new ServiceResult<int>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ListNotificationsAsync");
                return new ServiceResult<int>("An error occurred while processing your request.");
            }
        }

    }
}
