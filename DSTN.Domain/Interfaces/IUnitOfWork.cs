using DSTN.Domain.Entities;

namespace DSTN.Domain.Interfaces
{
  
    public interface IUnitOfWork
    {
        public IRepository<ObservedTimeZone> ObservedTimeZones { get; }
        public IRepository<Notification> Notifications { get; }
        Task SaveAsync();
    }
}
