using BusinessLogic;
using BusinessLogic.EmailSender;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class CodeController : Controller
{
    private readonly IEmailSender _emailSender;

    public CodeController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    
    [HttpPost("send-code")]
    public async Task<IActionResult> SendVerificationCode([FromBody] EmailRequestDto request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("Email не может быть пустым");
            }

            var verificationCode = await _emailSender.SendVerificationCodeAsync(request.Email);
            
            return Ok(new 
            {
                Email = request.Email,
                VerificationCode = verificationCode,
                Message = "Код верификации отправлен на email"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Не удалось отправить код верификации: {ex.Message}");
        }
    }
}
