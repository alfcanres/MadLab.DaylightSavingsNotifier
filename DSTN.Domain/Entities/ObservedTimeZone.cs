using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.Domain.Entities
{
    public class ObservedTimeZone
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public string DisplayName { get; set; }
        public string? Comments { get; set; } 
        public string TimeZoneId { get; set; }
        public DateTime? DSTStarts { get; set; }
        public DateTime? DSTEnds { get; set; }
        public DateTime? LastChanged { get; set; }
        public bool TimeZoneObservesDST { get; set; }
        public DateTime? NextTransitionDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
        public int NotifyDaysBefore { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
