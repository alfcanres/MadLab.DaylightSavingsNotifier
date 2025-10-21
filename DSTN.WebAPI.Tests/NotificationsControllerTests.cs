using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;



namespace DSTN.WebAPI.Tests
{
    public class NotificationsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public NotificationsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }



        [Fact]
        public async Task GetById_ReturnsOkOrBadRequest()
        {
            var response = await _client.GetAsync("/api/notifications/1");
            Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task MarkAsRead_ReturnsOkOrBadRequest()
        {
            var response = await _client.PostAsync("/api/notifications/1/mark-as-read", null);
            Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetDueOrOverdueNotifications_ReturnsOkOrBadRequest()
        {
            var response = await _client.GetAsync("/api/notifications/due");
            Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ScanTimeZonesForNotification_ReturnsOkOrBadRequest()
        {
            var response = await _client.GetAsync("/api/notifications/scan");
            Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}
