using Microsoft.Extensions.Logging;


namespace DSTN.AdminApp.WinForms.Helpers
{
    public class HttpClientHelper<TInput, TOutput> where TInput : class where TOutput : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;
        private readonly ILogger<HttpClientHelper<TInput, TOutput>> _logger;

        public HttpClientHelper(IHttpClientFactory httpClientFactory, ILogger<HttpClientHelper<TInput, TOutput>> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _client = _httpClientFactory.CreateClient();
        }

        public async Task<string> GetAsync(string url)
        {
            try
            {
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while making GET request to {Url}", url);
                throw;
            }
        }

        public async Task<string> PostAsync(string url, HttpContent content)
        {
            try
            {
                var response = await _client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while making POST request to {Url}", url);
                throw;
            }
        }

        // Add methods for PUT, DELETE

        public async Task<string> PutAsync(string url, HttpContent content)
        {
            try
            {
                var response = await _client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while making PUT request to {Url}", url);
                throw;
            }
        }

        public async Task<string> DeleteAsync(string url)
        {
            try
            {
                var response = await _client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while making DELETE request to {Url}", url);
                throw;
            }
        }






    }
}
