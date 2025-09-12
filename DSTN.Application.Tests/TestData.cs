using DSTN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.Application.Tests
{

    public class TestingTimezones
    {
        public string TimeZoneId { get; set; }
        public bool ObservesDST { get; set; }
        public DateTime? DSTStarts { get; set; }
        public DateTime? DSTEnds { get; set; }
    }

    public static class TestData
    {
        public static IEnumerable<TestingTimezones> GetDstObservingTimeZoneIds()
        {
            List<TestingTimezones> timezones = new List<TestingTimezones>
            {
                new TestingTimezones
                {
                    TimeZoneId = "Mountain Standard Time (Mexico)", // DST used to be observed but not in 2025
                    ObservesDST = false
                },
                new TestingTimezones
                {
                    TimeZoneId = "Pacific Standard Time", // DST 2025 Starts March 9, Ends Nov 2
                    ObservesDST = true,
                    DSTStarts = new DateTime(2025, 3, 9),
                    DSTEnds = new DateTime(2025, 11, 2)
                },
                new TestingTimezones
                {
                    TimeZoneId = "Easter Island Standard Time", // DST 2025 Starts Apr 6, Ends Sept 7
                    ObservesDST = true,
                    DSTStarts = new DateTime(2025,4,6),
                    DSTEnds = new DateTime(2025, 9, 7)
                },
                new TestingTimezones
                {
                    TimeZoneId = "Central America Standard Time", // NO DST
                    ObservesDST = false,
                    DSTStarts = null,
                    DSTEnds = null
                },
                new TestingTimezones
                {
                    TimeZoneId = "Central Brazilian Standard Time", // NO DST
                    ObservesDST = false,
                    DSTStarts = null,
                    DSTEnds = null
                }
            };

            return timezones;
        }
    }
}
