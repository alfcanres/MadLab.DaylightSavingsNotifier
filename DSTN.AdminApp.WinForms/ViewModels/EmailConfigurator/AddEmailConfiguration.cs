namespace DSTN.AdminApp.WinForms.ViewModels.EmailConfigurator;

public record AddEmailConfiguration(
    string Name,
    string SmtpHost,
    int SmtpPort,
    bool UseSsl,
    bool UseStartTls,
    string SenderName,
    string SenderEmail,
    string Username,
    string Password,
    bool IsActive,
    bool IsDefault
);
