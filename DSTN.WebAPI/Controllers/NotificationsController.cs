using DSTN.Application.DTO;
using DSTN.Application.Helpers;
using DSTN.Application.Services.TimeZoneConfigurator;
using DSTN.Application.Services.TimeZoneNotifier;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSTN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<ObservedTimeZonesController> _logger;
        private readonly ITimeZoneNotifierService _timeZoneNotifierService;
        public NotificationsController(ILogger<ObservedTimeZonesController> logger, ITimeZoneNotifierService timeZoneNotifierService)
        {
            _logger = logger;
            _timeZoneNotifierService = timeZoneNotifierService;
        }

        [HttpGet]
        public async Task<IActionResult> ListNotifications([FromQuery] NotificationListParamsDTO listParametersDTO)
        {
            try
            {
                var response = await _timeZoneNotifierService.ListNotificationsAsync(listParametersDTO);
                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                OperationResult<string> errorResponse = new OperationResult<string>();
                errorResponse.ValidatorResponse.AddError("Unable to get notifications");
                _logger.LogError(ex, "An error occurred while getting notifications: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _timeZoneNotifierService.GetNotificationByIdAsync(id);
                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                OperationResult<string> errorResponse = new OperationResult<string>();
                errorResponse.ValidatorResponse.AddError("Unable to get notification");
                _logger.LogError(ex, "An error occurred while getting notification: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }

        [HttpPost("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            try
            {
                var response = await _timeZoneNotifierService.MarkNotificationAsReadAsync(id);
                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                OperationResult<string> errorResponse = new OperationResult<string>();
                errorResponse.ValidatorResponse.AddError("Unable to mark notification as read");
                _logger.LogError(ex, "An error occurred while marking notification as read: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }

        [HttpPost("updatedst")]
        public async Task<IActionResult> UpdateDST([FromBody] int year)
        {
            try
            {
                var response = await _timeZoneNotifierService.UpdateDSTForObservedTimeZones(year);
                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                OperationResult<string> errorResponse = new OperationResult<string>();
                errorResponse.ValidatorResponse.AddError("Unable to update DST for observed time zones");
                _logger.LogError(ex, "An error occurred while updating DST for observed time zones: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }

        [HttpGet("due")]
        public async Task<IActionResult> GetDueOrOverdueNotifications()
        {
            try
            {
                var response = await _timeZoneNotifierService.GetDueOrOverdueNotificationsAsync(DateTime.UtcNow);
                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                OperationResult<string> errorResponse = new OperationResult<string>();
                errorResponse.ValidatorResponse.AddError("Unable to get due or overdue notifications");
                _logger.LogError(ex, "An error occurred while getting due or overdue notifications: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] ObservedTimeZoneDTO observedTimeZone)
        {
            try
            {
                var response = await _timeZoneNotifierService.CreateNotificationAsync(observedTimeZone);
                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                OperationResult<string> errorResponse = new OperationResult<string>();
                errorResponse.ValidatorResponse.AddError("Unable to create notification");
                _logger.LogError(ex, "An error occurred while creating notification: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }

        }

        [HttpGet("scan")]
        public async Task<IActionResult> ScanTimeZonesForNotification()
        {
            try
            {
                var response = await _timeZoneNotifierService.ScanTimeZonesForNotification(DateTime.UtcNow);
                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                OperationResult<string> errorResponse = new OperationResult<string>();
                errorResponse.ValidatorResponse.AddError("Unable to scan time zones for notification");
                _logger.LogError(ex, "An error occurred while scanning time zones for notification: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }
    }
}