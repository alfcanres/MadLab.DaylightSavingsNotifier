using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;

namespace DSTN.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRepository<ObservedTimeZone> _ObservedTimeZones;
        private readonly IRepository<Notification> _Notifications;

        public UnitOfWork(
            AppDbContext appDbContext,
            IRepository<ObservedTimeZone> observedTimeZones,
            IRepository<Notification> notifications)
        {
            _appDbContext = appDbContext;
            _ObservedTimeZones = observedTimeZones;
            _Notifications = notifications;
        }

        public IRepository<ObservedTimeZone> ObservedTimeZones => _ObservedTimeZones;
        public IRepository<Notification> Notifications => _Notifications;

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
