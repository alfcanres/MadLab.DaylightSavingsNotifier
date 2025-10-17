
using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;

namespace DSTN.Application.Services.TimeZoneConfigurator.Filters
{
    public class DisplayNameFilter : IQueryFilter<ObservedTimeZone>
    {
        private readonly string _displayName;
        public DisplayNameFilter(string displayName)
        {
            _displayName = displayName ?? throw new ArgumentNullException(nameof(displayName), "Display name cannot be null");
        }
        public IQueryable<ObservedTimeZone> ApplyFilter(IQueryable<ObservedTimeZone> queryable)
        {
            if (string.IsNullOrWhiteSpace(_displayName))
            {
                return queryable;
            }
            return queryable.Where(x => x.DisplayName.ToLower().Contains(_displayName.ToLower()));
        }
    }
}
