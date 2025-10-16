using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels.TimeZones
{
    public class ObservedTimeZoneForList
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string SystemTimeZoneId { get; set; }
        public DateTime? NextTransitionDate { get; set; }
        public string Comments { get; set; }
        public DateTime? DSTStarts { get; set; }
        public DateTime? DSTEnds { get; set; }
        public DateTime? LastChanged { get; set; }
        public bool TimeZoneObservesDST { get; set; }
        public bool IsActive { get; set; }
        public string NotificationSchedule { get; set; }
        public int NotificationsCount { get; set; }

        // Add this property for formatted display

    }
}
