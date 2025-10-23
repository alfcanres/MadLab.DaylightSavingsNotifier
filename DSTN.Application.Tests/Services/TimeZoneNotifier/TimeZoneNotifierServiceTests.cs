using DSTN.Application.DTO;
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
        private readonly IQueryBuilder<Notification> _timeZoneNotificationQueryBuilder;
        private readonly ISystemTimeZoneProvider _systemTimeZoneProvider;
        private readonly Mock<ILogger<TimeZoneNotifierService>> _mockLogger;
        private readonly TimeZoneNotifierService _timeZoneNotifierService;
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
            _systemTimeZoneProvider = new SystemTimeZoneProvider();
            _timeZoneNotificationQueryBuilder = new QueryBuilder<Notification>(dbContext);
            _systemTimeZoneProvider = new SystemTimeZoneProvider();
            _mockLogger = new Mock<ILogger<TimeZoneNotifierService>>();



            _timeZoneNotifierService = new TimeZoneNotifierService(
                _unitOfWork,
                _mockLogger.Object,
                _timeZoneNotificationQueryBuilder,
                _systemTimeZoneProvider
            );

        }


        #region TESTS FOR UpdateDSTForObservedTimeZones

        [Fact]
        public async Task UpdateDSTForObservedTimeZones_UpdatesDSTFields_ForActiveTimeZones()
        {
            // Arrange

            var pacificStandardTimeExpected = TestData.GetPacificStandardTimeWithDST();
            var estaerIslandStandardTimeExpected = TestData.GetEasterIslandStandardTimeWithDST();


            var today = new DateTime(2025, 1, 1);
            var tz1 = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "Pacific Standard Time",
                DisplayName = "Pacific Standard Time",
                IsActive = true,
                TimeZoneObservesDST = false,
                NotifyDaysBefore = 1
            };
            var tz2 = new ObservedTimeZone
            {
                Id = 2,
                TimeZoneId = "Easter Island Standard Time",
                DisplayName = "Easter Island Standard Time",
                IsActive = true,
                TimeZoneObservesDST = false,
                NotifyDaysBefore = 1
            };
            dbContext.TimeZones.AddRange(tz1, tz2);
            await dbContext.SaveChangesAsync();



            // Act
            var result = await _timeZoneNotifierService.UpdateDSTForObservedTimeZones(today);

            // Assert
            var pacificStandardTimeActual = dbContext.TimeZones.First(tz => tz.Id == 1);
            var estaerIslandStandardTimeActual = dbContext.TimeZones.First(tz => tz.Id == 2);

            Assert.True(result.ValidatorResponse.IsValid);

            Assert.True(pacificStandardTimeActual.TimeZoneObservesDST);
            Assert.Equal(pacificStandardTimeExpected.DSTStarts, pacificStandardTimeActual.DSTStarts);
            Assert.Equal(pacificStandardTimeExpected.DSTEnds, pacificStandardTimeActual.DSTEnds);

            Assert.True(estaerIslandStandardTimeActual.TimeZoneObservesDST);
            Assert.Equal(estaerIslandStandardTimeActual.DSTStarts, estaerIslandStandardTimeExpected.DSTStarts);
            Assert.Equal(estaerIslandStandardTimeActual.DSTEnds, estaerIslandStandardTimeExpected.DSTEnds);


        }

        [Fact]
        public async Task UpdateDSTForObservedTimeZones_DoesNotUpdateInactiveTimeZones()
        {
            // Arrange
            var today = new DateTime(2025, 1, 1);
            var tzNoDST = TestData.GetMountainStandardTimeNoDST();

            var inactiveTz = new ObservedTimeZone
            {
                Id = 3,
                TimeZoneId = tzNoDST.TimeZoneId,
                DisplayName = tzNoDST.TimeZoneId,
                IsActive = false,
                TimeZoneObservesDST = false
            };
            dbContext.TimeZones.Add(inactiveTz);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.UpdateDSTForObservedTimeZones(today);

            // Assert
            var updatedInactiveTz = dbContext.TimeZones.First(tz => tz.Id == 3);

            Assert.True(result.ValidatorResponse.IsValid);
            Assert.False(updatedInactiveTz.TimeZoneObservesDST);
            Assert.Null(updatedInactiveTz.DSTStarts);
            Assert.Null(updatedInactiveTz.DSTEnds);

        }

        [Fact]
        public async Task UpdateDSTForObservedTimeZones_Updates_NextNotificationDateForDSTEnd()
        {
            // Arrange
            var today = new DateTime(2025, 5, 1);

            int pacifictStdNotifyDaysBefore = 1;
            int estaerIslandStdNotifyDaysBefore = 10;

            var pacificStandardTimeExpected = TestData.GetPacificStandardTimeWithDST();
            var estaerIslandStandardTimeExpected = TestData.GetEasterIslandStandardTimeWithDST();


            DateTime? pacifictStdNextDST = _systemTimeZoneProvider.GetNextTransitionDate(today, pacificStandardTimeExpected.TimeZoneId);
            DateTime? estaerIslandStdNextDST = _systemTimeZoneProvider.GetNextTransitionDate(today, estaerIslandStandardTimeExpected.TimeZoneId);


            var expectedNextNotificationDateForPacificStd = pacifictStdNextDST!.Value.AddDays(-pacifictStdNotifyDaysBefore);
            var expectedNextNotificationDateForEasterIsland = estaerIslandStdNextDST!.Value.AddDays(-estaerIslandStdNotifyDaysBefore);


          
            var tz1 = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "Pacific Standard Time",
                DisplayName = "Pacific Standard Time",
                IsActive = true,
                TimeZoneObservesDST = false,
                NotifyDaysBefore = pacifictStdNotifyDaysBefore
            };
            var tz2 = new ObservedTimeZone
            {
                Id = 2,
                TimeZoneId = "Easter Island Standard Time",
                DisplayName = "Easter Island Standard Time",
                IsActive = true,
                TimeZoneObservesDST = false,
                NotifyDaysBefore = estaerIslandStdNotifyDaysBefore
            };
            dbContext.TimeZones.AddRange(tz1, tz2);
            await dbContext.SaveChangesAsync();



            // Act
            var result = await _timeZoneNotifierService.UpdateDSTForObservedTimeZones(today);

            // Assert
            var pacificStandardTimeActual = dbContext.TimeZones.First(tz => tz.Id == 1);
            var estaerIslandStandardTimeActual = dbContext.TimeZones.First(tz => tz.Id == 2);

            Assert.True(result.ValidatorResponse.IsValid);

            Assert.True(pacificStandardTimeActual.TimeZoneObservesDST);
            Assert.Equal(expectedNextNotificationDateForPacificStd, pacificStandardTimeActual.NextNotificationDate);

            Assert.True(estaerIslandStandardTimeActual.TimeZoneObservesDST);
            Assert.Equal(expectedNextNotificationDateForEasterIsland, estaerIslandStandardTimeActual.NextNotificationDate);


        }

        #endregion


        #region TESTS FOR ScanTimeZonesForNotification

        [Fact]
        public async Task ScanTimeZonesForNotification_ReturnsTimeZonesMissingNotifications()
        {
            // Arrange
            var today = DateTime.Now;
            var tz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ",
                DisplayName = "TEST TZ",
                IsActive = true,
                TimeZoneObservesDST = true,
                DSTStarts = today,
                DSTEnds = today.AddDays(5),
                NextNotificationDate = today,
                NotifyDaysBefore = 1
            };
            dbContext.TimeZones.Add(tz);

            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.ScanTimeZonesForNotification(today);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(tz.Id, result.Data.First().Id);

        }

        [Fact]
        public async Task ScanTimeZonesForNotification_DoesNotReturnsTimeZonesForFutureNotifications()
        {
            // Arrange
            var today = DateTime.Now;
            var tz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ",
                DisplayName = "TEST TZ",
                IsActive = true,
                TimeZoneObservesDST = true,
                DSTStarts = today.AddDays(1),
                DSTEnds = today.AddDays(5),
                NextNotificationDate = today.AddDays(1), //Notification should be tomorrow
                NotifyDaysBefore = 1
            };
            dbContext.TimeZones.Add(tz);

            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.ScanTimeZonesForNotification(today);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Empty(result.Data);


        }




        #endregion

        [Fact]
        public async Task CreateNotification_ShouldCreate()
        {
            // Arrange
            var today = DateTime.Now;


            var tz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ",
                DisplayName = "TEST TZ",
                IsActive = true,
                TimeZoneObservesDST = true,
                DSTStarts = today.AddDays(1),
                DSTEnds = today.AddDays(5),
                NextNotificationDate = today,
                NextTransitionDate = today,
                NotifyDaysBefore = 1
            };
            dbContext.TimeZones.Add(tz);


            await dbContext.SaveChangesAsync();

            var tzDTO = ObservedTimeZoneDTO.FromEntity(tz);

            // Act
            var result = await _timeZoneNotifierService.CreateNotificationAsync(tz.Id);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Equal(result.Data.DSTTransition, today);

        }


        [Fact]
        public async Task CreateNotification_ShouldCreateWithTimeZoneDisplayName()
        {
            // Arrange
            var today = DateTime.Now;


            var tz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ",
                DisplayName = "TEST TZ",
                IsActive = true,
                TimeZoneObservesDST = true,
                DSTStarts = today.AddDays(1),
                DSTEnds = today.AddDays(5),
                NextNotificationDate = today,
                NextTransitionDate = today,
                NotifyDaysBefore = 1
            };
            dbContext.TimeZones.Add(tz);


            await dbContext.SaveChangesAsync();

            var tzDTO = ObservedTimeZoneDTO.FromEntity(tz);

            // Act
            var result = await _timeZoneNotifierService.CreateNotificationAsync(tz.Id);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Equal(result.Data.DSTTransition, today);
            Assert.Equal(result.Data.TimeZoneDisplayName, tz.DisplayName);

        }

    }
}
