using DSTN.Domain.Entities;

namespace DSTN.Application.DTO
{
    /// <summary>
    /// DTO used when updating an existing EmailConfiguration.
    /// </summary>
    public class EditEmailConfigurationDTO
    {
        public int Id { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool UseSsl { get; set; }
        public bool UseStartTls { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public static void ToEntity(EditEmailConfigurationDTO dto, EmailConfiguration entity)
        {
            entity.SmtpHost = dto.SmtpHost;
            entity.SmtpPort = dto.SmtpPort;
            entity.UseSsl = dto.UseSsl;
            entity.UseStartTls = dto.UseStartTls;
            entity.SenderName = dto.SenderName;
            entity.SenderEmail = dto.SenderEmail;
            entity.Username = dto.Username;
            entity.Password = dto.Password;
            entity.IsActive = dto.IsActive;
        }

        public static EditEmailConfigurationDTO FromEntity(EmailConfiguration entity)
        {
            return new EditEmailConfigurationDTO
            {
                Id = entity.Id,
                SmtpHost = entity.SmtpHost,
                SmtpPort = entity.SmtpPort,
                UseSsl = entity.UseSsl,
                UseStartTls = entity.UseStartTls,
                SenderName = entity.SenderName,
                SenderEmail = entity.SenderEmail,
                Username = entity.Username,
                Password = entity.Password,
                IsActive = entity.IsActive
            };
        }
    }
}
