using DSTN.Domain.Entities;

namespace DSTN.Application.DTO
{
    /// <summary>
    /// DTO used when creating a new EmailConfiguration.
    /// </summary>
    public class AddEmailConfigurationDTO
    {
        public string SmtpHost { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int SmtpPort { get; set; }
        public bool UseSsl { get; set; }
        public bool UseStartTls { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public static EmailConfiguration ToEntity(AddEmailConfigurationDTO dto)
        {
            return new EmailConfiguration
            {
                Name = dto.Name,
                SmtpHost = dto.SmtpHost,
                SmtpPort = dto.SmtpPort,
                UseSsl = dto.UseSsl,
                UseStartTls = dto.UseStartTls,
                SenderName = dto.SenderName,
                SenderEmail = dto.SenderEmail,
                Username = dto.Username,
                Password = dto.Password,
                IsActive = dto.IsActive,
                IsDefault = dto.IsDefault
            };
        }
    }
}
