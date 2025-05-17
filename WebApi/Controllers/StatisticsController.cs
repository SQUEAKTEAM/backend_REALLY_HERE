using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class StatisticsController : ControllerBase
{
    /// <summary>
    /// Get user statistics
    /// </summary>
    /// <param name="user_id">User ID</param>
    [HttpGet("statistics/{user_id}")]
    [ProducesResponseType(typeof(List<StatisticsDto>), StatusCodes.Status200OK)]
    public IActionResult GetStatistics(int user_id)
    {
        // Implementation
        return Ok();
    }
}
