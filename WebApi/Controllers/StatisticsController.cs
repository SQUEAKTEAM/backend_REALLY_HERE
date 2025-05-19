using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
[Authorize]
public class StatisticsController(IStatisticsService statisticsService) : ControllerBase
{
    [HttpGet("statistics/")]
    [ProducesResponseType(typeof(List<StatisticsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatistics()
    {
        var stats = await statisticsService.GetStatisticsAsync();
        return Ok(stats);
    }

    [HttpGet("statistics/daily/")]
    [ProducesResponseType(typeof(List<DailyStatisticsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDailyStatistics()
    {
        var stats = await statisticsService.GetDailyStatisticsAsync();
        return Ok(stats);
    }
}
