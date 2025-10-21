using DSTN.AdminApp.WinForms.Properties;
using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


namespace DSTN.AdminApp.WinForms.Repository.TimeZoneConfigurator
{
    public class TimeZoneConfiguratorService : ITimeZoneConfiguratorService
    {
        private const string _baseEndPoint = "api/ObservedTimeZones";
        private readonly HttpClient _httpClient;
        private readonly ILogger<TimeZoneConfiguratorService> _logger;

        public TimeZoneConfiguratorService(IHttpClientFactory httpClientFactory, ILogger<TimeZoneConfiguratorService> logger)
        {
            _httpClient = httpClientFactory.CreateClient(Settings.Default.ClientName);
            _logger = logger;
        }

        public async Task<ServiceResult<ObservedTimeZone>> AddTimeZoneToObserveAsync(AddTimeZoneToObserve model)
        {

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseEndPoint, content);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<ObservedTimeZone>>();

                return new ServiceResult<ObservedTimeZone>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddTimeZoneToObserveAsync");
                return new ServiceResult<ObservedTimeZone>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<EmptyAPIResponse>> DeleteZoneToObserveAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_baseEndPoint + "/" + id.ToString());
                var result = await response.Content.ReadFromJsonAsync<APIResponse<EmptyAPIResponse>>();
                return new ServiceResult<EmptyAPIResponse>(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteZoneToObserveAsync");
                return new ServiceResult<EmptyAPIResponse>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<ObservedTimeZone>> EditTimeZoneToObserveAsync(EditTimeZoneToObserve model)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(_baseEndPoint + "/" + model.Id.ToString(), content);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<ObservedTimeZone>>();
                return new ServiceResult<ObservedTimeZone>(result);
            }
            catch(Exception ex)
            {   
                _logger.LogError(ex, "Error in EditTimeZoneToObserveAsync");
                return new ServiceResult<ObservedTimeZone>("An error occurred while processing your request.");
            }

        }

        public async Task<ServiceResult<IEnumerable<ItemForCombo>>> GetAllForCombo()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseEndPoint}/active-only");
                var result = await response.Content.ReadFromJsonAsync<APIResponse<IEnumerable<ObservedTimeZone>>>();

                var itemForCombos = result.Data
                    .Select(tz => new ItemForCombo(tz.Id, tz.DisplayName))
                    .ToList();

                APIResponse<IEnumerable<ItemForCombo>> retItemForCombos = new APIResponse<IEnumerable<ItemForCombo>>(itemForCombos, result.ValidatorResponse);


                return new ServiceResult<IEnumerable<ItemForCombo>>(retItemForCombos);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in ListObservedTimeZones");
                return new ServiceResult<IEnumerable<ItemForCombo>>("An error occurred while processing your request.");
            }
   
        }

        public async Task<ServiceResult<ObservedTimeZone>> GetByTimeZoneToObserveIdAsync(int timeZoneId)
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseEndPoint + "/" + timeZoneId);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<ObservedTimeZone>>();
                return new ServiceResult<ObservedTimeZone>(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in GetByTimeZoneToObserveIdAsync");
                return new ServiceResult<ObservedTimeZone>("An error occurred while processing your request.");
            }
        }

        public async Task<ServiceResult<PagedListResponse<ObservedTimeZoneForList>>> ListObservedTimeZones(ObservedTimeZoneForListParams listParametersDTO)
        {
            try
            {
                var url = _baseEndPoint + listParametersDTO.ToQueryString();
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadFromJsonAsync<APIResponse<PagedListResponse<ObservedTimeZoneForList>>>();
                return new ServiceResult<PagedListResponse<ObservedTimeZoneForList>>(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in ListObservedTimeZones");
                return new ServiceResult<PagedListResponse<ObservedTimeZoneForList>>("An error occurred while processing your request.");
            }
        }
    }
}
