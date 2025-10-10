using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels.TimeZones
{
    public record AddTimeZoneToObserve(
        string Color,
        string DisplayName,
        string Comments,
        string TimeZoneId,
        bool IsActive,
        int NotifyDaysBefore
    );
}
