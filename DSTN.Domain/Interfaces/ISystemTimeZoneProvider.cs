using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.Domain.Interfaces
{
    public interface ISystemTimeZoneProvider
    {
        bool SupportsDaylightSavingTime(string id, int year);
        string FindSystemTimeZoneById(string id);
        IEnumerable<string> GetSystemTimeZones();
        DateTime? GetDSTTransitionDate(int year, string systemTimeZoneId, bool isStart);
        DateTime? GetNextTransitionDate(DateTime currentDate, string timeZoneId);
        DateTime? GetNotificationDate(DateTime? DSTStartOrEnds, int notifyDaysBefore);
    }
}
