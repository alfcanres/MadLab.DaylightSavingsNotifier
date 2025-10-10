using DSTN.AdminApp.WinForms.ViewModels.TimeZones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels.Notifications
{
    public record NotificationRead(
        int Id,
        int TimeZoneId,
        string TimeZoneDisplayName,
        DateTime DSTTransition,
        DateTime NotifyDate,
        string Message,
        DateTime CreatedAt,
        bool WasRead,
        DateTime? ReadAt
    );
}
