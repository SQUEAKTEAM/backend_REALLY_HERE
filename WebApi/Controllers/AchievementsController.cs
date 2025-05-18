using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
[Authorize]
public class AchievementsController(IAchievementService achievementService) : ControllerBase
{

    [HttpGet("achievements/")]
    [ProducesResponseType(typeof(List<AchievementDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAchievements()
    {
        var achievements = await achievementService.GetAchievementsAsync();
        return Ok(achievements);
    }
}
