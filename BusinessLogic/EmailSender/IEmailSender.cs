namespace BusinessLogic.EmailSender;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
    Task<string> SendVerificationCodeAsync(string email);
}
