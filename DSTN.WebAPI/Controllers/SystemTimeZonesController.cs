using DSTN.Application.Helpers;
using DSTN.Application.Services.TimeZoneConfigurator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSTN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemTimeZonesController : ControllerBase
    {
        private readonly ILogger<SystemTimeZonesController> _logger;
        public SystemTimeZonesController(ILogger<SystemTimeZonesController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public IActionResult GetSystemTimeZones()
        {
            OperationResult<IEnumerable<string>> returnValue = new OperationResult<IEnumerable<string>>();

            try
            {
                var timeZones = TimeZoneInfo.GetSystemTimeZones().Select(tz => tz.Id).ToList();

                if (timeZones == null || !timeZones.Any())
                {
                    returnValue.ValidatorResponse.AddError("No time zones found on the system.");

                    return NotFound(returnValue);
                }
                else
                {
                    returnValue.Result = timeZones;
                }

                return Ok(returnValue);

            }
            catch (Exception ex)
            {
                returnValue.ValidatorResponse.AddError("Unable to get system time zones");
                _logger.LogError(ex, "An error occurred while getting system time zones: {Message}", ex.Message);
                return BadRequest(returnValue);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetSystemTimeZoneById(string id)
        {
            OperationResult<string> returnValue = new OperationResult<string>();
            try
            {
                var timeZone = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(tz => tz.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (timeZone == null)
                {
                    returnValue.ValidatorResponse.AddError($"Time zone with ID '{id}' not found on the system.");
                    return NotFound(returnValue);
                }
                else
                {
                    returnValue.Result = timeZone.Id;
                }
                return Ok(returnValue);
            }
            catch (Exception ex)
            {
                returnValue.ValidatorResponse.AddError("Unable to get system time zone by ID");
                _logger.LogError(ex, "An error occurred while getting system time zone by ID: {Message}", ex.Message);
                return BadRequest(returnValue);
            }
        }
    }
}
