using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;

namespace DSTN.Application.Services.TimeZoneNotifier.Filters
{
    internal class TimeZoneIdFilter : IQueryFilter<Notification>
    {
        private readonly int _timeZoneId;
        public TimeZoneIdFilter(int timeZoneId)
        {
            _timeZoneId = timeZoneId;
        }
        public IQueryable<Notification> ApplyFilter(IQueryable<Notification> queryable)
        {
            return queryable.Where(n => n.TimeZoneId == _timeZoneId);
        }
    }
}
