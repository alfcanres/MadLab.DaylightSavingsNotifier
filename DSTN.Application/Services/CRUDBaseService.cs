using DSTN.Application.Helpers;
using DSTN.Domain.Interfaces;
using Microsoft.Extensions.Logging;


namespace DSTN.Application.Services
{
    public abstract class CRUDBaseService<TEntity, TCreateDTO, TReadDTO, TUpdateDTO>
        where TEntity : class
        where TCreateDTO : class
        where TReadDTO : class
        where TUpdateDTO : class
    {
        private readonly IRepository<TEntity> _repository;
        protected readonly ILogger _logger;
        private readonly ValidationResult _validator = new ValidationResult();
        protected ValidationResult Validator => _validator;
        protected IRepository<TEntity> Repository { get { return _repository; } }
        protected CRUDBaseService(
            IRepository<TEntity> repository,
            ILogger logger
        )
        {
            _logger = logger;
            _repository = repository;
        }

        #region CRUD Methods
        protected async Task<OperationResult<TReadDTO>> InsertAsync(TCreateDTO createDTO)
        {
            try
            {
                await ValidateInsertAsync(createDTO);
                if (!Validator.IsValid)
                {
                    return new OperationResult<TReadDTO>
                    {
                        Data = default,
                        ValidatorResponse = Validator
                    };
                }

                var entity = MapCreateDTOToEntity(createDTO);

                await Repository.InsertAsync(entity);

                TReadDTO readDTO = MapEntityToReadDTO(entity);

                return new OperationResult<TReadDTO>
                {
                    Data = readDTO,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting entity.");
                Validator.AddError("An error occurred while processing your request.");
                return new OperationResult<TReadDTO>
                {
                    Data = default,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }

        }
        protected async Task<OperationResult<TReadDTO>> UpdateAsync(int id, TUpdateDTO updateDTO)
        {
            try
            {
                await ValidateUpdateAsync(id, updateDTO);
                if (!Validator.IsValid)
                {
                    return new OperationResult<TReadDTO>
                    {
                        Data = default,
                        ValidatorResponse = Validator.CrateNewCopy()
                    };
                }

                var entity = await Repository.GetByIdAsync(id);

                entity = MapUpdateDTOToEntity(updateDTO);

                await Repository.UpdateAsync(entity);

                TReadDTO readDTO = MapEntityToReadDTO(entity);

                return new OperationResult<TReadDTO>
                {
                    Data = readDTO,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating entity.");
                Validator.AddError("An error occurred while processing your request.");
                return new OperationResult<TReadDTO>
                {
                    Data = default,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }

        }
        protected async Task<OperationResult<EmptyOperationResult>> DeleteAsync(int id)
        {
            try
            {
                await ValidateDeleteAsync(id);
                if (!Validator.IsValid)
                {
                    return new OperationResult<EmptyOperationResult>()
                    {
                        Data = new EmptyOperationResult(),
                        ValidatorResponse = Validator.CrateNewCopy()
                    };
                }

                await Repository.DeleteAsync(id);

                return new OperationResult<EmptyOperationResult>()
                {
                    Data = new EmptyOperationResult(),
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting entity.");
                Validator.AddError("An error occurred while processing your request.");
                return new OperationResult<EmptyOperationResult>
                {
                    Data = new EmptyOperationResult(),
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }

        }
        protected async Task<OperationResult<TReadDTO>> GetByIdAsync(int id)
        {
            try
            {
                Validator.Clear();  

                var entity = await Repository.GetByIdAsync(id);

                if(entity == null)
                {
                    Validator.AddError("Item was not found.");
                    return new OperationResult<TReadDTO>
                    {
                        Data = default,
                        ValidatorResponse = Validator.CrateNewCopy()
                    };
                }
                else
                {
                    TReadDTO readDTO = MapEntityToReadDTO(entity);

                    return new OperationResult<TReadDTO>
                    {
                        Data = readDTO,
                        ValidatorResponse = Validator.CrateNewCopy()
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting entity.");
                Validator.AddError("An error occurred while processing your request.");
                return new OperationResult<TReadDTO>
                {
                    Data = default,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }
        #endregion

        protected abstract TEntity MapCreateDTOToEntity(TCreateDTO create);
        protected abstract TReadDTO MapEntityToReadDTO(TEntity entity);
        protected abstract TEntity MapUpdateDTOToEntity(TUpdateDTO entity);


        #region Model Validation
        protected abstract Task ValidateModelToInsertAsync(TCreateDTO createDTO);
        protected abstract Task ValidateModelToDeletetAsync(int id);
        protected abstract Task ValidateModelToUpdateAsync(int id, TUpdateDTO updateDTO);

        protected async Task ValidateInsertAsync(TCreateDTO createDTO)
        {
            Validator.Clear();
            await ValidateModelToInsertAsync(createDTO);
        }
        protected async Task ValidateDeleteAsync(int id)
        {
            Validator.Clear();
            await ValidateModelToDeletetAsync(id);
        }
        protected async Task ValidateUpdateAsync(int id, TUpdateDTO updateDTO)
        {
            Validator.Clear();

            if (id == 0)
                Validator.AddError("Id is required.");

            if (updateDTO == null)
                Validator.AddError("No information provided for update.");

            if (Validator.IsValid)
            {
                await ValidateModelToUpdateAsync(id, updateDTO);
            }
        }
        #endregion
    }
}
