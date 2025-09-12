using DSTN.Application.Helpers;
using DSTN.Domain.Interfaces;
using Microsoft.Extensions.Logging;



namespace DSTN.Application.Services
{
    public abstract class BaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        protected readonly ILogger _logger;
        private readonly ValidationResult _validator = new ValidationResult();

        protected IUnitOfWork UnitOfWork { get { return _unitOfWork; } } 

        protected ValidationResult Validator => _validator;

        protected BaseService(
         IUnitOfWork unitOfWork,
         ILogger logger
         )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
    }
}
