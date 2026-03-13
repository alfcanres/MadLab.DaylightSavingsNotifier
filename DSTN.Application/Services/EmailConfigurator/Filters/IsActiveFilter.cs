using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;

namespace DSTN.Application.Services.EmailConfigurator.Filters
{
    internal class IsActiveFilter : IQueryFilter<EmailConfiguration>
    {
        private readonly bool _isActive;

        public IsActiveFilter(bool isActive)
        {
            _isActive = isActive;
        }

        public IQueryable<EmailConfiguration> ApplyFilter(IQueryable<EmailConfiguration> queryable)
        {
            return queryable.Where(x => x.IsActive == _isActive);
        }
    }
}
