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
using System.Text;


namespace DSTN.Application.Tests
{
    public class TimeZoneConfiguratorTests
    {
        private readonly TimeZoneConfiguratorService timeZoneConfiguratorService;
        private readonly IRepository<ObservedTimeZone> repository;
        private readonly Mock<ILogger<TimeZoneConfiguratorService>> mockLogger;
        private readonly IQueryBuilder<ObservedTimeZone> queryBuilder;
        private readonly ISystemTimeZoneProvider systemTimeZoneProvider;
        private readonly AppDbContext dbContext;

        public TimeZoneConfiguratorTests()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new AppDbContext(dbOptions);



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

        #region TESTING TIME ZONE CONFIGS

        [Fact]
        public async Task AddTimeZoneToObserveAsync_ShouldCreateCentralAmericaStandardTimeNoDST()
        {
            // Arrange
            var testTimeZone = TestData.GetCentralAmericaStandardTimeNoDST();
            var addDto = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = testTimeZone.TimeZoneId,
                DisplayName = testTimeZone.TimeZoneId,
                CreatedAt = new DateTime(2025, 1, 1),
            };

            // Act
            var result = await timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Result);  

            Assert.Equal(testTimeZone.ObservesDST, result.Result.TimeZoneObservesDST);
            Assert.Equal(testTimeZone.DSTEnds, result.Result.DSTEnds);
            Assert.Equal(testTimeZone.DSTStarts, result.Result.DSTStarts);
        }

        public static IEnumerable<object[]> GetTimeZoneTestData()
        {
            foreach (var tz in TestData.GetDstObservingTimeZoneIds())
            {
                yield return new object[] { tz };
            }
        }

        [Theory]
        [MemberData(nameof(GetTimeZoneTestData))]
        public async Task AddTimeZoneToObserveAsync_ShouldHandleVariousTimeZones(TestingTimezones testTimeZone)
        {
            // Arrange
            var addDto = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = testTimeZone.TimeZoneId,
                DisplayName = $"Test {testTimeZone.TimeZoneId}",
                CreatedAt = new DateTime(2025, 1, 1),
            };

            // Act
            var result = await timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            // Assert
            Assert.NotNull(result.Result);
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Equal(addDto.TimeZoneId, result.Result.TimeZoneId);
            Assert.Equal(testTimeZone.ObservesDST, result.Result.TimeZoneObservesDST);
            
            if (testTimeZone.ObservesDST)
            {
                Assert.NotNull(result.Result.DSTStarts);
                Assert.NotNull(result.Result.DSTEnds);
            }
            else
            {
                Assert.Null(result.Result.DSTStarts);
                Assert.Null(result.Result.DSTEnds);
            }
        }


        [Fact]
        public async Task AddTimeZoneToObserveAsync_ShouldCreatePacificStandardTime_WithNexTransitionDateAsStartDate()
        {
            // Arrange
            var testTimeZone = TestData.GetPacificStandardTimeWithDST();
            var addDto = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = testTimeZone.TimeZoneId,
                DisplayName = testTimeZone.TimeZoneId,
                CreatedAt = new DateTime(2025, 1, 1),
            };

            var expectedNextTransitionDate = new DateTime(2025, 3, 9);

            // Act
            var result = await timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Result);
            Assert.Equal(expectedNextTransitionDate, result.Result.DSTStarts);
            Assert.Equal(expectedNextTransitionDate, result.Result.NextTransitionDate);

        }


        [Fact]
        public async Task AddTimeZoneToObserveAsync_ShouldCreatePacificStandardTime_WithNexTransitionDateAsEndDate()
        {
            // Arrange
            var testTimeZone = TestData.GetPacificStandardTimeWithDST();
            var addDto = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = testTimeZone.TimeZoneId,
                DisplayName = testTimeZone.TimeZoneId,
                CreatedAt = new DateTime(2025, 6, 1),
            };

            var expectedNextTransitionDate = new DateTime(2025, 11, 2);

            // Act
            var result = await timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Result);
            Assert.Equal(expectedNextTransitionDate, result.Result.DSTEnds);
            Assert.Equal(expectedNextTransitionDate, result.Result.NextTransitionDate);

        }

        #endregion

        #region TESTING CRUD
        [Fact]
        public async Task AddTimeZoneToObserveAsync_ShouldAddTimeZone()
        {
            // Arrange

            var testTimeZone = TestData.GetCentralAmericaStandardTimeNoDST();

            var addDto = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = testTimeZone.TimeZoneId,
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

        [Fact]
        public async Task AddTimeZoneToObserveAsync_ShouldAFailToddTimeZone()
        {
            // Arrange
            string expectedErrorMessageFortimezoneId = "TimeZoneId is not valid.";
            string expectedErrorMessageForDisplayName = "Display Name is required.";
            var addDto = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = "Fake time zone",
                DisplayName = "",
                CreatedAt = DateTime.UtcNow,
            };
            // Act
            var result = await timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);
            // Assert
            Assert.NotNull(result);
            Assert.False(result.ValidatorResponse.IsValid);

            Assert.Contains(expectedErrorMessageFortimezoneId, result.ValidatorResponse.MessageList);
            Assert.Contains(expectedErrorMessageForDisplayName, result.ValidatorResponse.MessageList);
        }

        [Fact]
        public async Task GetById_ShouldReturn_TimeZone()
        {

            // Arrange
            var timezone = TestData.GetPacificStandardTimeWithDST();
            var addDto = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = timezone.TimeZoneId,
                DisplayName = timezone.TimeZoneId,
                CreatedAt = DateTime.UtcNow,
            };

            var addResult = await timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            //Act
            var getResult = await timeZoneConfiguratorService.GetByTimeZoneToObserveIdAsync(addResult.Result.Id);

            //Assert
            Assert.Equal(addResult.Result.Id, getResult.Result.Id);

        }

        [Fact]
        public async Task GetById_ShouldReturnNull_ForInexistentTimeZone()
        {
            //Act
            var getResult = await timeZoneConfiguratorService.GetByTimeZoneToObserveIdAsync(999);
            //Assert
            Assert.Null(getResult.Result);
            Assert.False(getResult.ValidatorResponse.IsValid);
            Assert.Contains("Item was not found.", getResult.ValidatorResponse.MessageList);
        }

        [Fact]
        public async Task DeleteZoneToObserveAsync_ShouldDelete_WhenIdExists()
        {
            // Arrange
            var testTimeZone = TestData.GetCentralBrazilianStandardTimeNoDST();
            var entity = new ObservedTimeZone
            {
                TimeZoneId = testTimeZone.TimeZoneId,
                DisplayName = testTimeZone.TimeZoneId,
                CreatedAt = DateTime.UtcNow
            };
            await repository.InsertAsync(entity);
            int insertedId = entity.Id;

            // Act
            var deletedResult = await timeZoneConfiguratorService.DeleteZoneToObserveAsync(insertedId);

            var findResult = await timeZoneConfiguratorService.GetByTimeZoneToObserveIdAsync(insertedId);

            // Assert
            Assert.NotNull(deletedResult);
            Assert.True(deletedResult.ValidatorResponse.IsValid);
            Assert.Contains("Item was not found.", findResult.ValidatorResponse.MessageList);
        }

        [Fact]
        public async Task DeleteZoneToObserveAsync_ShouldReturnError_WhenIdDoesNotExist()
        {
            // Arrange
            int nonExistentId = 9999;

            // Act
            var result = await timeZoneConfiguratorService.DeleteZoneToObserveAsync(nonExistentId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Contains("TimeZone does not exist.", result.ValidatorResponse.MessageList);
        }

        #endregion 

        #region TESTING PAGING AND FILTERING

        private async Task SeedTimeZonesAsync()
        {
            foreach (var timezone in TestData.GetDstObservingTimeZoneIds())
            {
                var addDto = new AddTimeZoneToObserveDTO
                {
                    TimeZoneId = timezone.TimeZoneId,
                    DisplayName = timezone.TimeZoneId,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };


                await timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            }
        }

        [Fact]
        public async Task ListObservedTimeZones_ShouldReturnCorrectPageCount()
        {
            // Arrange

            await SeedTimeZonesAsync();

            var pager = new ObservedTimeZoneForListParamsDTO
            {
                CurrentPage = 1,
                RecordsPerPage = 2
            };

            // Act
            var result = await timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Result);
            Assert.Equal(3, result.Result.PageCount); // 5 records, 2 per page => 3 pages
            Assert.Equal(5, result.Result.RecordCount);
            Assert.Equal(2, result.Result.List.Count());
        }

        [Fact]
        public async Task ListObservedTimeZones_ShouldReturnFilteredByDisplayName()
        {
            // Arrange
            await SeedTimeZonesAsync();
            var pager = new ObservedTimeZoneForListParamsDTO
            {
                CurrentPage = 1,
                RecordsPerPage = 10,
                DisplayName = "Pacific"
            };

            // Act
            var result = await timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Result);
            bool found = result.Result.List.Where(t => t.DisplayName.Contains(pager.DisplayName)).Any();
            Assert.True(found);
        }

        [Fact]
        public async Task ListObservedTimeZones_ShouldReturnFilteredByIsActive()
        {
            // Arrange
            await SeedTimeZonesAsync();
            var pager = new ObservedTimeZoneForListParamsDTO
            {
                CurrentPage = 1,
                RecordsPerPage = 10,
                IsActive = true
            };

            // Act
            var result = await timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Result);
            Assert.Equal(5, result.Result.List.Count());
            Assert.All(result.Result.List, tz => Assert.True(tz.IsActive));
        }

        [Fact]
        public async Task ListObservedTimeZones_ShouldReturnFilteredByDisplayNameAndIsActive()
        {
            // Arrange
            await SeedTimeZonesAsync();
            var pager = new ObservedTimeZoneForListParamsDTO
            {
                CurrentPage = 1,
                RecordsPerPage = 10,
                DisplayName = "Central",
                IsActive = true
            };

            // Act
            var result = await timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Result);
            bool found = result.Result.List.Where(t => t.DisplayName.Contains(pager.DisplayName)).Any();
            Assert.True(found);
        }

        [Fact]
        public async Task ListObservedTimeZones_ShouldNotReturnFilteredByDisplayNameThatDoesNotExists()
        {
            // Arrange
            await SeedTimeZonesAsync();
            var pager = new ObservedTimeZoneForListParamsDTO
            {
                CurrentPage = 1,
                RecordsPerPage = 10,
                DisplayName = "NOT FOUND",
                IsActive = false
            };

            // Act
            var result = await timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Result);
            Assert.Equal(0, result.Result.PageCount);
            Assert.Equal(0, result.Result.RecordCount);

        }

        #endregion

    }
}
