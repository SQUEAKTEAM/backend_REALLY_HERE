using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthUserDto authDto)
    {
        try
        {
            await _userService.Register(authDto);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Ошибка регистрации",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(AuthUserDto authDto)
    {
        var tokens = await _userService.Login(authDto);
        return Ok(tokens);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenDto>> Refresh(TokenDto tokenDto)
    {
        var tokens = await _userService.RefreshTokens(tokenDto.AccessToken, tokenDto.RefreshToken);
        return Ok(tokens);
    }
}