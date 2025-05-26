using BusinessLogic;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/password")]
public class PasswordController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public PasswordController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    [HttpPost("reset")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPasswordDto request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        
        if (user == null)
        {
            return BadRequest("Пользователь с указанным email не найден");
        }

        user.HashPass = _passwordHasher.HashPassword(user, request.NewPassword);
        
        await _userRepository.UpdateAsync(user, cancellationToken);

        return Ok("Пароль успешно изменен");
    }
}
