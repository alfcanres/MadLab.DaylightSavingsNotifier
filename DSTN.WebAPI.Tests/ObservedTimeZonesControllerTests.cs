using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;


namespace DSTN.WebAPI.Tests
{
    public class ObservedTimeZonesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ObservedTimeZonesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ListObservedTimeZones_ReturnsOkOrBadRequest()
        {
            var response = await _client.GetAsync("/api/observedtimezones");
            Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetById_ReturnsOkOrBadRequest()
        {
            var response = await _client.GetAsync("/api/observedtimezones/1");
            Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddTimeZoneToObserve_ReturnsOkOrBadRequest()
        {
            var payload = new { /* fill with required AddTimeZoneToObserveDTO properties */ };
            var response = await _client.PostAsJsonAsync("/api/observedtimezones", payload);
            Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest);
        }

    }
}
