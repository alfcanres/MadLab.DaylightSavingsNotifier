
using DSTN.Application.DTO;
using DSTN.Application.Helpers;
using DSTN.Application.Services.TimeZoneConfigurator;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            OperationResult<PagedList<ObservedTimeZoneForListDTO>> response = new OperationResult<PagedList<ObservedTimeZoneForListDTO>>();
            try
            {
                response = await _timeZoneService.ListObservedTimeZones(listParametersDTO);
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
                response.ValidatorResponse.AddError("Unable to get observed time zones");
                _logger.LogError(ex, "An error occurred while getting observed time zones: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            OperationResult<ObservedTimeZoneDTO> response = new OperationResult<ObservedTimeZoneDTO>();
            try
            {
                response = await _timeZoneService.GetByTimeZoneToObserveIdAsync(id);
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
                response.ValidatorResponse.AddError("Unable to get the observed time zone");
                _logger.LogError(ex, "An error occurred while getting the observed time zone: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTimeZoneToObserve(AddTimeZoneToObserveDTO model)
        {
            OperationResult<ObservedTimeZoneDTO> response = new OperationResult<ObservedTimeZoneDTO>();
            try
            {
                model.CreatedAt = DateTime.UtcNow;  
                response = await _timeZoneService.AddTimeZoneToObserveAsync(model);
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
                response.ValidatorResponse.AddError("Unable to add the observed time zone");
                _logger.LogError(ex, "An error occurred while adding the observed time zone: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTimeZoneToObserve(int id, [FromBody]EditTimeZoneToObserveDTO model)
        {
            OperationResult<ObservedTimeZoneDTO> response = new OperationResult<ObservedTimeZoneDTO>();
            try
            {
                model.LastChanged = DateTime.UtcNow;
                response = await _timeZoneService.EditTimeZoneToObserveAsync(model);
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
                response.ValidatorResponse.AddError("Unable to edit the observed time zone");
                _logger.LogError(ex, "An error occurred while editing the observed time zone: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZoneToObserve(int id)
        {
            OperationResult<EmptyOperationResult> response = new OperationResult<EmptyOperationResult>();
            try
            {
                response = await _timeZoneService.DeleteZoneToObserveAsync(id);
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
                response.ValidatorResponse.AddError("Unable to delete the observed time zone");
                _logger.LogError(ex, "An error occurred while deleting the observed time zone: {Message}", ex.Message);
                return StatusCode(500, response);
            }


        }

        [HttpGet("active-only")]
        public async Task<IActionResult> GetActive()
        {
            OperationResult<IEnumerable<ObservedTimeZoneDTO>> response = new OperationResult<IEnumerable<ObservedTimeZoneDTO>>();

            try
            {
                response = await _timeZoneService.GetAllActive();
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
                response.ValidatorResponse.AddError("Unable to get the observed time zone");
                _logger.LogError(ex, "An error occurred while getting the observed time zone: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

    }
}