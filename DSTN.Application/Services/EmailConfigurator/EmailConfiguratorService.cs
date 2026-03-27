using DSTN.Application.DTO;
using DSTN.Application.Helpers;
using DSTN.Application.Services.EmailConfigurator.Filters;
using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace DSTN.Application.Services.EmailConfigurator
{
    public class EmailConfiguratorService : CRUDBaseService<
        EmailConfiguration,
        AddEmailConfigurationDTO,
        EmailConfigurationDTO,
        EditEmailConfigurationDTO>,
        IEmailConfiguratorService
    {
        private readonly IQueryBuilder<EmailConfiguration> _queryBuilder;

        public EmailConfiguratorService(
            IRepository<EmailConfiguration> repository,
            ILogger<EmailConfiguratorService> logger,
            IQueryBuilder<EmailConfiguration> queryBuilder
            ) : base(repository, logger)
        {
            _queryBuilder = queryBuilder;
        }

        /// <summary>Adds a new email configuration record.</summary>
        public async Task<OperationResult<EmailConfigurationDTO>> AddEmailConfigurationAsync(AddEmailConfigurationDTO model)
        {
            return await InsertAsync(model);
        }

        /// <summary>Updates an existing email configuration record.</summary>
        public async Task<OperationResult<EmailConfigurationDTO>> EditEmailConfigurationAsync(EditEmailConfigurationDTO model)
        {
            try
            {
                await ValidateUpdateAsync(model.Id, model);

                if (!Validator.IsValid)
                {
                    return new OperationResult<EmailConfigurationDTO>
                    {
                        Data = null,
                        ValidatorResponse = Validator.CrateNewCopy()
                    };
                }

                var entity = await Repository.GetByIdAsync(model.Id);

                MapUpdateDTOToEntity(model, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await Repository.UpdateAsync(entity);

                var readDTO = MapEntityToReadDTO(entity);

                return new OperationResult<EmailConfigurationDTO>
                {
                    Data = readDTO,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating email configuration.");
                Validator.AddError("An error occurred while processing your request.");
                return new OperationResult<EmailConfigurationDTO>
                {
                    Data = null,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }

        /// <summary>Retrieves an email configuration by its ID.</summary>
        public async Task<OperationResult<EmailConfigurationDTO>> GetEmailConfigurationByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        /// <summary>Deletes an email configuration by its ID.</summary>
        public async Task<OperationResult<EmptyOperationResult>> DeleteEmailConfigurationAsync(int id)
        {
            return await DeleteAsync(id);
        }

        /// <summary>
        /// Returns the first active email configuration. Returns a not-found validation error if none exists.
        /// </summary>
        public async Task<OperationResult<EmailConfigurationDTO>> GetActiveEmailConfigurationAsync()
        {
            try
            {
                Validator.Clear();

                var query = Repository
                    .Query()
                    .Where(e => e.IsActive);

                var entity = await Repository.FirstOrDefaultAsync(query);

                if (entity is null)
                {
                    Validator.AddError("No active email configuration was found.");
                    return new OperationResult<EmailConfigurationDTO>
                    {
                        Data = null,
                        ValidatorResponse = Validator.CrateNewCopy()
                    };
                }

                return new OperationResult<EmailConfigurationDTO>
                {
                    Data = MapEntityToReadDTO(entity),
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving active email configuration.");
                Validator.AddError("An error occurred while processing your request.");
                return new OperationResult<EmailConfigurationDTO>
                {
                    Data = null,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }

        /// <summary>
        /// Returns a paged list of email configurations with optional filters.
        /// </summary>
        public async Task<OperationResult<PagedList<EmailConfigurationDTO>>> ListEmailConfigurationAsync(EmailConfigurationListParamsDTO listParametersDTO)
        {
            try
            {
                Validator.Clear();

                if (!string.IsNullOrWhiteSpace(listParametersDTO.Name))
                {
                    _queryBuilder.AddFilter(new NameFilter(listParametersDTO.Name));
                }

                if (listParametersDTO.IsActive.HasValue)
                {
                    _queryBuilder.AddFilter(new IsActiveFilter(listParametersDTO.IsActive.Value));
                }

                int totalRecords = await _queryBuilder.CountAsync();

                _queryBuilder.AddPaging(listParametersDTO.CurrentPage, listParametersDTO.RecordsPerPage);

                var results = await _queryBuilder.GetListAsync();

                var pagedList = new PagedList<EmailConfigurationDTO>(
                    results.Select(e => EmailConfigurationDTO.FromEntity(e)),
                    totalRecords,
                    listParametersDTO);

                return new OperationResult<PagedList<EmailConfigurationDTO>>()
                {
                    Data = pagedList,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while listing email configurations.");
                Validator.AddError("An error occurred while processing your request.");
                return new OperationResult<PagedList<EmailConfigurationDTO>>()
                {
                    Data = null,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }

        protected override EmailConfiguration MapCreateDTOToEntity(AddEmailConfigurationDTO create)
        {
            var entity = AddEmailConfigurationDTO.ToEntity(create);
            entity.CreatedAt = DateTime.UtcNow;
            return entity;
        }

        protected override EmailConfigurationDTO MapEntityToReadDTO(EmailConfiguration entity)
        {
            return EmailConfigurationDTO.FromEntity(entity);
        }

        protected override void MapUpdateDTOToEntity(EditEmailConfigurationDTO updateDTO, EmailConfiguration entity)
        {
            EditEmailConfigurationDTO.ToEntity(updateDTO, entity);
        }

        protected override async Task ValidateModelToInsertAsync(AddEmailConfigurationDTO createDTO)
        {
            if (createDTO is null)
            {
                Validator.AddError("Create DTO cannot be null.");
                return;
            }

            ValidateCommonFields(createDTO.SmtpHost, createDTO.SmtpPort, createDTO.SenderEmail, createDTO.Username, createDTO.Password);
        }

        protected override async Task ValidateModelToUpdateAsync(int id, EditEmailConfigurationDTO updateDTO)
        {
            var existsQuery = Repository.Query().Where(e => e.Id == id);
            var exists = await Repository.AnyAsync(existsQuery);

            if (!exists)
            {
                Validator.AddError("Email configuration does not exist.");
                return;
            }

            if (updateDTO is null)
            {
                Validator.AddError("Update DTO cannot be null.");
                return;
            }

            ValidateCommonFields(updateDTO.SmtpHost, updateDTO.SmtpPort, updateDTO.SenderEmail, updateDTO.Username, updateDTO.Password);
        }

        protected override async Task ValidateModelToDeletetAsync(int id)
        {
            var existsQuery = Repository.Query().Where(e => e.Id == id);
            var exists = await Repository.AnyAsync(existsQuery);

            if (!exists)
            {
                Validator.AddError("Email configuration does not exist.");
            }
        }

        /// <summary>
        /// Validates fields shared by both insert and update operations.
        /// </summary>
        private void ValidateCommonFields(string smtpHost, int smtpPort, string senderEmail, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(smtpHost))
            {
                Validator.AddError("SMTP host is required.");
            }

            if (smtpPort < 1 || smtpPort > 65535)
            {
                Validator.AddError("SMTP port must be between 1 and 65535.");
            }

            if (string.IsNullOrWhiteSpace(senderEmail))
            {
                Validator.AddError("Sender email is required.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(senderEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Validator.AddError("Sender email is not a valid email address.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                Validator.AddError("Username is required.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                Validator.AddError("Password is required.");
            }
        }
    }
}
