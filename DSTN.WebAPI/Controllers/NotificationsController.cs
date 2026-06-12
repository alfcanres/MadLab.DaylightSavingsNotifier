using DSTN.Application.DTO;
using DSTN.Application.Helpers;
using DSTN.Application.Services.TimeZoneNotifier;
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
            OperationResult<PagedList<NotificationReadDTO>> response = new OperationResult<PagedList<NotificationReadDTO>>();
            try
            {
                response = await _timeZoneNotifierService.ListNotificationsAsync(listParametersDTO);
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
                response.ValidatorResponse.AddError("Unable to get notifications");
                _logger.LogError(ex, "An error occurred while getting notifications: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            OperationResult<NotificationReadDTO> response = new OperationResult<NotificationReadDTO>();
            try
            {
                response = await _timeZoneNotifierService.GetNotificationByIdAsync(id);
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
                response.ValidatorResponse.AddError("Unable to get notification");
                _logger.LogError(ex, "An error occurred while getting notification: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            OperationResult<NotificationReadDTO> response = new OperationResult<NotificationReadDTO>();
            try
            {
                response = await _timeZoneNotifierService.MarkNotificationAsReadAsync(id);
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
                response.ValidatorResponse.AddError("Unable to mark notification as read");
                _logger.LogError(ex, "An error occurred while marking notification as read: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateNotification([FromBody] int id)
        {
            OperationResult<NotificationReadDTO> response = new OperationResult<NotificationReadDTO>();
            try
            {
                if (id <= 0)
                {
                    response.ValidatorResponse.AddError("ObservedTimeZoneDTO cannot be null.");
                    return BadRequest(response);
                }

                response = await _timeZoneNotifierService.CreateNotificationAsync(id);

                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ValidatorResponse.AddError("Unable to create notification.");
                _logger.LogError(ex, "An error occurred while creating notification: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }


        [HttpGet("count-unread")]
        public async Task<IActionResult> CountUnreadNotifications()
        {
            OperationResult<int> response = new OperationResult<int>();
            try
            {
                response = await _timeZoneNotifierService.CountUnreadNotifications();

                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ValidatorResponse.AddError("Unable to get unread notification count.");
                _logger.LogError(ex, "An error occurred while getting unread notification count: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("emails-to-notify")]
        public async Task<IActionResult> GetEmailsToNotify()
        {
            OperationResult<IEnumerable<EmailTimeZoneNotificationDTO>> response = new OperationResult<IEnumerable<EmailTimeZoneNotificationDTO>>();
            try
            {
                response = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ValidatorResponse.AddError("Unable to get emails to notify.");
                _logger.LogError(ex, "An error occurred while getting emails to notify: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost("send-email-summary")]
        public async Task<IActionResult> SendEmailForTimeZoneSummary([FromBody] EmailTimeZoneNotificationDTO emailToNotify)
        {
            OperationResult<EmptyOperationResult> response = new OperationResult<EmptyOperationResult>();
            try
            {
                if (emailToNotify is null)
                {
                    response.ValidatorResponse.AddError("EmailTimeZoneNotificationDTO cannot be null.");
                    return BadRequest(response);
                }

                response = await _timeZoneNotifierService.SendEmailForTimeZoneSummary(emailToNotify);

                if (!response.ValidatorResponse.IsValid)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ValidatorResponse.AddError("Unable to send email for time zone summary.");
                _logger.LogError(ex, "An error occurred while sending email for time zone summary: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }


    }
}