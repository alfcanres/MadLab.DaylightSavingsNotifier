using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.EmailConfigurator;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DSTN.AdminApp.WinForms.Repository.EmailConfigurator;

public class EmailConfiguratorService : IEmailConfiguratorService
{
    private const string _baseEndPoint = "api/EmailConfiguration";
    private readonly HttpClient _httpClient;
    private readonly ILogger<EmailConfiguratorService> _logger;

    public EmailConfiguratorService(IHttpClientFactory httpClientFactory, ILogger<EmailConfiguratorService> logger)
    {
        _httpClient = httpClientFactory.CreateClient(Properties.Settings.Default.ClientName);
        _logger = logger;
    }

    public async Task<ServiceResult<EmailConfiguration>> GetActiveEmailConfigurationAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_baseEndPoint);
            var result = await response.Content.ReadFromJsonAsync<APIResponse<EmailConfiguration>>();
            return new ServiceResult<EmailConfiguration>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetActiveEmailConfigurationAsync");
            return new ServiceResult<EmailConfiguration>("An error occurred while processing your request.");
        }
    }

    public async Task<ServiceResult<EmailConfiguration>> GetEmailConfigurationByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseEndPoint}/{id}");
            var result = await response.Content.ReadFromJsonAsync<APIResponse<EmailConfiguration>>();
            return new ServiceResult<EmailConfiguration>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetEmailConfigurationByIdAsync");
            return new ServiceResult<EmailConfiguration>("An error occurred while processing your request.");
        }
    }

    public async Task<ServiceResult<EmailConfiguration>> AddEmailConfigurationAsync(AddEmailConfiguration model)
    {
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseEndPoint, content);
            var result = await response.Content.ReadFromJsonAsync<APIResponse<EmailConfiguration>>();
            return new ServiceResult<EmailConfiguration>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AddEmailConfigurationAsync");
            return new ServiceResult<EmailConfiguration>("An error occurred while processing your request.");
        }
    }

    public async Task<ServiceResult<EmailConfiguration>> EditEmailConfigurationAsync(EditEmailConfiguration model)
    {
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseEndPoint}/{model.Id}", content);
            var result = await response.Content.ReadFromJsonAsync<APIResponse<EmailConfiguration>>();
            return new ServiceResult<EmailConfiguration>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in EditEmailConfigurationAsync");
            return new ServiceResult<EmailConfiguration>("An error occurred while processing your request.");
        }
    }

    public async Task<ServiceResult<EmptyAPIResponse>> DeleteEmailConfigurationAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_baseEndPoint}/{id}");
            var result = await response.Content.ReadFromJsonAsync<APIResponse<EmptyAPIResponse>>();
            return new ServiceResult<EmptyAPIResponse>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteEmailConfigurationAsync");
            return new ServiceResult<EmptyAPIResponse>("An error occurred while processing your request.");
        }
    }
}
