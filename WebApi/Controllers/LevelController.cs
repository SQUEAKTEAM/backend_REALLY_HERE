using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class LevelController : ControllerBase
{
    /// <summary>
    /// Get user level information
    /// </summary>
    /// <param name="user_id">User ID</param>
    [HttpGet("lvl/{user_id}")]
    [ProducesResponseType(typeof(LevelDto), StatusCodes.Status200OK)]
    public IActionResult GetLevel(int user_id)
    {
        // Implementation
        return Ok();
    }

    /// <summary>
    /// Update user level
    /// </summary>
    /// <param name="id">Level ID</param>
    /// <param name="lvl">Level data</param>
    [HttpPut("lvl/{user_id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateLevel(int id, [FromBody] LevelDto lvl)
    {
        // 1. Найти пользователя по id
        // 2. Обновить его данные из levelData
        // 3. Сохранить изменения

        return Ok();
    }
}
