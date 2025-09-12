using DSTN.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.TimeZoneInfo;

namespace DSTN.Infrastructure
{
    public class SystemTimeZoneProvider : ISystemTimeZoneProvider
    {
        public string FindSystemTimeZoneById(string id)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(id);

            if (timezone == null)
                throw new TimeZoneNotFoundException($"Time zone with ID '{id}' not found.");


            return TimeZoneInfo.FindSystemTimeZoneById(id).Id;

        }

        public DateTime? GetDSTTransitionDate(int year, string systemTimeZoneId, bool isStart)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(systemTimeZoneId);

            var rules = timeZoneInfo.GetAdjustmentRules();
            var rule = rules.FirstOrDefault(r => r.DateStart.Year <= year && r.DateEnd.Year >= year);
            if (rule == null)
                return null;

            var transition = isStart ? rule.DaylightTransitionStart : rule.DaylightTransitionEnd;

            return GetDateByWeekOfMonth(year, transition);
        }

        public DateTime? GetNextTransitionDate(DateTime currentDate, string timeZoneId)
        {
            if (SupportsDaylightSavingTime(timeZoneId, currentDate.Year) == false)
            {
                return null;
            }
            else
            {
                DateTime? nextStart = GetDSTTransitionDate(currentDate.Year, timeZoneId, true);
                DateTime? nextEnd = GetDSTTransitionDate(currentDate.Year, timeZoneId, false);
                if (currentDate <= nextStart)
                {
                    return nextStart;
                }
                else if (currentDate <= nextEnd)
                {
                    return nextEnd;
                }
                else
                {
                    nextStart = GetDSTTransitionDate(currentDate.Year + 1, timeZoneId, true);
                    return nextStart;
                }
            }
        }

        public DateTime? GetNotificationDate(DateTime? DSTStartOrEnds, int notifyDaysBefore)
        {
            if (DSTStartOrEnds == null)
            {
                return null;
            }
            return DSTStartOrEnds.Value.AddDays(-notifyDaysBefore);
        }

        public IEnumerable<string> GetSystemTimeZones()
        {
            return TimeZoneInfo.GetSystemTimeZones().Select(tz => tz.Id);
        }

        public bool SupportsDaylightSavingTime(string id, int year)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(id);

            if (timezone == null)
                throw new TimeZoneNotFoundException($"Time zone with ID '{id}' not found.");

            bool hasRuleForYear = timezone.GetAdjustmentRules()
                .Any(r => r.DateStart.Year <= year && r.DateEnd.Year >= year);

            bool supportsDST = timezone.SupportsDaylightSavingTime;


            return supportsDST && hasRuleForYear;
        }

        private DateTime? GetDateByWeekOfMonth(int year, TransitionTime transition)
        {

            int month = transition.Month;
            int weekOfMonth = transition.Week;
            int day = (int)transition.DayOfWeek;

            if (weekOfMonth < 1 || weekOfMonth > 5)
                throw new ArgumentOutOfRangeException(nameof(weekOfMonth), "weekOfMonth must be between 1 and 5.");
            if (day < 0 || day > 6)
                throw new ArgumentOutOfRangeException(nameof(day), "day must be between 0 (Sunday) and 6 (Saturday).");

            int daysInMonth = DateTime.DaysInMonth(year, month);
            int weekCount = 0;

            for (int d = 1; d <= daysInMonth; d++)
            {
                var date = new DateTime(year, month, d);
                if ((int)date.DayOfWeek == day)
                {
                    weekCount++;
                    if (weekCount == weekOfMonth)
                        return date;
                }
            }

            // If the requested week does not exist, return null
            return null;
        }
    }
}
