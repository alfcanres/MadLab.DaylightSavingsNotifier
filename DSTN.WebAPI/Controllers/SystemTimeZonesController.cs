using DSTN.Application.Helpers;
using DSTN.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DSTN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemTimeZonesController : ControllerBase
    {
        private readonly ILogger<SystemTimeZonesController> _logger;
        private readonly ISystemTimeZoneProvider _systemTimeZoneProvider;
        public SystemTimeZonesController(
            ISystemTimeZoneProvider systemTimeZoneProvider,
            ILogger<SystemTimeZonesController> logger)
        {
            _logger = logger;
            _systemTimeZoneProvider = systemTimeZoneProvider;

        }

        [HttpGet("supports-daylight-saving-time")]
        public IActionResult SupportsDaylightSavingTime([FromQuery] string id, [FromQuery] int year)
        {
            OperationResult<bool> response = new OperationResult<bool>();
            try
            {
                response.Data = _systemTimeZoneProvider.SupportsDaylightSavingTime(id, year);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SupportsDaylightSavingTime with id: {Id} and year: {Year}", id, year);
                response.ValidatorResponse.AddError("Unable to get system time zones");
                return StatusCode(500, response);
            }
        }

        [HttpGet("find-system-timezone-by-id")]
        public IActionResult FindSystemTimeZoneById([FromQuery] string id)
        {
            OperationResult<string> response = new OperationResult<string>();
            try
            {
                response.Data = _systemTimeZoneProvider.FindSystemTimeZoneById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FindSystemTimeZoneById with id: {Id}", id);
                response.ValidatorResponse.AddError("Unable to get system time zones");
                return StatusCode(500, response);
            }
        }

        [HttpGet("is-valid-timezone-id")]
        public IActionResult IsValidTimeZoneId([FromQuery] string id)
        {
            OperationResult<bool> response = new OperationResult<bool>();
            try
            {
                response.Data = _systemTimeZoneProvider.IsValidTimeZoneId(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in IsValidTimeZoneId with id: {Id}", id);
                response.ValidatorResponse.AddError("Unable to get system time zones");
                return StatusCode(500, response);
            }
        }

        [HttpGet("get-system-timezones")]
        public IActionResult GetSystemTimeZones()
        {
            OperationResult<IEnumerable<string>> response = new OperationResult<IEnumerable<string>>();
            try
            {
                response.Data = _systemTimeZoneProvider.GetSystemTimeZones();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSystemTimeZones");
                response.ValidatorResponse.AddError("Unable to get system time zones");
                return StatusCode(500, response);
            }

        }

        [HttpGet("get-dst-transition-date")]
        public IActionResult GetDSTTransitionDate([FromQuery] int year, [FromQuery] string systemTimeZoneId, [FromQuery] bool isStart)
        {
            OperationResult<DateTime?> response = new OperationResult<DateTime?>();
            try
            {
                response.Data = _systemTimeZoneProvider.GetDSTTransitionDate(year, systemTimeZoneId, isStart);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDSTTransitionDate with year: {Year}, systemTimeZoneId: {SystemTimeZoneId}, isStart: {IsStart}", year, systemTimeZoneId, isStart);
                response.ValidatorResponse.AddError("Unable to get system time zones");
                return StatusCode(500, response);
            }
        }

        [HttpGet("get-next-transition-date")]
        public IActionResult GetNextTransitionDate([FromQuery] DateTime currentDate, [FromQuery] string timeZoneId)
        {
            OperationResult<DateTime?> response = new OperationResult<DateTime?>(); 
            try
            {
                response.Data = _systemTimeZoneProvider.GetNextTransitionDate(currentDate, timeZoneId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetNextTransitionDate with currentDate: {CurrentDate}, timeZoneId: {TimeZoneId}", currentDate, timeZoneId);
                response.ValidatorResponse.AddError("Unable to get system time zones");
                return StatusCode(500, response);
            }
        }

        [HttpGet("get-notification-date")]
        public IActionResult GetNotificationDate([FromQuery] DateTime? DSTStartOrEnds, [FromQuery] int notifyDaysBefore)
        {
            OperationResult<DateTime?> response = new OperationResult<DateTime?>();
            try
            {
                response.Data = _systemTimeZoneProvider.GetNotificationDate(DSTStartOrEnds, notifyDaysBefore);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetNotificationDate with DSTStartOrEnds: {DSTStartOrEnds}, notifyDaysBefore: {NotifyDaysBefore}", DSTStartOrEnds, notifyDaysBefore);
                response.ValidatorResponse.AddError("Unable to get notification date");
                return StatusCode(500, response);
            }
        }

    }
}
