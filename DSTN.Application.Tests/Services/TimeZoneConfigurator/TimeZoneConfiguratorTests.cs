using DSTN.Application.DTO;
using DSTN.Application.Services.TimeZoneConfigurator;
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
    public class TimeZoneConfiguratorTests
    {
        private readonly TimeZoneConfiguratorService timeZoneConfiguratorService;
        private readonly IRepository<ObservedTimeZone> repository;
        private readonly Mock<ILogger<TimeZoneConfiguratorService>> mockLogger;
        private readonly IQueryBuilder<ObservedTimeZone> queryBuilder;
        private readonly ISystemTimeZoneProvider systemTimeZoneProvider;

        public TimeZoneConfiguratorTests()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var dbContext = new AppDbContext(dbOptions);

            repository = new Repository<ObservedTimeZone>(dbContext);
            queryBuilder = new QueryBuilder<ObservedTimeZone>(dbContext);
            systemTimeZoneProvider = new SystemTimeZoneProvider();
            mockLogger = new Mock<ILogger<TimeZoneConfiguratorService>>();


            timeZoneConfiguratorService = new TimeZoneConfiguratorService(
                repository,
                mockLogger.Object,
                queryBuilder,
                systemTimeZoneProvider
            );
        }

        [Fact]
        public async Task AddTimeZoneToObserveAsync_ShouldAddTimeZone()
        {
            // Arrange
            var addDto = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = "Pacific Standard Time",
                DisplayName = "Test Description",
                CreatedAt = DateTime.UtcNow,    
            };
            // Act
            var result = await timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);
            // Assert
            Assert.NotNull(result);
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Result);
            Assert.Equal(addDto.TimeZoneId, result.Result.TimeZoneId);
            Assert.Equal(addDto.DisplayName, result.Result.DisplayName);
        }

    }
}
