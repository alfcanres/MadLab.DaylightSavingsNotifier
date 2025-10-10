using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels.TimeZones
{
    public record ObservedTimeZone(
        int Id,
        int CountryId,
        string Color,
        string DisplayName,
        string Comments,
        string TimeZoneId,
        DateTime? DSTStarts,
        DateTime? DSTEnds,
        DateTime? LastChanged,
        bool TimeZoneObservesDST,
        DateTime? NextTransitionDate,
        bool IsActive,
        int NotifyDaysBefore
    );
}
