using DSTN.Application.DTO;
using DSTN.Application.Helpers;
using DSTN.Application.Services.EmailConfigurator;
using Microsoft.AspNetCore.Mvc;

namespace DSTN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailConfigurationController : ControllerBase
    {
        private readonly ILogger<EmailConfigurationController> _logger;
        private readonly IEmailConfiguratorService _emailConfiguratorService;

        public EmailConfigurationController(
            ILogger<EmailConfigurationController> logger,
            IEmailConfiguratorService emailConfiguratorService)
        {
            _logger = logger;
            _emailConfiguratorService = emailConfiguratorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActive()
        {
            OperationResult<EmailConfigurationDTO> response = new OperationResult<EmailConfigurationDTO>();
            try
            {
                response = await _emailConfiguratorService.GetActiveEmailConfigurationAsync();
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
                response.ValidatorResponse.AddError("Unable to get the active email configuration");
                _logger.LogError(ex, "An error occurred while getting the active email configuration: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListEmailConfiguration([FromQuery] EmailConfigurationListParamsDTO listParametersDTO)
        {
            OperationResult<PagedList<EmailConfigurationDTO>> response = new OperationResult<PagedList<EmailConfigurationDTO>>();
            try
            {
                response = await _emailConfiguratorService.ListEmailConfigurationAsync(listParametersDTO);
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
                response.ValidatorResponse.AddError("Unable to get the email configurations");
                _logger.LogError(ex, "An error occurred while getting the email configurations: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            OperationResult<EmailConfigurationDTO> response = new OperationResult<EmailConfigurationDTO>();
            try
            {
                response = await _emailConfiguratorService.GetEmailConfigurationByIdAsync(id);
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
                response.ValidatorResponse.AddError("Unable to get the email configuration");
                _logger.LogError(ex, "An error occurred while getting the email configuration: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmailConfiguration(AddEmailConfigurationDTO model)
        {
            OperationResult<EmailConfigurationDTO> response = new OperationResult<EmailConfigurationDTO>();
            try
            {
                response = await _emailConfiguratorService.AddEmailConfigurationAsync(model);
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
                response.ValidatorResponse.AddError("Unable to add the email configuration");
                _logger.LogError(ex, "An error occurred while adding the email configuration: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmailConfiguration(int id, [FromBody] EditEmailConfigurationDTO model)
        {
            OperationResult<EmailConfigurationDTO> response = new OperationResult<EmailConfigurationDTO>();
            try
            {
                model.Id = id;
                response = await _emailConfiguratorService.EditEmailConfigurationAsync(model);
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
                response.ValidatorResponse.AddError("Unable to edit the email configuration");
                _logger.LogError(ex, "An error occurred while editing the email configuration: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmailConfiguration(int id)
        {
            OperationResult<EmptyOperationResult> response = new OperationResult<EmptyOperationResult>();
            try
            {
                response = await _emailConfiguratorService.DeleteEmailConfigurationAsync(id);
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
                response.ValidatorResponse.AddError("Unable to delete the email configuration");
                _logger.LogError(ex, "An error occurred while deleting the email configuration: {Message}", ex.Message);
                return StatusCode(500, response);
            }
        }
    }
}
