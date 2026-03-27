using DSTN.AdminApp.WinForms.Interfaces;

namespace DSTN.AdminApp.WinForms.Forms.EmailConfigurator;

public interface IEditEmailConfigurator : IEditorForm
{
    int Id { get; set; }
    string ConfigName { get; set; }
    string SmtpHost { get; set; }
    int SmtpPort { get; set; }
    bool UseSsl { get; set; }
    bool UseStartTls { get; set; }
    string SenderName { get; set; }
    string SenderEmail { get; set; }
    string Username { get; set; }
    string Password { get; set; }
    bool IsActive { get; set; }
    bool IsDefault { get; set; }
    void Show();
}
