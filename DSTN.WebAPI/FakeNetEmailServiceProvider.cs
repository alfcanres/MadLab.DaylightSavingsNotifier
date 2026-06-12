using DSTN.Domain.Interfaces;

namespace DSTN.WebAPI
{
    public class FakeNetEmailServiceProvider : IEmailService
    {
        private const int FailureThresholdPercent = 10;


        private const int MinDelayMs = 300;
        private const int MaxDelayMs = 1500;
        public void ConfigureCredentials(string smtpServer, int port, string username, string password, bool enableSSL)
        {

        }

        public async Task<(bool success, string message)> SendEmailAsync(string from, string to, string subject, string body, bool isBodyHtml)
        {
            int delay = Random.Shared.Next(MinDelayMs, MaxDelayMs);
            await Task.Delay(delay);

            // Randomly simulate a transient failure at the configured rate
            bool simulateFailure = Random.Shared.Next(1, 101) <= FailureThresholdPercent;

            return simulateFailure
                ? (false, $"[FAKE] Failed to deliver email to '{to}' after {delay}ms. Simulated transient SMTP error.")
                : (true, $"[FAKE] Email successfully sent to '{to}' in {delay}ms. Subject: '{subject}'.");
        }
    }
}
