
using DSTN.Application.DTO;
using DSTN.Application.Helpers;
using DSTN.Application.Services.TimeZoneConfigurator;
using Microsoft.AspNetCore.Mvc;

namespace DSTN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservedTimeZonesController : ControllerBase
    {
        private readonly ILogger<ObservedTimeZonesController> _logger;
        private readonly ITimeZoneConfiguratorService _timeZoneService;
        public ObservedTimeZonesController(ILogger<ObservedTimeZonesController> logger, ITimeZoneConfiguratorService timeZoneService)
        {
            _logger = logger;
            _timeZoneService = timeZoneService;
        }

        [HttpGet]
        public async Task<IActionResult> ListObservedTimeZones([FromQuery] ObservedTimeZoneForListParamsDTO listParametersDTO)
        {
            try
            {
                var response = await _timeZoneService.ListObservedTimeZones(listParametersDTO);
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
                errorResponse.ValidatorResponse.AddError("Unable to get system time zones");
                _logger.LogError(ex, "An error occurred while getting system time zones: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _timeZoneService.GetById(id);
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
                errorResponse.ValidatorResponse.AddError("Unable to get the observed time zone");
                _logger.LogError(ex, "An error occurred while getting the observed time zone: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTimeZoneToObserve(AddTimeZoneToObserveDTO model)
        {
            try
            {
                var response = await _timeZoneService.AddTimeZoneToObserveAsync(model);
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
                errorResponse.ValidatorResponse.AddError("Unable to add the observed time zone");
                _logger.LogError(ex, "An error occurred while adding the observed time zone: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditTimeZoneToObserve(EditTimeZoneToObserveDTO model)
        {
            try
            {
                var response = await _timeZoneService.EditTimeZoneToObserveAsync(model);
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
                errorResponse.ValidatorResponse.AddError("Unable to edit the observed time zone");
                _logger.LogError(ex, "An error occurred while editing the observed time zone: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteZoneToObserve(int id)
        {
            try
            {
                var response = await _timeZoneService.DeleteZoneToObserveAsync(id);
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
                errorResponse.ValidatorResponse.AddError("Unable to delete the observed time zone");
                _logger.LogError(ex, "An error occurred while deleting the observed time zone: {Message}", ex.Message);
                return BadRequest(errorResponse);
            }


        }
    }
}