using DSTN.Application.Services.TimeZoneConfigurator;
using DSTN.Application.Services.TimeZoneNotifier;
using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;
using DSTN.Infrastructure;
using DSTN.Infrastructure.Persistence;
using DSTN.Infrastructure.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace DSTN.Application.Tests
{
    public class TimeZoneNotifierServiceTests
    {

        private readonly IRepository<Notification> _timeZoneNotificationRepository;
        private readonly IRepository<ObservedTimeZone> _observedTimeZoneRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mock<ILogger<TimeZoneNotifierService>> _timeZoneNotifierServiceMockLogger;
        private readonly IQueryBuilder<Notification> _timeZoneNotificationQueryBuilder;
        private readonly ISystemTimeZoneProvider _systemTimeZoneProvider;

        private readonly AppDbContext dbContext;

        public TimeZoneNotifierServiceTests()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new AppDbContext(dbOptions);

            _observedTimeZoneRepository = new Repository<ObservedTimeZone>(dbContext);
            _timeZoneNotificationRepository = new Repository<Notification>(dbContext);
            _unitOfWork = new UnitOfWork(dbContext, _observedTimeZoneRepository, _timeZoneNotificationRepository);
            _timeZoneNotifierServiceMockLogger = new Mock<ILogger<TimeZoneNotifierService>>();
            _timeZoneNotificationQueryBuilder = new QueryBuilder<Notification>(dbContext);
            _systemTimeZoneProvider = new SystemTimeZoneProvider();


        }


        #region TESTS FOR UpdateDSTForObservedTimeZones
        [Fact]
        public async Task UpdateDSTForObservedTimeZones_UpdatesDSTFields_ForActiveTimeZones()
        {
            // Arrange
            var year = 2025;
            var tz1 = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "Europe/London",
                DisplayName = "Europe/London",
                IsActive = true,
                TimeZoneObservesDST = false
            };
            var tz2 = new ObservedTimeZone
            {
                Id = 2,
                TimeZoneId = "America/New_York",
                DisplayName = "America/New_York",
                IsActive = true,
                TimeZoneObservesDST = false
            };
            dbContext.TimeZones.AddRange(tz1, tz2);
            await dbContext.SaveChangesAsync();

            var systemTimeZoneProviderMock = new Mock<ISystemTimeZoneProvider>();
            systemTimeZoneProviderMock
                .Setup(p => p.SupportsDaylightSavingTime("Europe/London", year))
                .Returns(false);
            systemTimeZoneProviderMock
                .Setup(p => p.SupportsDaylightSavingTime("America/New_York", year))
                .Returns(true);
            systemTimeZoneProviderMock
                .Setup(p => p.GetDSTTransitionDate(year, "Europe/London", true))
                .Returns(new DateTime(year, 3, 31));
            systemTimeZoneProviderMock
                .Setup(p => p.GetDSTTransitionDate(year, "Europe/London", false))
                .Returns(new DateTime(year, 10, 27));

            var service = new TimeZoneNotifierService(
                _unitOfWork,
                _timeZoneNotifierServiceMockLogger.Object,
                _timeZoneNotificationQueryBuilder,
                systemTimeZoneProviderMock.Object
            );

            // Act
            var result = await service.UpdateDSTForObservedTimeZones(year);

            // Assert
            var updatedTz1 = dbContext.TimeZones.First(tz => tz.Id == 1);
            var updatedTz2 = dbContext.TimeZones.First(tz => tz.Id == 2);

            Assert.True(updatedTz1.TimeZoneObservesDST);
            Assert.Equal(new DateTime(year, 3, 31), updatedTz1.DSTStarts);
            Assert.Equal(new DateTime(year, 10, 27), updatedTz1.DSTEnds);

            Assert.False(updatedTz2.TimeZoneObservesDST);
            Assert.Null(updatedTz2.DSTStarts);
            Assert.Null(updatedTz2.DSTEnds);

            Assert.True(result.ValidatorResponse.IsValid);
        }

        [Fact]
        public async Task UpdateDSTForObservedTimeZones_DoesNotUpdateInactiveTimeZones()
        {
            // Arrange
            var year = 2025;
            var inactiveTz = new ObservedTimeZone
            {
                Id = 3,
                TimeZoneId = "Asia/Tokyo",
                DisplayName = "Asia/Tokyo",
                IsActive = false,
                TimeZoneObservesDST = false
            };
            dbContext.TimeZones.Add(inactiveTz);
            await dbContext.SaveChangesAsync();

            var systemTimeZoneProviderMock = new Mock<ISystemTimeZoneProvider>();
            var service = new TimeZoneNotifierService(
                _unitOfWork,
                _timeZoneNotifierServiceMockLogger.Object,
                _timeZoneNotificationQueryBuilder,
                systemTimeZoneProviderMock.Object
            );

            // Act
            var result = await service.UpdateDSTForObservedTimeZones(year);

            // Assert
            var updatedInactiveTz = dbContext.TimeZones.First(tz => tz.Id == 3);
            Assert.False(updatedInactiveTz.TimeZoneObservesDST);
            Assert.Null(updatedInactiveTz.DSTStarts);
            Assert.Null(updatedInactiveTz.DSTEnds);
            Assert.True(result.ValidatorResponse.IsValid);
        }

        [Fact]
        public async Task UpdateDSTForObservedTimeZones_HandlesException_AndSetsValidatorInvalid()
        {
            // Arrange
            var year = 2025;
            var tz = new ObservedTimeZone
            {
                Id = 4,
                TimeZoneId = "Europe/Berlin",
                DisplayName = "Europe/Berlin",
                IsActive = true,
                TimeZoneObservesDST = false
            };
            dbContext.TimeZones.Add(tz);
            await dbContext.SaveChangesAsync();

            var systemTimeZoneProviderMock = new Mock<ISystemTimeZoneProvider>();
            systemTimeZoneProviderMock
                .Setup(p => p.SupportsDaylightSavingTime(It.IsAny<string>(), year))
                .Throws(new Exception("Test exception"));

            var service = new TimeZoneNotifierService(
                _unitOfWork,
                _timeZoneNotifierServiceMockLogger.Object,
                _timeZoneNotificationQueryBuilder,
                systemTimeZoneProviderMock.Object
            );

            // Act
            var result = await service.UpdateDSTForObservedTimeZones(year);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Contains("Unexpected error", result.ValidatorResponse.MessageList.FirstOrDefault());
        }
        #endregion


        #region TESTS FOR ScanTimeZonesForNotification

        [Fact]
        public async Task ScanTimeZonesForNotification_ReturnsTimeZonesMissingNotifications()
        {
            // Arrange
            var systemTimeZoneProviderMock = new Mock<ISystemTimeZoneProvider>();
            var today = DateTime.UtcNow;
            var tz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "Europe/London",
                DisplayName = "Europe/London",  
                IsActive = true,
                TimeZoneObservesDST = true,
                DSTStarts = today.AddDays(10),
                DSTEnds = today.AddDays(100)
            };
            dbContext.TimeZones.Add(tz);

            // Only one notification exists (missing one for DSTEnds)
            dbContext.Notifications.Add(new Notification
            {
                Id = 1,
                TimeZoneId = tz.Id,
                DSTTransition = tz.DSTStarts.Value,
                NotifyDate = today.AddDays(5),
                Message = "DST starts soon",
                WasRead = false
            });

            await dbContext.SaveChangesAsync();

            var service = new TimeZoneNotifierService(
                _unitOfWork,
                _timeZoneNotifierServiceMockLogger.Object,
                _timeZoneNotificationQueryBuilder,
                systemTimeZoneProviderMock.Object
            );

            // Act
            var result = await service.ScanTimeZonesForNotification(today);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(tz.Id, result.Data.First().Id);
            Assert.True(result.ValidatorResponse.IsValid);
        }

        [Fact]
        public async Task ScanTimeZonesForNotification_DoesNotReturnTimeZonesWithBothNotifications()
        {
            // Arrange
            var today = DateTime.UtcNow;
            var tz = new ObservedTimeZone
            {
                Id = 2,
                TimeZoneId = "America/New_York",
                DisplayName = "America/New_York",
                IsActive = true,
                TimeZoneObservesDST = true,
                DSTStarts = today.AddDays(20),
                DSTEnds = today.AddDays(200)
            };
            dbContext.TimeZones.Add(tz);

            dbContext.Notifications.AddRange(
                new Notification
                {
                    Id = 2,
                    TimeZoneId = tz.Id,
                    DSTTransition = tz.DSTStarts.Value,
                    NotifyDate = today.AddDays(15),
                    Message = "DST starts soon",
                    WasRead = false
                },
                new Notification
                {
                    Id = 3,
                    TimeZoneId = tz.Id,
                    DSTTransition = tz.DSTEnds.Value,
                    NotifyDate = today.AddDays(195),
                    Message = "DST ends soon",
                    WasRead = false
                }
            );

            await dbContext.SaveChangesAsync();

            var service = new TimeZoneNotifierService(
                _unitOfWork,
                _timeZoneNotifierServiceMockLogger.Object,
                _timeZoneNotificationQueryBuilder,
                _systemTimeZoneProvider
            );

            // Act
            var result = await service.ScanTimeZonesForNotification(today);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            Assert.True(result.ValidatorResponse.IsValid);
        }

        [Fact]
        public async Task ScanTimeZonesForNotification_IgnoresInactiveOrNonDSTTimeZones()
        {
            // Arrange
            var today = DateTime.UtcNow;
            var inactiveTz = new ObservedTimeZone
            {
                Id = 3,
                TimeZoneId = "Asia/Tokyo",
                DisplayName = "Asia/Tokyo",
                IsActive = false,
                TimeZoneObservesDST = true
            };
            var noDstTz = new ObservedTimeZone
            {
                Id = 4,
                TimeZoneId = "Africa/Cairo",
                DisplayName = "Africa/Cairo",
                IsActive = true,
                TimeZoneObservesDST = false
            };
            dbContext.TimeZones.AddRange(inactiveTz, noDstTz);
            await dbContext.SaveChangesAsync();

            var service = new TimeZoneNotifierService(
                _unitOfWork,
                _timeZoneNotifierServiceMockLogger.Object,
                _timeZoneNotificationQueryBuilder,
                _systemTimeZoneProvider
            );

            // Act
            var result = await service.ScanTimeZonesForNotification(today);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            Assert.True(result.ValidatorResponse.IsValid);
        }
        #endregion

    }
}
