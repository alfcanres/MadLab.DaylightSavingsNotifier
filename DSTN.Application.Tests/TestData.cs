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
                    TimeZoneId = "Mountain Standard Time (Mexico)",
                    ObservesDST = false
                },
                new TestingTimezones
                {
                    TimeZoneId = "Pacific Standard Time",
                    ObservesDST = true,
                    DSTStarts = new DateTime(2025, 3, 9),
                    DSTEnds = new DateTime(2025, 11, 2)
                },
                new TestingTimezones
                {
                    TimeZoneId = "Easter Island Standard Time",
                    ObservesDST = true,
                    DSTStarts = new DateTime(2025,9,6),
                    DSTEnds = new DateTime(2025,4,5)
                },
                new TestingTimezones
                {
                    TimeZoneId = "Central America Standard Time",
                    ObservesDST = false,
                    DSTStarts = null,
                    DSTEnds = null
                },
                new TestingTimezones
                {
                    TimeZoneId = "Central Brazilian Standard Time",
                    ObservesDST = false,
                    DSTStarts = null,
                    DSTEnds = null
                },
                new TestingTimezones
                {
                    TimeZoneId = "Alaskan Standard Time",
                    ObservesDST = true,
                    DSTStarts = new DateTime(2025,3,9),
                    DSTEnds = new DateTime(2025,11,2)
                },
                new TestingTimezones
                {
                    TimeZoneId = "West Bank Standard Time",
                    ObservesDST = true,
                    DSTStarts = new DateTime(2025,4,12),
                    DSTEnds = new DateTime(2025,10,25)
                },
                new TestingTimezones
                {
                    TimeZoneId = "Central Europe Standard Time",
                    ObservesDST = true,
                    DSTStarts = new DateTime(2025,3,30),
                    DSTEnds = new DateTime(2025,10,26)
                },
                new TestingTimezones
                {
                    TimeZoneId = "Eastern Standard Time",
                    ObservesDST = true,
                    DSTStarts = new DateTime(2025,3,9),
                    DSTEnds = new DateTime(2025,11,2)
                }
            };

            return timezones;
        }


        public static TestingTimezones GetMountainStandardTimeNoDST()
        {
            return GetDstObservingTimeZoneIds().First(tz => tz.TimeZoneId == "Mountain Standard Time (Mexico)");
        }

        public static TestingTimezones GetPacificStandardTimeWithDST()
        {
            return GetDstObservingTimeZoneIds().First(tz => tz.TimeZoneId == "Pacific Standard Time");
        }

        public static TestingTimezones GetEasterIslandStandardTimeWithDST()
        {
            return GetDstObservingTimeZoneIds().First(tz => tz.TimeZoneId == "Easter Island Standard Time");
        }

        public static TestingTimezones GetCentralAmericaStandardTimeNoDST()
        {
            return GetDstObservingTimeZoneIds().First(tz => tz.TimeZoneId == "Central America Standard Time");
        }

        public static TestingTimezones GetCentralBrazilianStandardTimeNoDST()
        {
            return GetDstObservingTimeZoneIds().First(tz => tz.TimeZoneId == "Central Brazilian Standard Time");
        }
    }
}
