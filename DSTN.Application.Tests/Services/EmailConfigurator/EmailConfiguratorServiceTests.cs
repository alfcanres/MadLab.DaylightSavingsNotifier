using DSTN.Application.DTO;
using DSTN.Application.Services.EmailConfigurator;
using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;
using DSTN.Infrastructure.Persistence;
using DSTN.Infrastructure.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace DSTN.Application.Tests.Services.EmailConfigurator
{
    public class EmailConfiguratorServiceTests
    {
        private readonly AppDbContext _dbContext;
        private readonly IRepository<EmailConfiguration> _repository;
        private readonly IQueryBuilder<EmailConfiguration> _queryBuilder;
        private readonly Mock<ILogger<EmailConfiguratorService>> _mockLogger;
        private readonly EmailConfiguratorService _service;

        public EmailConfiguratorServiceTests()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(dbOptions);
            _repository = new Repository<EmailConfiguration>(_dbContext);
            _queryBuilder = new QueryBuilder<EmailConfiguration>(_dbContext);
            _mockLogger = new Mock<ILogger<EmailConfiguratorService>>();

            _service = new EmailConfiguratorService(
                _repository,
                _mockLogger.Object,
                _queryBuilder
            );
        }

        [Fact]
        public async Task ListEmailConfigurationAsync_NoFilters_ReturnsAllRecords()
        {
            // Arrange
            _dbContext.EmailConfigurations.AddRange(
                new EmailConfiguration { Name = "Config1", SmtpHost = "smtp1.test.com", SmtpPort = 25, Username = "user1", Password = "pass1", SenderEmail = "s1@test.com", SenderName = "Sender1", IsActive = true, IsDefault = false },
                new EmailConfiguration { Name = "Config2", SmtpHost = "smtp2.test.com", SmtpPort = 587, Username = "user2", Password = "pass2", SenderEmail = "s2@test.com", SenderName = "Sender2", IsActive = false, IsDefault = true }
            );
            await _dbContext.SaveChangesAsync();

            var listParams = new EmailConfigurationListParamsDTO
            {
                CurrentPage = 1,
                RecordsPerPage = 10
            };

            // Act
            var result = await _service.ListEmailConfigurationAsync(listParams);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Equal(2, result.Data.RecordCount);
            Assert.Equal(2, result.Data.List.Count());
        }

        [Fact]
        public async Task ListEmailConfigurationAsync_FilterByName_ReturnsMatchingRecords()
        {
            // Arrange
            _dbContext.EmailConfigurations.AddRange(
                new EmailConfiguration { Name = "Production SMTP", SmtpHost = "smtp1.test.com", SmtpPort = 25, Username = "user1", Password = "pass1", SenderEmail = "s1@test.com", SenderName = "Sender1", IsActive = true, IsDefault = false },
                new EmailConfiguration { Name = "Development SMTP", SmtpHost = "smtp2.test.com", SmtpPort = 587, Username = "user2", Password = "pass2", SenderEmail = "s2@test.com", SenderName = "Sender2", IsActive = true, IsDefault = true },
                new EmailConfiguration { Name = "Staging Config", SmtpHost = "smtp3.test.com", SmtpPort = 465, Username = "user3", Password = "pass3", SenderEmail = "s3@test.com", SenderName = "Sender3", IsActive = false, IsDefault = false }
            );
            await _dbContext.SaveChangesAsync();

            var listParams = new EmailConfigurationListParamsDTO
            {
                Name = "SMTP",
                CurrentPage = 1,
                RecordsPerPage = 10
            };

            // Act
            var result = await _service.ListEmailConfigurationAsync(listParams);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Equal(2, result.Data.RecordCount);
            Assert.All(result.Data.List, dto => Assert.Contains("SMTP", dto.Name));
        }

        [Fact]
        public async Task ListEmailConfigurationAsync_FilterByIsActive_ReturnsActiveRecords()
        {
            // Arrange
            _dbContext.EmailConfigurations.AddRange(
                new EmailConfiguration { Name = "Active1", SmtpHost = "smtp1.test.com", SmtpPort = 25, Username = "user1", Password = "pass1", SenderEmail = "s1@test.com", SenderName = "Sender1", IsActive = true, IsDefault = false },
                new EmailConfiguration { Name = "Inactive1", SmtpHost = "smtp2.test.com", SmtpPort = 587, Username = "user2", Password = "pass2", SenderEmail = "s2@test.com", SenderName = "Sender2", IsActive = false, IsDefault = true },
                new EmailConfiguration { Name = "Active2", SmtpHost = "smtp3.test.com", SmtpPort = 465, Username = "user3", Password = "pass3", SenderEmail = "s3@test.com", SenderName = "Sender3", IsActive = true, IsDefault = false }
            );
            await _dbContext.SaveChangesAsync();

            var listParams = new EmailConfigurationListParamsDTO
            {
                IsActive = true,
                CurrentPage = 1,
                RecordsPerPage = 10
            };

            // Act
            var result = await _service.ListEmailConfigurationAsync(listParams);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Equal(2, result.Data.RecordCount);
            Assert.All(result.Data.List, dto => Assert.True(dto.IsActive));
        }

        [Fact]
        public async Task ListEmailConfigurationAsync_WithPaging_ReturnsCorrectPage()
        {
            // Arrange
            for (int i = 1; i <= 5; i++)
            {
                _dbContext.EmailConfigurations.Add(new EmailConfiguration
                {
                    Name = $"Config{i}",
                    SmtpHost = $"smtp{i}.test.com",
                    SmtpPort = 25,
                    Username = $"user{i}",
                    Password = $"pass{i}",
                    SenderEmail = $"s{i}@test.com",
                    SenderName = $"Sender{i}",
                    IsActive = true,
                    IsDefault = false
                });
            }
            await _dbContext.SaveChangesAsync();

            var listParams = new EmailConfigurationListParamsDTO
            {
                CurrentPage = 1,
                RecordsPerPage = 2
            };

            // Act
            var result = await _service.ListEmailConfigurationAsync(listParams);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Equal(5, result.Data.RecordCount);
            Assert.Equal(2, result.Data.List.Count());
            Assert.Equal(3, result.Data.PageCount);
            Assert.Equal(1, result.Data.CurrentPage);
        }

        [Fact]
        public async Task ListEmailConfigurationAsync_NoMatchingRecords_ReturnsEmptyList()
        {
            // Arrange
            _dbContext.EmailConfigurations.Add(new EmailConfiguration
            {
                Name = "OnlyConfig",
                SmtpHost = "smtp.test.com",
                SmtpPort = 25,
                Username = "user",
                Password = "pass",
                SenderEmail = "s@test.com",
                SenderName = "Sender",
                IsActive = true,
                IsDefault = false
            });
            await _dbContext.SaveChangesAsync();

            var listParams = new EmailConfigurationListParamsDTO
            {
                Name = "NonExistent",
                CurrentPage = 1,
                RecordsPerPage = 10
            };

            // Act
            var result = await _service.ListEmailConfigurationAsync(listParams);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.ValidatorResponse.IsValid);
            Assert.Equal(0, result.Data.RecordCount);
            Assert.Empty(result.Data.List);
        }
    }
}
