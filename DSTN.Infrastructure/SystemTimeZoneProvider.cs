using DSTN.Domain.Interfaces;
using static System.TimeZoneInfo;

namespace DSTN.Infrastructure
{
    /// <summary>
    /// Provides system timezone information and daylight saving time (DST) operations.
    /// This class wraps the .NET TimeZoneInfo API to provide comprehensive timezone and DST functionality.
    /// </summary>
    public class SystemTimeZoneProvider : ISystemTimeZoneProvider
    {
        /// <summary>
        /// Determines whether the specified timezone supports daylight saving time for a given year.
        /// </summary>
        /// <param name="id">The timezone identifier (e.g., "Pacific Standard Time", "UTC").</param>
        /// <param name="year">The year to check for DST support.</param>
        /// <returns>True if the timezone supports DST in the specified year; otherwise, false.</returns>
        /// <exception cref="TimeZoneNotFoundException">Thrown when the timezone ID is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the timezone ID is null.</exception>
        public bool SupportsDaylightSavingTime(string id, int year)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id), "Timezone ID cannot be null or empty.");

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(id);

            // Check if the timezone has any adjustment rules for the specified year
            if (!timeZone.SupportsDaylightSavingTime)
                return false;

            // Get adjustment rules and check if any apply to the specified year
            var adjustmentRules = timeZone.GetAdjustmentRules();
            return adjustmentRules.Any(rule =>
                rule.DateStart.Year <= year && rule.DateEnd.Year >= year);
        }

        /// <summary>
        /// Finds and validates a system timezone by its identifier.
        /// </summary>
        /// <param name="id">The timezone identifier to find.</param>
        /// <returns>The timezone ID if found; otherwise, null.</returns>
        public string FindSystemTimeZoneById(string id)
        {
 
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(id);
            return timeZone.Id;

        }

        /// <summary>
        /// Validates whether the specified timezone identifier exists in the system.
        /// </summary>
        /// <param name="id">The timezone identifier to validate.</param>
        /// <returns>True if the timezone ID is valid and exists; otherwise, false.</returns>
        public bool IsValidTimeZoneId(string id)
        {
            try
            {
                var timezone = TimeZoneInfo.FindSystemTimeZoneById(id);
                return timezone != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves all available system timezone identifiers.
        /// </summary>
        /// <returns>An enumerable collection of all system timezone IDs.</returns>
        public IEnumerable<string> GetSystemTimeZones()
        {
            return TimeZoneInfo.GetSystemTimeZones().Select(tz => tz.Id);
        }

        /// <summary>
        /// Gets the daylight saving time start date for a specific year and timezone.
        /// </summary>
        /// <param name="year">The year to check for DST start date.</param>
        /// <param name="systemTimeZoneId">The timezone identifier.</param>
        /// <returns>The DST start date if applicable; otherwise, null.</returns>
        /// <exception cref="TimeZoneNotFoundException">Thrown when the timezone ID is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the timezone ID is null.</exception>
        public DateTime? GetDSTStartDate(int year, string systemTimeZoneId)
        {
            if (string.IsNullOrWhiteSpace(systemTimeZoneId))
                throw new ArgumentNullException(nameof(systemTimeZoneId), "Timezone ID cannot be null or empty.");

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(systemTimeZoneId);

            if (!timeZone.SupportsDaylightSavingTime)
                return null;

            // Get the adjustment rule for the specified year
            var adjustmentRule = timeZone.GetAdjustmentRules()
                .FirstOrDefault(rule => rule.DateStart.Year <= year && rule.DateEnd.Year >= year);

            if (adjustmentRule == null)
                return null;

            // Calculate the DST start date based on the transition time
            return GetTransitionDate(year, adjustmentRule.DaylightTransitionStart, timeZone);
        }

        /// <summary>
        /// Gets the daylight saving time end date for a specific year and timezone.
        /// </summary>
        /// <param name="year">The year to check for DST end date.</param>
        /// <param name="systemTimeZoneId">The timezone identifier.</param>
        /// <returns>The DST end date if applicable; otherwise, null.</returns>
        /// <exception cref="TimeZoneNotFoundException">Thrown when the timezone ID is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the timezone ID is null.</exception>
        public DateTime? GetDSTEndDate(int year, string systemTimeZoneId)
        {
            if (string.IsNullOrWhiteSpace(systemTimeZoneId))
                throw new ArgumentNullException(nameof(systemTimeZoneId), "Timezone ID cannot be null or empty.");

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(systemTimeZoneId);

            if (!timeZone.SupportsDaylightSavingTime)
                return null;

            // Get the adjustment rule for the specified year
            var adjustmentRule = timeZone.GetAdjustmentRules()
                 .FirstOrDefault(rule => rule.DateStart.Year <= year && rule.DateEnd.Year >= year);

            if (adjustmentRule == null)
                return null;

            // Calculate the DST end date based on the transition time
            return GetTransitionDate(year, adjustmentRule.DaylightTransitionEnd, timeZone);
        }

        /// <summary>
        /// Gets the next timezone transition date (DST start or end) after the specified date.
        /// </summary>
        /// <param name="currentDate">The date from which to find the next transition.</param>
        /// <param name="timeZoneId">The timezone identifier.</param>
        /// <returns>The next transition date if one exists; otherwise, null.</returns>
        /// <exception cref="TimeZoneNotFoundException">Thrown when the timezone ID is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the timezone ID is null.</exception>
        public DateTime? GetNextTransitionDate(DateTime currentDate, string timeZoneId)
        {
            if (string.IsNullOrWhiteSpace(timeZoneId))
                throw new ArgumentNullException(nameof(timeZoneId), "Timezone ID cannot be null or empty.");

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            if (!timeZone.SupportsDaylightSavingTime)
                return null;

            // Check transitions in current year and next few years
            for (int yearOffset = 0; yearOffset <= 5; yearOffset++)
            {
                int checkYear = currentDate.Year + yearOffset;

                var dstStart = GetDSTStartDate(checkYear, timeZoneId);
                var dstEnd = GetDSTEndDate(checkYear, timeZoneId);

                // Check if DST start is after current date
                if (dstStart.HasValue && dstStart.Value > currentDate)
                    return dstStart.Value;

                // Check if DST end is after current date
                if (dstEnd.HasValue && dstEnd.Value > currentDate)
                    return dstEnd.Value;
            }

            return null;
        }

        /// <summary>
        /// Helper method to calculate the actual transition date based on a transition time rule.
        /// </summary>
        /// <param name="year">The year for which to calculate the transition.</param>
        /// <param name="transitionTime">The transition time rule.</param>
        /// <param name="timeZone">The timezone information.</param>
        /// <returns>The calculated transition date.</returns>
        private DateTime GetTransitionDate(int year, TransitionTime transitionTime, TimeZoneInfo timeZone)
        {
            if (transitionTime.IsFixedDateRule)
            {
                // Fixed date rule: transition occurs on a specific date
                return new DateTime(year, transitionTime.Month, transitionTime.Day);
            }
            else
            {
                // Floating date rule: transition occurs on a specific day of week in a specific week
                // For example: "Second Sunday of March"

                // Get the first day of the month
                DateTime firstDayOfMonth = new DateTime(year, transitionTime.Month, 1,
               transitionTime.TimeOfDay.Hour, transitionTime.TimeOfDay.Minute,
                transitionTime.TimeOfDay.Second);

                // Find the first occurrence of the target day of week
                int daysUntilTarget = ((int)transitionTime.DayOfWeek - (int)firstDayOfMonth.DayOfWeek + 7) % 7;
                DateTime firstOccurrence = firstDayOfMonth.AddDays(daysUntilTarget);

                // Add weeks to get to the target week
                int weeksToAdd = transitionTime.Week == 5
                      ? GetLastWeekOccurrence(firstOccurrence, transitionTime.Month, year)
               : transitionTime.Week - 1;

                var transitionDate = firstOccurrence.AddDays(weeksToAdd * 7);


                return new DateTime(transitionDate.Year, transitionDate.Month, transitionDate.Day);
            }
        }

        /// <summary>
        /// Helper method to determine the last occurrence of a day of week in a month.
        /// Used when the transition rule specifies "last [day of week] of the month".
        /// </summary>
        /// <param name="firstOccurrence">The first occurrence of the target day of week.</param>
        /// <param name="month">The month being evaluated.</param>
        /// <param name="year">The year being evaluated.</param>
        /// <returns>The number of weeks to add to get the last occurrence.</returns>
        private int GetLastWeekOccurrence(DateTime firstOccurrence, int month, int year)
        {
            int weeksToAdd = 0;
            DateTime candidate = firstOccurrence;

            // Keep adding weeks until we would go into the next month
            while (candidate.AddDays(7).Month == month)
            {
                weeksToAdd++;
                candidate = candidate.AddDays(7);
            }

            return weeksToAdd;
        }
    }
}
