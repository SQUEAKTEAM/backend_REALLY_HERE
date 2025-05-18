using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
[Authorize]
public class LevelController(ILvLService lvLService) : ControllerBase
{

    [HttpGet("lvl/")]
    [ProducesResponseType(typeof(LevelDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLevel()
    {
        var lvl = await lvLService.GetLvLAsync();
        return Ok(lvl);
    }

    [HttpPut("lvl/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateLevel([FromBody] LevelDto lvl)
    {
        await lvLService.UpdateLvLAsync(lvl);
        return Ok();
    }
}
