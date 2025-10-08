using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;


namespace DSTN.WebAPI.Tests
{
    public class SystemTimeZonesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SystemTimeZonesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetSystemTimeZones_ReturnsOkOrNotFoundOrBadRequest()
        {
            var response = await _client.GetAsync("/api/systemtimezones");
            Assert.Contains(response.StatusCode, new[] { HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest });
        }

        [Fact]
        public async Task GetSystemTimeZoneById_ReturnsOkOrNotFoundOrBadRequest()
        {
            var response = await _client.GetAsync("/api/systemtimezones/UTC");
            Assert.Contains(response.StatusCode, new[] { HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest });
        }
    }
}
