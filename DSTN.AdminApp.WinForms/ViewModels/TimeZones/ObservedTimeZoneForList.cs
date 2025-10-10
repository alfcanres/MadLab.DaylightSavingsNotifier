using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels.TimeZones
{
    public record ObservedTimeZoneForList(
        int Id,
        string Color,
        string DisplayName,
        string Comments,
        DateTime? DSTStarts,
        DateTime? DSTEnds,
        DateTime? LastChanged,
        bool TimeZoneObservesDST,
        DateTime? NextTransitionDate,
        bool IsActive,
        string NotificationSchedule,
        int NotificationsCount
    );
}
