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
        private readonly TimeZoneConfiguratorService _timeZoneConfiguratorService;
        private readonly IRepository<ObservedTimeZone> _repository;
        private readonly Mock<ILogger<TimeZoneConfiguratorService>> _mockLogger;
        private readonly IQueryBuilder<ObservedTimeZone> _queryBuilder;
        private readonly ISystemTimeZoneProvider _systemTimeZoneProvider;
        private readonly AppDbContext dbContext;

        public TimeZoneConfiguratorTests()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new AppDbContext(dbOptions);



            _repository = new Repository<ObservedTimeZone>(dbContext);
            _queryBuilder = new QueryBuilder<ObservedTimeZone>(dbContext);
            _systemTimeZoneProvider = new SystemTimeZoneProvider();
            _mockLogger = new Mock<ILogger<TimeZoneConfiguratorService>>();


            _timeZoneConfiguratorService = new TimeZoneConfiguratorService(
                _repository,
                _mockLogger.Object,
                _queryBuilder,
                _systemTimeZoneProvider
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
            var result = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);  

            Assert.Equal(testTimeZone.ObservesDST, result.Data.TimeZoneObservesDST);
            Assert.Equal(testTimeZone.DSTEnds, result.Data.DSTEnds);
            Assert.Equal(testTimeZone.DSTStarts, result.Data.DSTStarts);
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
            var result = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Equal(addDto.TimeZoneId, result.Data.TimeZoneId);
            Assert.Equal(testTimeZone.ObservesDST, result.Data.TimeZoneObservesDST);
            
            if (testTimeZone.ObservesDST)
            {
                Assert.NotNull(result.Data.DSTStarts);
                Assert.NotNull(result.Data.DSTEnds);
            }
            else
            {
                Assert.Null(result.Data.DSTStarts);
                Assert.Null(result.Data.DSTEnds);
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

            var expectedNextTransitionDate = testTimeZone.DSTStarts;

            // Act
            var result = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedNextTransitionDate, result.Data.DSTStarts);
            Assert.Equal(expectedNextTransitionDate, result.Data.NextTransitionDate);

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
            var result = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedNextTransitionDate, result.Data.DSTEnds);
            Assert.Equal(expectedNextTransitionDate, result.Data.NextTransitionDate);

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
            var result = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);
            // Assert
            Assert.NotNull(result);
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Equal(addDto.TimeZoneId, result.Data.TimeZoneId);
            Assert.Equal(addDto.DisplayName, result.Data.DisplayName);
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
            var result = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);
            // Assert
            Assert.NotNull(result);
            Assert.False(result.ValidatorResponse.IsValid);

            Assert.Contains(expectedErrorMessageFortimezoneId, result.ValidatorResponse.MessageList);
            Assert.Contains(expectedErrorMessageForDisplayName, result.ValidatorResponse.MessageList);
        }

        [Fact]
        public async Task EditTimeZoneToObserveAsync_ValidUpdate_UpdatesEntity()
        {
            // Arrange
            var testTimeZone = TestData.GetCentralAmericaStandardTimeNoDST();
            var addDto = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = testTimeZone.TimeZoneId,
                DisplayName = "Original Name",
                CreatedAt = new DateTime(2025, 1, 1),
            };
            var addResult = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            var editDto = new EditTimeZoneToObserveDTO
            {
                Id = addResult.Data.Id,
                TimeZoneId = testTimeZone.TimeZoneId,
                DisplayName = "Updated Name",
                LastChanged = new DateTime(2025, 2, 1),
                IsActive = true,
                Color = "#FF0000",
                Comments = "Updated comments"
            };

            // Act
            var result = await _timeZoneConfiguratorService.EditTimeZoneToObserveAsync(editDto);

            // Assert
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.NotNull(result.Data);
            Assert.Equal(editDto.DisplayName, result.Data.DisplayName);
            Assert.Equal(editDto.LastChanged, result.Data.LastChanged);
        }

        [Fact]
        public async Task EditTimeZoneToObserveAsync_InvalidDTO_ReturnsValidationError()
        {
            // Arrange
            var editDto = new EditTimeZoneToObserveDTO
            {
                Id = 1,
                TimeZoneId = "",
                DisplayName = "",
                LastChanged = DateTime.UtcNow
            };

            // Act
            var result = await _timeZoneConfiguratorService.EditTimeZoneToObserveAsync(editDto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Contains("TimeZoneId is required.", result.ValidatorResponse.MessageList);
            Assert.Contains("DisplayName is required.", result.ValidatorResponse.MessageList);
        }

        [Fact]
        public async Task EditTimeZoneToObserveAsync_NonExistentId_ReturnsValidationError()
        {
            // Arrange
            var editDto = new EditTimeZoneToObserveDTO
            {
                Id = 9999,
                TimeZoneId = "Central America Standard Time",
                DisplayName = "Nonexistent",
                LastChanged = DateTime.UtcNow
            };

            // Act
            var result = await _timeZoneConfiguratorService.EditTimeZoneToObserveAsync(editDto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Contains("TimeZone does not exist.", result.ValidatorResponse.MessageList);
        }

        [Fact]
        public async Task EditTimeZoneToObserveAsync_DuplicateTimeZoneIdOrDisplayName_ReturnsValidationError()
        {
            // Arrange
            var testTimeZone1 = TestData.GetCentralAmericaStandardTimeNoDST();
            var testTimeZone2 = TestData.GetPacificStandardTimeWithDST();

            var addDto1 = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = testTimeZone1.TimeZoneId,
                DisplayName = "Name1",
                CreatedAt = DateTime.UtcNow
            };
            var addDto2 = new AddTimeZoneToObserveDTO
            {
                TimeZoneId = testTimeZone2.TimeZoneId,
                DisplayName = "Name2",
                CreatedAt = DateTime.UtcNow
            };

            var addResult1 = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto1);
            var addResult2 = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto2);

            var editDto = new EditTimeZoneToObserveDTO
            {
                Id = addResult2.Data.Id,
                TimeZoneId = testTimeZone1.TimeZoneId, // Duplicate TimeZoneId
                DisplayName = "Name1", // Duplicate DisplayName
                LastChanged = DateTime.UtcNow
            };

            // Act
            var result = await _timeZoneConfiguratorService.EditTimeZoneToObserveAsync(editDto);

            // Assert
            Assert.False(result.ValidatorResponse.IsValid);
            Assert.Contains("TimeZone already exists.", result.ValidatorResponse.MessageList);
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

            var addResult = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

            //Act
            var getResult = await _timeZoneConfiguratorService.GetByTimeZoneToObserveIdAsync(addResult.Data.Id);

            //Assert
            Assert.Equal(addResult.Data.Id, getResult.Data.Id);

        }

        [Fact]
        public async Task GetById_ShouldReturnNull_ForInexistentTimeZone()
        {
            //Act
            var getResult = await _timeZoneConfiguratorService.GetByTimeZoneToObserveIdAsync(999);
            //Assert
            Assert.Null(getResult.Data);
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
            await _repository.InsertAsync(entity);
            int insertedId = entity.Id;

            // Act
            var deletedResult = await _timeZoneConfiguratorService.DeleteZoneToObserveAsync(insertedId);

            var findResult = await _timeZoneConfiguratorService.GetByTimeZoneToObserveIdAsync(insertedId);

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
            var result = await _timeZoneConfiguratorService.DeleteZoneToObserveAsync(nonExistentId);

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


                await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addDto);

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
            var result = await _timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.PageCount); // 5 records, 2 per page => 3 pages
            Assert.Equal(5, result.Data.RecordCount);
            Assert.Equal(2, result.Data.List.Count());
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
            var result = await _timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Data);
            bool found = result.Data.List.Where(t => t.DisplayName.Contains(pager.DisplayName)).Any();
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
            var result = await _timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(5, result.Data.List.Count());
            Assert.All(result.Data.List, tz => Assert.True(tz.IsActive));
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
            var result = await _timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Data);
            bool found = result.Data.List.Where(t => t.DisplayName.Contains(pager.DisplayName)).Any();
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
            var result = await _timeZoneConfiguratorService.ListObservedTimeZones(pager);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(0, result.Data.PageCount);
            Assert.Equal(0, result.Data.RecordCount);

        }

        #endregion

    }
}
