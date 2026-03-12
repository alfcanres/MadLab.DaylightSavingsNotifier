namespace DSTN.Domain.Interfaces;
public interface IEmailService
{
    void ConfigureCredentials(string smtpServer, int port, string username, string password, bool enableSSL);
    Task<(bool success, string message)> SendEmailAsync(string from, string to, string subject, string body,bool isBodyHtml);
}
