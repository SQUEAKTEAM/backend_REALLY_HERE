using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] AuthUserDto authDto)
    {
        await userService.Register(authDto);
        return Created();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthUserDto authDto)
    {
        var token = await userService.Login(authDto);
        return Ok(token);
    }
}
