using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;


namespace DSTN.Application.Services.TimeZoneNotifier.Filters
{
    internal class DateRangeFilter : IQueryFilter<Notification>
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        public DateRangeFilter(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }
        public IQueryable<Notification> ApplyFilter(IQueryable<Notification> queryable)
        {
            return queryable.Where(n => n.DSTTransition >= _startDate && n.DSTTransition <= _endDate)
                            .OrderBy(n => n.DSTTransition);
        }
    }
}
