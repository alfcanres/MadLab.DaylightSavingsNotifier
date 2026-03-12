using DSTN.Domain.Interfaces;
using System.Net;
using System.Net.Mail;

namespace DSTN.Infrastructure;

public class SystemNetEmailServiceProvider : IEmailService
{

    private string _smtpServer = "";
    private int _smtpPort = 0;
    private string _smtpUsername = "";
    private string _smtpPassword = "";
    private bool _enableSSL = false;

    public void ConfigureCredentials(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, bool enableSSL)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
        _enableSSL = enableSSL;
    }

    public async Task<(bool success, string message)> SendEmailAsync(string from, string to, string subject, string body, bool isBodyHtml)
    {
        try
        {
            if(
                string.IsNullOrEmpty(_smtpServer) || 
                _smtpPort == 0 || 
                string.IsNullOrEmpty(_smtpUsername) || 
                string.IsNullOrEmpty(_smtpPassword))
            {
                return (false, "SMTP credentials are not configured.");
            }


            using var message = new MailMessage();
            message.From = new MailAddress(from);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isBodyHtml;

            using var smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            smtp.EnableSsl = _enableSSL;

            await smtp.SendMailAsync(message);

            return (true, "Email sent successfully.");
         
        }
        catch (Exception ex)
        {
            return (false, $"Failed to send email: {ex.Message}");
        }


    }
}
