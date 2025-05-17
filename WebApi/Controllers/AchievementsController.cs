using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class AchievementsController : ControllerBase
{
    /// <summary>
    /// Get user achievements
    /// </summary>
    /// <param name="user_id">User ID</param>
    [HttpGet("achievements/{user_id}")]
    [ProducesResponseType(typeof(List<AchievementDto>), StatusCodes.Status200OK)]
    public IActionResult GetAchievements(int user_id)
    {
        // Implementation
        return Ok();
    }
}
