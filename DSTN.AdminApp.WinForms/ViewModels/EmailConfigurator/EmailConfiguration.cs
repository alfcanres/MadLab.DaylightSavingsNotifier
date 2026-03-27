namespace DSTN.AdminApp.WinForms.ViewModels.EmailConfigurator;

public record EmailConfiguration(
    int Id,
    string Name,
    string SmtpHost,
    int SmtpPort,
    bool UseSsl,
    bool UseStartTls,
    string SenderName,
    string SenderEmail,
    string Username,
    bool IsActive,
    bool IsDefault,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
