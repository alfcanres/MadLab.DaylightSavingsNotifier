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
        private const string _baseUrl = "api/ObservedTimeZones";
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TimeZoneConfiguratorService> _logger;

        public TimeZoneConfiguratorService(
            IHttpClientFactory httpClientFactory,
            ILogger<TimeZoneConfiguratorService> logger)
        {
           
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient(Settings.Default.ClientName);
            _logger = logger;
        }



        public async Task<OperationResultVM<ObservedTimeZoneVM>> AddTimeZoneToObserveAsync(AddTimeZoneToObserveVM model)
        {

            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OperationResultVM<ObservedTimeZoneVM>>();

            return result;
        }

        public async Task<OperationResultVM<EmptyOperationResultVM>> DeleteZoneToObserveAsync(int id)
        {


            var response = await _httpClient.DeleteAsync(_baseUrl + "/" + id.ToString());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OperationResultVM<EmptyOperationResultVM>>();

            return result;
        }

        public async Task<OperationResultVM<ObservedTimeZoneVM>> EditTimeZoneToObserveAsync(EditTimeZoneToObserveVM model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(_baseUrl + "/" + model.Id.ToString(), content);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OperationResultVM<ObservedTimeZoneVM>>();

            return result;
        }

        public async Task<OperationResultVM<ObservedTimeZoneVM>> GetByTimeZoneToObserveIdAsync(int timeZoneId)
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/" + timeZoneId);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OperationResultVM<ObservedTimeZoneVM>>();

            return result;

        }

        public async Task<OperationResultVM<PagedListVM<ObservedTimeZoneForListVM>>> ListObservedTimeZones(ObservedTimeZoneForListParamsVM listParametersDTO)
        {
            var url = _baseUrl + listParametersDTO.ToQueryString();
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OperationResultVM<PagedListVM<ObservedTimeZoneForListVM>>>();

            return result;


        }
    }
}
