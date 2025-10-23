using DSTN.Infrastructure;

namespace DSTN.Application.Tests
{
    public class SystemTimeZoneProviderTests : SystemTimeZoneProvider
    {


        [Fact]
        public void FindSystemTimeZoneById_ValidId_ReturnsTimeZoneId()
        {
            // Arrange
            var testTimeZone = TestData.GetDstObservingTimeZoneIds().First();

            // Act
            var result = FindSystemTimeZoneById(testTimeZone.TimeZoneId);
            // Assert
            Assert.Equal(testTimeZone.TimeZoneId, result);
        }

        [Fact]
        public void FindSystemTimeZoneById_InvalidId_ThrowsTimeZoneNotFoundException()
        {
            // Arrange
            var invalidId = "Invalid Time Zone";
            // Act & Assert
            Assert.Throws<TimeZoneNotFoundException>(() => FindSystemTimeZoneById(invalidId));
        }

        [Fact]
        public void GetNextTransitionDate_TimeZoneWithoutDST_ReturnsNull()
        {
            // Arrange
            var testTimeZone = TestData.GetDstObservingTimeZoneIds().First(tz => tz.ObservesDST == false);
            var currentDate = new DateTime(2023, 1, 1);
            var timeZoneId = testTimeZone.TimeZoneId;
            // Act
            var result = GetNextTransitionDate(currentDate, timeZoneId);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetNextTransitionDate_TimeZoneWithDST_ReturnsNextTransitionDate_For_YearStart()
        {
            // Arrange
            var currentDate = new DateTime(2025, 1, 1);
            var testTimeZone = TestData.GetDstObservingTimeZoneIds().First(tz => tz.ObservesDST == true);
            var timeZoneId = testTimeZone.TimeZoneId;
            // Act
            var result = GetNextTransitionDate(currentDate, timeZoneId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(testTimeZone.DSTStarts, result);
        }

        [Fact]
        public void GetNextTransitionDate_TimeZoneWithDST_ReturnsNextTransitionDate_For_YearEnd()
        {
            // Arrange
            var currentDate = new DateTime(2025, 10, 1);
            var testTimeZone = TestData.GetDstObservingTimeZoneIds().First(tz => tz.ObservesDST == true);
            var timeZoneId = testTimeZone.TimeZoneId;
            // Act
            var result = GetNextTransitionDate(currentDate, timeZoneId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(testTimeZone.DSTEnds, result);
        }

        [Fact]
        public void SupportsDaylightSavingTime_TimeZoneWithDST_ReturnsTrue()
        {
            // Arrange
            var year = 2025;    
            var timeZoneId = TestData.GetDstObservingTimeZoneIds().First(tz => tz.ObservesDST == true).TimeZoneId;
            // Act
            var result = SupportsDaylightSavingTime(timeZoneId, year);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void SupportsDaylightSavingTime_TimeZoneWithoutDST_ReturnsFalse()
        {
            // Arrange
            int year = 2025;
            var timeZone = TestData.GetDstObservingTimeZoneIds().First(tz => tz.ObservesDST == false);
            // Act
            var result = SupportsDaylightSavingTime(timeZone.TimeZoneId, year);
            // Assert
            Assert.False(result);
        }


        [Fact]
        public void SupportsDaylightSavingTime_TimeZoneWithDST_That_No_LongerObservesDST_ReturnsFalse()
        {
            // Arrange

            var timeZoneId = "Mountain Standard Time (Mexico)";
            var year = 2025;
            // Act
            var result = SupportsDaylightSavingTime(timeZoneId, year);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetDSTTransitionDate_ValidYearAndTimeZoneId_ReturnsTransitionDate()
        {

            // Arrange
            var year = 2025;
            var testTz = TestData.GetDstObservingTimeZoneIds().First(tz => tz.TimeZoneId == "Pacific Standard Time");
            // Act
            var startTransition = GetDSTStartDate(year, testTz.TimeZoneId);
            var endTransition = GetDSTEndDate(year, testTz.TimeZoneId);
            // Assert
            Assert.Equal(testTz.DSTStarts, startTransition); 
            Assert.Equal(testTz.DSTEnds, endTransition);  
        }

        [Fact]
        public void GetDSTTransitionDate_YearWithoutDST_ReturnsNull()
        {
            // Arrange
            var year = 2025;
            var timeZoneId = TestData.GetDstObservingTimeZoneIds().First(tz => tz.ObservesDST == false).TimeZoneId;
            // Act
            var startTransition = GetDSTStartDate(year, timeZoneId);
            var endTransition = GetDSTEndDate(year, timeZoneId);
            // Assert
            Assert.Null(startTransition);
            Assert.Null(endTransition);
        }

    }
}
