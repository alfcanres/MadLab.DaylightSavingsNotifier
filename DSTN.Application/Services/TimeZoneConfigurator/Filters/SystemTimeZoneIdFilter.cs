using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;

namespace DSTN.Application.Services.TimeZoneConfigurator.Filters
{
    public class SystemTimeZoneIdFilter : IQueryFilter<ObservedTimeZone>
    {
        private readonly string _systemTimeZone;
        public SystemTimeZoneIdFilter(string systemTimeZone)
        {
            _systemTimeZone = systemTimeZone ?? throw new ArgumentNullException(nameof(systemTimeZone), "System Time Zone ID name cannot be null");
        }
        public IQueryable<ObservedTimeZone> ApplyFilter(IQueryable<ObservedTimeZone> queryable)
        {
            if (string.IsNullOrWhiteSpace(_systemTimeZone))
            {
                return queryable;
            }
            return queryable.Where(x => x.TimeZoneId.ToLower().Contains(_systemTimeZone.ToLower()));
        }
    }
}

