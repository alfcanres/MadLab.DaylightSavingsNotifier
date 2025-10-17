using DSTN.Application.DTO;
using DSTN.Application.Helpers;
using DSTN.Application.Services.TimeZoneConfigurator.Filters;
using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace DSTN.Application.Services.TimeZoneConfigurator
{
    public class TimeZoneConfiguratorService : CRUDBaseService<
        ObservedTimeZone,
        AddTimeZoneToObserveDTO,
        ObservedTimeZoneDTO,
        EditTimeZoneToObserveDTO>,
        ITimeZoneConfiguratorService
    {
        private readonly IQueryBuilder<ObservedTimeZone> _queryBuilder;
        private readonly ISystemTimeZoneProvider _systemTimeZoneProvider;

        public TimeZoneConfiguratorService(
            IRepository<ObservedTimeZone> repository,
            ILogger<TimeZoneConfiguratorService> logger,
            IQueryBuilder<ObservedTimeZone> queryBuilder,
            ISystemTimeZoneProvider systemTimeZoneProvider
            ) : base(repository, logger)
        {
            _queryBuilder = queryBuilder;
            _systemTimeZoneProvider = systemTimeZoneProvider;
        }

        public async Task<OperationResult<ObservedTimeZoneDTO>> AddTimeZoneToObserveAsync(AddTimeZoneToObserveDTO model)
        {
            return await InsertAsync(model);
        }

        public async Task<OperationResult<EmptyOperationResult>> DeleteZoneToObserveAsync(int id)
        {

            return await this.DeleteAsync(id);
        }

        public async Task<OperationResult<ObservedTimeZoneDTO>> EditTimeZoneToObserveAsync(EditTimeZoneToObserveDTO model)
        {
            try
            {
                await ValidateUpdateAsync(model.Id, model);

                if (!Validator.IsValid)
                {
                    return new OperationResult<ObservedTimeZoneDTO>
                    {
                        Data = null,
                        ValidatorResponse = Validator.CrateNewCopy()
                    };
                }

                var entity = await Repository.GetByIdAsync(model.Id);

                MapUpdateDTOToEntity(model, entity);

                entity.TimeZoneObservesDST = _systemTimeZoneProvider.SupportsDaylightSavingTime(model.TimeZoneId, model.LastChanged.Year);
                entity.LastChanged = model.LastChanged;

                if (entity.TimeZoneObservesDST)
                {
                    entity.DSTStarts = _systemTimeZoneProvider.GetDSTTransitionDate(model.LastChanged.Year, model.TimeZoneId, true);
                    entity.DSTEnds = _systemTimeZoneProvider.GetDSTTransitionDate(model.LastChanged.Year, model.TimeZoneId, false);
                    entity.NextTransitionDate = _systemTimeZoneProvider.GetNextTransitionDate(DateTime.UtcNow, model.TimeZoneId);
                }


                await Repository.UpdateAsync(entity);

                var readDTO = MapEntityToReadDTO(entity);

                return new OperationResult<ObservedTimeZoneDTO>
                {
                    Data = readDTO,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating entity.");
                Validator.AddError("An error occurred while processing your request.");
                return new OperationResult<ObservedTimeZoneDTO>
                {
                    Data = null,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }

        public async Task<OperationResult<ObservedTimeZoneDTO>> GetByTimeZoneToObserveIdAsync(int timeZoneId)
        {
            return await base.GetByIdAsync(timeZoneId);
        }

        public async Task<OperationResult<PagedList<ObservedTimeZoneForListDTO>>> ListObservedTimeZones(ObservedTimeZoneForListParamsDTO listParametersDTO)
        {
            try
            {
                Validator.Clear();

                if (!string.IsNullOrWhiteSpace(listParametersDTO.DisplayName))
                {
                    _queryBuilder
                    .AddFilter(new DisplayNameFilter(listParametersDTO.DisplayName));
                }

                if (!string.IsNullOrWhiteSpace(listParametersDTO.TimeZoneId))
                {
                    _queryBuilder
                    .AddFilter(new SystemTimeZoneIdFilter(listParametersDTO.TimeZoneId));
                }

                int totalRecords = await _queryBuilder.CountAsync();

                _queryBuilder
                .AddPaging(listParametersDTO.CurrentPage, listParametersDTO.RecordsPerPage);

                var results = await _queryBuilder.GetListAsync();

                var pagedList = new PagedList<ObservedTimeZoneForListDTO>(
                    results.Select(t => ObservedTimeZoneForListDTO.FromEntity(t)),
                    totalRecords,
                    listParametersDTO);

                return new OperationResult<PagedList<ObservedTimeZoneForListDTO>>()
                {
                    Data = pagedList,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while listing observed time zones.");
                Validator.AddError("An error occurred while processing your request.");
                return new OperationResult<PagedList<ObservedTimeZoneForListDTO>>()
                {
                    Data = null,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }

        }

        protected override ObservedTimeZone MapCreateDTOToEntity(AddTimeZoneToObserveDTO create)
        {
            var entity = AddTimeZoneToObserveDTO.ToEntity(create);

            entity.TimeZoneObservesDST = _systemTimeZoneProvider.SupportsDaylightSavingTime(create.TimeZoneId, create.CreatedAt.Year);
            entity.LastChanged = create.CreatedAt;
            entity.CreatedAt = create.CreatedAt;
            if (entity.TimeZoneObservesDST)
            {
                entity.DSTStarts = _systemTimeZoneProvider.GetDSTTransitionDate(create.CreatedAt.Year, create.TimeZoneId, true);
                entity.DSTEnds = _systemTimeZoneProvider.GetDSTTransitionDate(create.CreatedAt.Year, create.TimeZoneId, false);
                entity.NextTransitionDate = _systemTimeZoneProvider.GetNextTransitionDate(create.CreatedAt, create.TimeZoneId);
            }
            return entity;
        }

        protected override ObservedTimeZoneDTO MapEntityToReadDTO(ObservedTimeZone entity)
        {
            return ObservedTimeZoneDTO.FromEntity(entity);
        }

        protected override void MapUpdateDTOToEntity(EditTimeZoneToObserveDTO updateDTO, ObservedTimeZone entity)
        {
            EditTimeZoneToObserveDTO.ToEntity(updateDTO, entity);
        }

        protected override async Task ValidateModelToDeletetAsync(int id)
        {
            var timeZoneExistsQry = Repository
                .Query().Where(x => x.Id == id);

            var timeZoneExists = await Repository.AnyAsync(timeZoneExistsQry);

            if (!timeZoneExists)
            {
                Validator.AddError("TimeZone does not exist.");
            }
        }

        protected override async Task ValidateModelToInsertAsync(AddTimeZoneToObserveDTO createDTO)
        {
            if (createDTO == null)
            {
                Validator.AddError("Create DTO cannot be null.");
            }
            else
            {



                if (string.IsNullOrWhiteSpace(createDTO.TimeZoneId))
                {
                    Validator.AddError("TimeZoneId is required.");
                }
                else if (!_systemTimeZoneProvider.IsValidTimeZoneId(createDTO.TimeZoneId))
                {
                    Validator.AddError("TimeZoneId is not valid.");
                }

                if (string.IsNullOrWhiteSpace(createDTO.DisplayName))
                {
                    Validator.AddError("Display Name is required.");
                }

                var timeZoneAlreadyExistsQry = Repository
                    .Query()
                    .Where(x =>
                    x.TimeZoneId == createDTO.TimeZoneId
                    ||
                    x.DisplayName == createDTO.DisplayName
                    );

                var timeZoneAlreadyExists = await Repository
                    .AnyAsync(timeZoneAlreadyExistsQry);

                if (timeZoneAlreadyExists)
                {
                    Validator.AddError("TimeZone already exists.");
                }


            }


        }

        protected override async Task ValidateModelToUpdateAsync(int id, EditTimeZoneToObserveDTO updateDTO)
        {
            //Validate if the entity exists 
            var timeZoneExistsQry = Repository
                .Query().Where(x => x.Id == id);    
            var timeZoneExists = await Repository.AnyAsync(timeZoneExistsQry);
            if (!timeZoneExists)
            {
                Validator.AddError("TimeZone does not exist.");
            }
            else
            {
                if (updateDTO == null)
                {
                    Validator.AddError("Update DTO cannot be null.");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(updateDTO.TimeZoneId))
                    {
                        Validator.AddError("TimeZoneId is required.");
                    }
                    else if (!_systemTimeZoneProvider.IsValidTimeZoneId(updateDTO.TimeZoneId))
                    {
                        Validator.AddError("TimeZoneId is not valid.");
                    }

                    if (string.IsNullOrWhiteSpace(updateDTO.DisplayName))
                    {
                        Validator.AddError("DisplayName is required.");
                    }

                    var timeZoneAlreadyExistsQry = Repository
                        .Query()
                        .Where(x =>
                        x.Id != id &&
                        (x.TimeZoneId == updateDTO.TimeZoneId
                        ||
                        x.DisplayName == updateDTO.DisplayName)
                        );

                    var timeZoneAlreadyExists = await Repository
                        .AnyAsync(timeZoneAlreadyExistsQry);

                    if (timeZoneAlreadyExists)
                    {
                        Validator.AddError("TimeZone already exists.");
                    }
                }
            }




        }
    }
}
