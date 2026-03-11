using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;

namespace DSTN.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRepository<ObservedTimeZone> _ObservedTimeZones;
        private readonly IRepository<Notification> _Notifications;
        private readonly IRepository<EmailConfiguration> _EmailConfigurations;

        public UnitOfWork(
            AppDbContext appDbContext,
            IRepository<ObservedTimeZone> observedTimeZones,
            IRepository<Notification> notifications,
            IRepository<EmailConfiguration> emailConfigurations)
        {
            _appDbContext = appDbContext;
            _ObservedTimeZones = observedTimeZones;
            _Notifications = notifications;
            _EmailConfigurations = emailConfigurations;
        }

        public IRepository<ObservedTimeZone> ObservedTimeZones => _ObservedTimeZones;
        public IRepository<Notification> Notifications => _Notifications;
        public IRepository<EmailConfiguration> EmailConfigurations => _EmailConfigurations;

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
