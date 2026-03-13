using DSTN.Application.Services.TimeZoneNotifier;
using System.Diagnostics.Eventing.Reader;

namespace DSTN.WebAPI.Workers
{
    public class DSTNotificationWorker : BackgroundService
    {
        private readonly ILogger<DSTNotificationWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public DSTNotificationWorker(ILogger<DSTNotificationWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation($"DSTNotificationWorker started at {DateTime.Now}.");
            try
            {
                await RunNotifierAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"DSTNotificationWorker First run failed at {DateTime.Now}");
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                var delay = GetDelayUntilNext3AM();

                _logger.LogInformation("DSTNotificationWorker Next run scheduled in {Delay}.", delay);

                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }

                try
                {
                    await RunNotifierAsync();
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("DSTNotificationWorker Run cancelled.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"DSTNotificationWorker Error during scheduled run at {DateTime.Now}");
                }


            }
            _logger.LogInformation($"DSTNotificationWorker is stopping at {DateTime.Now}.");
        }

        private TimeSpan GetDelayUntilNext3AM()
        {
            var now = DateTime.Now;
            var next = new DateTime(now.Year, now.Month, now.Day, 3, 0, 0);
            if (next <= now)
            {
                next = next.AddDays(1);
            }

            return next - now;
        }

        private async Task RunNotifierAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var timeZoneNotifierService = scope.ServiceProvider.GetRequiredService<ITimeZoneNotifierService>();
                _logger.LogInformation($"DSTNotificationWorker running UpdateDSTForObservedTimeZones");

                var today = DateTime.Now;
                var updateTzRes = await timeZoneNotifierService.UpdateDSTForObservedTimeZones(today);
                foreach (var tz in updateTzRes.Data)
                {
                    _logger.LogInformation($"DSTNotificationWorker updated Time Zone {tz.DisplayName} - {tz.TimeZoneId}.");
                }
                _logger.LogInformation($"DSTNotificationWorker Total updated Time Zones {updateTzRes.Data.Count()}.");


                _logger.LogInformation($"DSTNotificationWorker running ScanTimeZonesForNotification");

                var scanTzRes = await timeZoneNotifierService.ScanTimeZonesForNotification(today);

                _logger.LogInformation($"DSTNotificationWorker found {scanTzRes.Data.Count()} time zones to notify");

                foreach (var tz in scanTzRes.Data)
                {
                    var notification = await timeZoneNotifierService.CreateNotificationAsync(tz.Id);
                    _logger.LogInformation($"DSTNotificationWorker created notification for Time Zone {tz.DisplayName} - {tz.TimeZoneId}");

                    if (notification.ValidatorResponse.IsValid)
                    {
                        var sendEmailRes = await timeZoneNotifierService.SendEmailNotificationAsync(notification.Data);

                        if (sendEmailRes.ValidatorResponse.IsValid)
                        {
                            _logger.LogInformation($"DSTNotificationWorker sent email notification for Time Zone {tz.DisplayName} - {tz.TimeZoneId}");
                        }
                        else
                        {
                            foreach (var error in sendEmailRes.ValidatorResponse.MessageList)
                            {
                                _logger.LogError(error);
                            }
                        }
                    }
                    else
                    {
                        foreach (var error in notification.ValidatorResponse.MessageList)
                        {
                            _logger.LogError($"DSTNotificationWorker error for Time Zone {tz.DisplayName} - {tz.TimeZoneId}: {error}");
                        }
                    }



                }

                _logger.LogInformation($"DSTNotificationWorker done with notifications");
            }


        }
    }
}
