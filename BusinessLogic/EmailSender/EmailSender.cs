using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace BusinessLogic.EmailSender;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    
    public async Task<string> SendVerificationCodeAsync(string email)
    {
        var verificationCode = GenerateRandomCode();
        
        var subject = "Ваш код подтверждения";
        var message = $"Ваш код для подтверждения: {verificationCode}";
        
        await SendEmailAsync(email, subject, message);
        
        return verificationCode;
    }
    
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
        {
            EnableSsl = _emailSettings.EnableSsl,
            Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
            Timeout = _emailSettings.Timeout
        };
        return client.SendMailAsync(
            new MailMessage(from: _emailSettings.Email, to: email, subject, message));
    }
    
    private string GenerateRandomCode()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}
