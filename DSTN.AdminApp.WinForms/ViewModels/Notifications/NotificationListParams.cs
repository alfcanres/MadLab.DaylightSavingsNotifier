using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels.Notifications
{
    public record NotificationListParams(
        int ObservedTimeZoneId,
        DateTime? StartDate,
        DateTime? EndDate,
        int RecordsPerPage,
        int CurrentPage
    );
}
