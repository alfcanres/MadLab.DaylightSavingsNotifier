using DSTN.Application.Services.TimeZoneNotifier;

namespace DSTN.WebAPI.Workers
{
    public class DSTNotificationWorker : BackgroundService
    {
        private readonly ILogger<DSTNotificationWorker> _logger;
        private readonly ITimeZoneNotifierService _timeZoneNotifierService;

        public DSTNotificationWorker(ILogger<DSTNotificationWorker> logger, TimeZoneNotifierService timeZoneNotifierService)
        {
            _logger = logger;
            _timeZoneNotifierService = timeZoneNotifierService;
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
            _logger.LogInformation($"DSTNotificationWorker running UpdateDSTForObservedTimeZones");

            var today = DateTime.Now;
            var updateTzRes = await _timeZoneNotifierService.UpdateDSTForObservedTimeZones(today);
            foreach (var tz in updateTzRes.Data)
            {
                _logger.LogInformation($"DSTNotificationWorker updated Time Zone {tz.DisplayName} - {tz.TimeZoneId}.");
            }
            _logger.LogInformation($"DSTNotificationWorker Total updated Time Zones {updateTzRes.Data.Count()}.");


            _logger.LogInformation($"DSTNotificationWorker running ScanTimeZonesForNotification");
            
            var scanTzRes = await _timeZoneNotifierService.ScanTimeZonesForNotification(today);

            _logger.LogInformation($"DSTNotificationWorker found {scanTzRes.Data.Count()} time zones to notify");
            
            foreach (var tz in scanTzRes.Data)
            {
                var notification = await _timeZoneNotifierService.CreateNotificationAsync(tz.Id);
                _logger.LogInformation($"DSTNotificationWorker created notification for Time Zone {tz.DisplayName} - {tz.TimeZoneId}");
            }

            _logger.LogInformation($"DSTNotificationWorker done with notifications");
        }
    }
}
