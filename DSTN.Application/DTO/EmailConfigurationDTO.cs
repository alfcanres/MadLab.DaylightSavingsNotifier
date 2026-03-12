using DSTN.Domain.Entities;

namespace DSTN.Application.DTO
{
    /// <summary>
    /// Read DTO for EmailConfiguration. Password is omitted for security.
    /// </summary>
    public class EmailConfigurationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool UseSsl { get; set; }
        public bool UseStartTls { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static EmailConfigurationDTO FromEntity(EmailConfiguration entity)
        {
            return new EmailConfigurationDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                SmtpHost = entity.SmtpHost,
                SmtpPort = entity.SmtpPort,
                UseSsl = entity.UseSsl,
                UseStartTls = entity.UseStartTls,
                SenderName = entity.SenderName,
                SenderEmail = entity.SenderEmail,
                Username = entity.Username,
                IsActive = entity.IsActive,
                IsDefault = entity.IsDefault,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
