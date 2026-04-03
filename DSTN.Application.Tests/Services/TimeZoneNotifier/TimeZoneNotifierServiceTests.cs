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
        private readonly IEmailService _emailService;
        public TimeZoneNotifierServiceTests()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new AppDbContext(dbOptions);

            _observedTimeZoneRepository = new Repository<ObservedTimeZone>(dbContext);
            _timeZoneNotificationRepository = new Repository<Notification>(dbContext);
            _unitOfWork = new UnitOfWork(dbContext, _observedTimeZoneRepository, _timeZoneNotificationRepository, new Repository<EmailConfiguration>(dbContext));
            _systemTimeZoneProvider = new SystemTimeZoneProvider();
            _timeZoneNotificationQueryBuilder = new QueryBuilder<Notification>(dbContext);
            _systemTimeZoneProvider = new SystemTimeZoneProvider();
            _mockLogger = new Mock<ILogger<TimeZoneNotifierService>>();
            _emailService = new Mock<IEmailService>().Object;



            _timeZoneNotifierService = new TimeZoneNotifierService(
                _unitOfWork,
                _mockLogger.Object,
                _timeZoneNotificationQueryBuilder,
                _systemTimeZoneProvider,
                _emailService
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


        #region TESTS FOR SendEmailNotificationAsync

        [Fact]
        public async Task SendEmailNotificationAsync_DefaultConfigExists_AllEmailsSent_ReturnsAllSuccess()
        {
            // Arrange
            var emailConfig = new EmailConfiguration
            {
                Id = 1,
                Name = "Default",
                SmtpHost = "smtp.test.com",
                SmtpPort = 25,
                Username = "user",
                Password = "pass",
                SenderEmail = "testing@test.com",
                SenderName = "testing@test.com",
                UseSsl = true,
                IsActive = true,
                IsDefault = true
            };
            dbContext.EmailConfigurations.Add(emailConfig);

            var observedTz = new ObservedTimeZone
            {
                Id = 1,
                DisplayName = "Test TZ",
                TimeZoneId = "Test/TZ",
                ForwardEmailList = "a@test.com;b@test.com",
                IsActive = true
            };
            dbContext.TimeZones.Add(observedTz);
            await dbContext.SaveChangesAsync();

            var notificationDto = new NotificationReadDTO
            {
                Id = 1,
                TimeZoneId = observedTz.Id,
                Message = "Test message"
            };

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock.Setup(x => x.ConfigureCredentials(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));
            emailServiceMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((true, "OK"));

            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);

            // Act
            var result = await notifier.SendEmailNotificationAsync(notificationDto);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Equal(2, result.Data.Count);
            Assert.All(result.Data.Values, v => Assert.True(v));
        }

        [Fact]
        public async Task SendEmailNotificationAsync_DefaultConfigMissing_ReturnsFailure()
        {
            // Arrange
            var observedTz = new ObservedTimeZone
            {
                Id = 2,
                DisplayName = "Test TZ",
                TimeZoneId = "Test/TZ",
                ForwardEmailList = "a@test.com",
                IsActive = true
            };
            dbContext.TimeZones.Add(observedTz);
            await dbContext.SaveChangesAsync();

            var notificationDto = new NotificationReadDTO
            {
                Id = 2,
                TimeZoneId = observedTz.Id,
                Message = "Test message"
            };

            var emailServiceMock = new Mock<IEmailService>();
            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);

            // Act
            var result = await notifier.SendEmailNotificationAsync(notificationDto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Empty(result.Data);
            Assert.Contains(result.ValidatorResponse.MessageList, e => e.Contains("No default email configuration"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task SendEmailNotificationAsync_ForwardEmailListIsNullOrEmpty_NoEmailsSent(string? emailList)
        {
            // Arrange
            var emailConfig = new EmailConfiguration
            {
                Id = 3,
                Name = "Default",
                SmtpHost = "smtp.test.com",
                SmtpPort = 25,
                Username = "user",
                Password = "pass",
                SenderEmail = "testing@test.com",
                SenderName = "testing@test.com",
                UseSsl = true,
                IsActive = true,
                IsDefault = true
            };
            dbContext.EmailConfigurations.Add(emailConfig);

            var observedTz = new ObservedTimeZone
            {
                Id = 3,
                DisplayName = "Test TZ",
                TimeZoneId = "Test/TZ",
                ForwardEmailList = emailList,
                IsActive = true
            };
            dbContext.TimeZones.Add(observedTz);
            await dbContext.SaveChangesAsync();

            var notificationDto = new NotificationReadDTO
            {
                Id = 3,
                TimeZoneId = observedTz.Id,
                Message = "Test message"
            };

            var emailServiceMock = new Mock<IEmailService>();
            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);

            // Act
            var result = await notifier.SendEmailNotificationAsync(notificationDto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Empty(result.Data);
            Assert.Contains(result.ValidatorResponse.MessageList, e => e.Contains("No email list"));
        }

        [Fact]
        public async Task SendEmailNotificationAsync_PartialEmailsSent_ReturnsPartialSuccess()
        {
            // Arrange
            var emailConfig = new EmailConfiguration
            {
                Id = 4,
                Name = "Default",
                SmtpHost = "smtp.test.com",
                SmtpPort = 25,
                Username = "user",
                Password = "pass",
                SenderEmail = "testing@test.com",
                SenderName = "testing@test.com",
                UseSsl = true,
                IsActive = true,
                IsDefault = true
            };
            dbContext.EmailConfigurations.Add(emailConfig);

            var observedTz = new ObservedTimeZone
            {
                Id = 4,
                DisplayName = "Test TZ",
                TimeZoneId = "Test/TZ",
                ForwardEmailList = "a@test.com;b@test.com",
                IsActive = true
            };
            dbContext.TimeZones.Add(observedTz);
            await dbContext.SaveChangesAsync();

            var notificationDto = new NotificationReadDTO
            {
                Id = 4,
                TimeZoneId = observedTz.Id,
                Message = "Test message"
            };

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock.Setup(x => x.ConfigureCredentials(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));
            emailServiceMock.SetupSequence(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((true, "OK"))
                .ReturnsAsync((false, "SMTP error"));

            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);

            // Act
            var result = await notifier.SendEmailNotificationAsync(notificationDto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Equal(2, result.Data.Count);
            Assert.Contains(result.Data, kvp => kvp.Value == false);
            Assert.Contains(result.ValidatorResponse.MessageList, e => e.Contains("Failed to send email"));
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

        #region TESTS FOR GetEmailsToNotifyAsync

        [Fact]
        public async Task GetEmailsToNotifyAsync_NoActiveTimeZones_ReturnsEmptyList()
        {
            // Arrange
            var inactiveTz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ",
                DisplayName = "TEST TZ",
                IsActive = false,
                ForwardEmailList = "test@example.com"
            };
            dbContext.TimeZones.Add(inactiveTz);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_TimeZonesWithNullOrEmptyEmailList_ReturnsEmptyList()
        {
            // Arrange
            var tzWithNull = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ 1",
                DisplayName = "TEST TZ 1",
                IsActive = true,
                ForwardEmailList = null
            };
            var tzWithEmpty = new ObservedTimeZone
            {
                Id = 2,
                TimeZoneId = "TEST TZ 2",
                DisplayName = "TEST TZ 2",
                IsActive = true,
                ForwardEmailList = ""
            };
            var tzWithWhitespace = new ObservedTimeZone
            {
                Id = 3,
                TimeZoneId = "TEST TZ 3",
                DisplayName = "TEST TZ 3",
                IsActive = true,
                ForwardEmailList = "   "
            };
            dbContext.TimeZones.AddRange(tzWithNull, tzWithEmpty, tzWithWhitespace);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_SingleTimeZoneWithSingleEmail_ReturnsOneEmailWithOneTimeZone()
        {
            // Arrange
            var tz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ",
                DisplayName = "TEST TZ",
                IsActive = true,
                ForwardEmailList = "test@example.com"
            };
            dbContext.TimeZones.Add(tz);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);

            var emailDto = result.Data.First();
            Assert.Equal("test@example.com", emailDto.Email);
            Assert.Single(emailDto.ObservedTimeZoneIds);
            Assert.Contains(1, emailDto.ObservedTimeZoneIds);
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_SingleTimeZoneWithMultipleEmails_ReturnsMultipleEmailsWithSameTimeZone()
        {
            // Arrange
            var tz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ",
                DisplayName = "TEST TZ",
                IsActive = true,
                ForwardEmailList = "user1@example.com;user2@example.com;user3@example.com"
            };
            dbContext.TimeZones.Add(tz);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.Count());

            var emails = result.Data.ToList();
            Assert.All(emails, e => Assert.Single(e.ObservedTimeZoneIds));
            Assert.All(emails, e => Assert.Contains(1, e.ObservedTimeZoneIds));
            Assert.Contains(emails, e => e.Email == "user1@example.com");
            Assert.Contains(emails, e => e.Email == "user2@example.com");
            Assert.Contains(emails, e => e.Email == "user3@example.com");
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_MultipleTimeZonesWithSameEmail_ReturnsSingleEmailWithMultipleTimeZones()
        {
            // Arrange
            var tz1 = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ 1",
                DisplayName = "TEST TZ 1",
                IsActive = true,
                ForwardEmailList = "shared@example.com"
            };
            var tz2 = new ObservedTimeZone
            {
                Id = 2,
                TimeZoneId = "TEST TZ 2",
                DisplayName = "TEST TZ 2",
                IsActive = true,
                ForwardEmailList = "shared@example.com"
            };
            var tz3 = new ObservedTimeZone
            {
                Id = 3,
                TimeZoneId = "TEST TZ 3",
                DisplayName = "TEST TZ 3",
                IsActive = true,
                ForwardEmailList = "shared@example.com"
            };
            dbContext.TimeZones.AddRange(tz1, tz2, tz3);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);

            var emailDto = result.Data.First();
            Assert.Equal("shared@example.com", emailDto.Email);
            Assert.Equal(3, emailDto.ObservedTimeZoneIds.Count());
            Assert.Contains(1, emailDto.ObservedTimeZoneIds);
            Assert.Contains(2, emailDto.ObservedTimeZoneIds);
            Assert.Contains(3, emailDto.ObservedTimeZoneIds);
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_ComplexScenario_MultipleTimeZonesWithOverlappingEmails_ReturnsCorrectMapping()
        {
            // Arrange
            var tz1 = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "Pacific Standard Time",
                DisplayName = "Pacific",
                IsActive = true,
                ForwardEmailList = "admin@example.com;user1@example.com"
            };
            var tz2 = new ObservedTimeZone
            {
                Id = 2,
                TimeZoneId = "Eastern Standard Time",
                DisplayName = "Eastern",
                IsActive = true,
                ForwardEmailList = "admin@example.com;user2@example.com"
            };
            var tz3 = new ObservedTimeZone
            {
                Id = 3,
                TimeZoneId = "Central Standard Time",
                DisplayName = "Central",
                IsActive = true,
                ForwardEmailList = "user1@example.com;user2@example.com;user3@example.com"
            };
            dbContext.TimeZones.AddRange(tz1, tz2, tz3);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Equal(4, result.Data.Count());

            var emailList = result.Data.ToList();

            var adminEmail = emailList.First(e => e.Email == "admin@example.com");
            Assert.Equal(2, adminEmail.ObservedTimeZoneIds.Count());
            Assert.Contains(1, adminEmail.ObservedTimeZoneIds);
            Assert.Contains(2, adminEmail.ObservedTimeZoneIds);

            var user1Email = emailList.First(e => e.Email == "user1@example.com");
            Assert.Equal(2, user1Email.ObservedTimeZoneIds.Count());
            Assert.Contains(1, user1Email.ObservedTimeZoneIds);
            Assert.Contains(3, user1Email.ObservedTimeZoneIds);

            var user2Email = emailList.First(e => e.Email == "user2@example.com");
            Assert.Equal(2, user2Email.ObservedTimeZoneIds.Count());
            Assert.Contains(2, user2Email.ObservedTimeZoneIds);
            Assert.Contains(3, user2Email.ObservedTimeZoneIds);

            var user3Email = emailList.First(e => e.Email == "user3@example.com");
            Assert.Single(user3Email.ObservedTimeZoneIds);
            Assert.Contains(3, user3Email.ObservedTimeZoneIds);
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_EmailsWithWhitespace_TrimsCorrectly()
        {
            // Arrange
            var tz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ",
                DisplayName = "TEST TZ",
                IsActive = true,
                ForwardEmailList = " user1@example.com ; user2@example.com "
            };
            dbContext.TimeZones.Add(tz);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);

            var emails = result.Data.Select(e => e.Email).ToList();

            Assert.Contains("user1@example.com", emails);
            Assert.Contains("user2@example.com", emails);
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_DuplicateEmailsInSameTimeZone_ReturnsUniqueEmailOnce()
        {
            // Arrange
            var tz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "TEST TZ",
                DisplayName = "TEST TZ",
                IsActive = true,
                ForwardEmailList = "user@example.com;user@example.com;user@example.com"
            };
            dbContext.TimeZones.Add(tz);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);

            var emailDto = result.Data.First();
            Assert.Equal("user@example.com", emailDto.Email);
            Assert.Single(emailDto.ObservedTimeZoneIds);
            Assert.Contains(1, emailDto.ObservedTimeZoneIds);
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_StressTest_Over1000TimeZonesAndOver200Emails_PerformsCorrectly()
        {
            // Arrange
            const int timeZoneCount = 1200;
            const int uniqueEmailsPerTimeZone = 5;
            const int totalUniqueEmails = 250;

            var random = new Random(42); // Fixed seed for reproducibility
            var timeZones = new List<ObservedTimeZone>();

            // Generate 1200 time zones
            for (int i = 1; i <= timeZoneCount; i++)
            {
                // Create a list of 5 random emails for this timezone
                var emailsForTimeZone = new List<string>();
                for (int j = 0; j < uniqueEmailsPerTimeZone; j++)
                {
                    int emailIndex = random.Next(1, totalUniqueEmails + 1);
                    emailsForTimeZone.Add($"user{emailIndex}@example.com");
                }

                var tz = new ObservedTimeZone
                {
                    Id = i,
                    TimeZoneId = $"TimeZone/Region{i}",
                    DisplayName = $"Time Zone {i}",
                    IsActive = true,
                    ForwardEmailList = string.Join(";", emailsForTimeZone)
                };
                timeZones.Add(tz);
            }

            dbContext.TimeZones.AddRange(timeZones);
            await dbContext.SaveChangesAsync();

            // Act
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();
            stopwatch.Stop();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);

            var emailDtos = result.Data.ToList();

            // Verify unique emails (should be <= 250 unique emails)
            var uniqueEmails = emailDtos.Select(e => e.Email).Distinct().ToList();
            Assert.True(uniqueEmails.Count <= totalUniqueEmails);
            Assert.Equal(emailDtos.Count, uniqueEmails.Count); // No duplicate emails in result

            // Verify each email has associated time zones
            Assert.All(emailDtos, emailDto =>
            {
                Assert.NotNull(emailDto.ObservedTimeZoneIds);
                Assert.NotEmpty(emailDto.ObservedTimeZoneIds);
                Assert.True(emailDto.ObservedTimeZoneIds.Count() <= timeZoneCount);
            });

            // Verify total mappings are reasonable
            var totalMappings = emailDtos.Sum(e => e.ObservedTimeZoneIds.Count());
            Assert.True(totalMappings >= timeZoneCount); // At least as many mappings as time zones

            // Spot check a few emails to ensure correct mapping
            var sampleEmail = emailDtos.First();
            foreach (var tzId in sampleEmail.ObservedTimeZoneIds)
            {
                var tz = timeZones.First(t => t.Id == tzId);
                Assert.Contains(sampleEmail.Email, tz.ForwardEmailList);
            }

            // Log statistics for analysis
            _mockLogger.Object.LogInformation(
                $"Stress test completed: {emailDtos.Count} unique emails, " +
                $"{totalMappings} total mappings, " +
                $"{stopwatch.ElapsedMilliseconds}ms elapsed");
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_MixedActiveAndInactive_ReturnsOnlyActiveTimeZones()
        {
            // Arrange
            var activeTz = new ObservedTimeZone
            {
                Id = 1,
                TimeZoneId = "Active TZ",
                DisplayName = "Active",
                IsActive = true,
                ForwardEmailList = "active@example.com"
            };
            var inactiveTz = new ObservedTimeZone
            {
                Id = 2,
                TimeZoneId = "Inactive TZ",
                DisplayName = "Inactive",
                IsActive = false,
                ForwardEmailList = "inactive@example.com"
            };
            dbContext.TimeZones.AddRange(activeTz, inactiveTz);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);

            var emailDto = result.Data.First();
            Assert.Equal("active@example.com", emailDto.Email);
            Assert.Contains(1, emailDto.ObservedTimeZoneIds);
            Assert.DoesNotContain(2, emailDto.ObservedTimeZoneIds);
        }

        [Fact]
        public async Task GetEmailsToNotifyAsync_EmptyDatabase_ReturnsEmptyList()
        {
            // Arrange
            // No time zones added

            // Act
            var result = await _timeZoneNotifierService.GetEmailsToNotifyAsync();

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
        }

        #endregion

        #region TESTS FOR SendEmailForTimeZoneSummary

        [Fact]
        public async Task SendEmailForTimeZoneSummary_NullInput_ReturnsValidationError()
        {
            // Arrange
            var emailServiceMock = new Mock<IEmailService>();
            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);

            // Act
            var result = await notifier.SendEmailForTimeZoneSummary(null!);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Empty(result.Data);
            Assert.Contains(result.ValidatorResponse.MessageList, e => e.Contains("valid email recipient"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task SendEmailForTimeZoneSummary_EmptyEmail_ReturnsValidationError(string? email)
        {
            // Arrange
            var emailServiceMock = new Mock<IEmailService>();
            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);
            var dto = new EmailTimeZoneNotificationDTO { Email = email!, ObservedTimeZoneIds = new[] { 1 } };

            // Act
            var result = await notifier.SendEmailForTimeZoneSummary(dto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Empty(result.Data);
            Assert.Contains(result.ValidatorResponse.MessageList, e => e.Contains("valid email recipient"));
        }

        [Fact]
        public async Task SendEmailForTimeZoneSummary_EmptyTimeZoneIds_ReturnsValidationError()
        {
            // Arrange
            var emailServiceMock = new Mock<IEmailService>();
            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);
            var dto = new EmailTimeZoneNotificationDTO { Email = "user@example.com", ObservedTimeZoneIds = Array.Empty<int>() };

            // Act
            var result = await notifier.SendEmailForTimeZoneSummary(dto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Empty(result.Data);
            Assert.Contains(result.ValidatorResponse.MessageList, e => e.Contains("time zone ID"));
        }

        [Fact]
        public async Task SendEmailForTimeZoneSummary_DefaultConfigMissing_ReturnsFailure()
        {
            // Arrange
            var emailServiceMock = new Mock<IEmailService>();
            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);
            var dto = new EmailTimeZoneNotificationDTO { Email = "user@example.com", ObservedTimeZoneIds = new[] { 1 } };

            // Act
            var result = await notifier.SendEmailForTimeZoneSummary(dto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Empty(result.Data);
            Assert.Contains(result.ValidatorResponse.MessageList, e => e.Contains("No default email configuration"));
        }

        [Fact]
        public async Task SendEmailForTimeZoneSummary_EmailServiceFails_ReturnsFailureResult()
        {
            // Arrange
            var emailConfig = new EmailConfiguration
            {
                Id = 100,
                Name = "Default",
                SmtpHost = "smtp.test.com",
                SmtpPort = 25,
                Username = "user",
                Password = "pass",
                SenderEmail = "sender@test.com",
                SenderName = "Sender",
                UseSsl = false,
                IsActive = true,
                IsDefault = true
            };
            var tz = new ObservedTimeZone
            {
                Id = 100,
                TimeZoneId = "Test/TZ",
                DisplayName = "Test TZ",
                IsActive = true
            };
            dbContext.EmailConfigurations.Add(emailConfig);
            dbContext.TimeZones.Add(tz);
            await dbContext.SaveChangesAsync();

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock.Setup(x => x.ConfigureCredentials(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));
            emailServiceMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((false, "SMTP error"));

            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);
            var dto = new EmailTimeZoneNotificationDTO { Email = "user@example.com", ObservedTimeZoneIds = new[] { 100 } };

            // Act
            var result = await notifier.SendEmailForTimeZoneSummary(dto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Single(result.Data);
            Assert.False(result.Data["user@example.com"]);
            Assert.Contains(result.ValidatorResponse.MessageList, e => e.Contains("Failed to send time zone summary"));
        }

        [Fact]
        public async Task SendEmailForTimeZoneSummary_SuccessPath_EmailSentAndBodyContainsTimeZoneFields()
        {
            // Arrange
            var emailConfig = new EmailConfiguration
            {
                Id = 101,
                Name = "Default",
                SmtpHost = "smtp.test.com",
                SmtpPort = 25,
                Username = "user",
                Password = "pass",
                SenderEmail = "sender@test.com",
                SenderName = "Sender",
                UseSsl = false,
                IsActive = true,
                IsDefault = true
            };
            var tz = new ObservedTimeZone
            {
                Id = 101,
                TimeZoneId = "Pacific Standard Time",
                DisplayName = "Pacific Time",
                IsActive = true,
                TimeZoneObservesDST = true,
                DSTStarts = new DateTime(2025, 3, 9),
                DSTEnds = new DateTime(2025, 11, 2),
                NextTransitionDate = new DateTime(2025, 3, 9),
                NextNotificationDate = new DateTime(2025, 3, 8),
                NotifyDaysBefore = 1,
                Comments = "Test comment"
            };
            dbContext.EmailConfigurations.Add(emailConfig);
            dbContext.TimeZones.Add(tz);
            await dbContext.SaveChangesAsync();

            string? capturedBody = null;
            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock.Setup(x => x.ConfigureCredentials(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));
            emailServiceMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Callback<string, string, string, string, bool>((_, _, _, body, _) => capturedBody = body)
                .ReturnsAsync((true, "OK"));

            var notifier = new TimeZoneNotifierService(_unitOfWork, _mockLogger.Object, _timeZoneNotificationQueryBuilder, _systemTimeZoneProvider, emailServiceMock.Object);
            var dto = new EmailTimeZoneNotificationDTO { Email = "user@example.com", ObservedTimeZoneIds = new[] { 101 } };

            // Act
            var result = await notifier.SendEmailForTimeZoneSummary(dto);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Single(result.Data);
            Assert.True(result.Data["user@example.com"]);
            Assert.NotNull(capturedBody);
            Assert.Contains("Pacific Time", capturedBody);
            Assert.Contains("Pacific Standard Time", capturedBody);
            Assert.Contains("DST Starts", capturedBody);
            Assert.Contains("DST Ends", capturedBody);
        }

        #endregion

    }
}
