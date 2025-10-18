using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.Helpers
{
    public static class DateTimeExtensions
    {
        public static string ToFriendlyDateString(this DateTime date, DateTime? today = null)
        {
            var reference = today?.Date ?? DateTime.Today;
            var target = date.Date;
            var daysDiff = (target - reference).Days;

            if (target.Year > reference.Year)
            {
                return "Next year";
            }
            if (daysDiff > 31)
            {
                return target.ToShortDateString();
            }
            if (target.Month == reference.Month && daysDiff > 13)
            {
                return "This month";
            }
            if (daysDiff >= 14 && daysDiff < 21)
            {
                return "In two weeks";
            }
            if (daysDiff >= 7 && daysDiff < 14)
            {
                return "In one week";
            }
            if (daysDiff >= 2 && daysDiff < 7)
            {
                return $"In {daysDiff} days";
            }
            if (daysDiff == 1)
            {
                return "Tomorrow";
            }
            // Fallback: show the date
            return target.ToShortDateString();
        }
    }
}
