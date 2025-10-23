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
        bool IsValidTimeZoneId(string id);
        IEnumerable<string> GetSystemTimeZones();
        DateTime? GetDSTStartDate(int year, string systemTimeZoneId);
        DateTime? GetDSTEndDate(int year, string systemTimeZoneId);
        DateTime? GetNextTransitionDate(DateTime currentDate, string timeZoneId);
    }
}
