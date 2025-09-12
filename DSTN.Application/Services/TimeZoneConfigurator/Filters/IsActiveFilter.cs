using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;

namespace DSTN.Application.Services.TimeZoneConfigurator.Filters
{
    public class IsActiveFilter : IQueryFilter<ObservedTimeZone>
    {
        private readonly bool _isActive;
        public IsActiveFilter(bool isActive)
        {
            _isActive = isActive;
        }
        public IQueryable<ObservedTimeZone> ApplyFilter(IQueryable<ObservedTimeZone> queryable)
        {
            return queryable.Where(x => x.IsActive == _isActive);
        }
    }
}
