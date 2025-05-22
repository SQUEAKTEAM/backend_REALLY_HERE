using System.Net;
using System.Net.Mail;

namespace BusinessLogic.EmailSender;

public class EmailSender : IEmailSender
{
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
        var mail = "squake_team@outlook.com";
        var pw = "GrishaMishaDima";

        var client = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, pw)
        };

        return client.SendMailAsync(
            new MailMessage(from: mail, to: email, subject, message));
    }
    
    private string GenerateRandomCode()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}