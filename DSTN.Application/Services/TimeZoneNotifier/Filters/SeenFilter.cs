using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;


namespace DSTN.Application.Services.TimeZoneNotifier.Filters
{
    internal class SeenFilter : IQueryFilter<Notification>
    {
        private readonly bool? _unread;
        public SeenFilter(bool? unread)
        {
            _unread = unread;
        }
        public IQueryable<Notification> ApplyFilter(IQueryable<Notification> queryable)
        {
            if (!_unread.HasValue)
            {
                return queryable;
            }
            else
            {
                return queryable.Where(n => n.WasRead == _unread.Value);
            }
        }
    }
}
