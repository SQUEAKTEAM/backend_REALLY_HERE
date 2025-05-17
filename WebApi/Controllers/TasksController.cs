using System.Globalization;
using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
[Authorize]
public class TasksController(IDayTaskService dayTaskService, ICurrentUserService currentUserService) : ControllerBase
{

    [HttpDelete("task/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var user = await currentUserService.GetCurrentUserAsync();
        await dayTaskService.DeleteByIdAsync(id, user.Id);
        return Ok();
    }

    [HttpPut("task/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTask([FromBody] TaskDto task)
    {
        await dayTaskService.UpdateAsync(task);
        return Ok();
    }

    [HttpPost("task")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto task)
    {
        await dayTaskService.CreateAsync(task);
        return Created();
    }


    [HttpGet("tasks/{user_id}")]
    [ProducesResponseType(typeof(List<TaskDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTasks(int userId)
    {
        var tasks = await dayTaskService.GetTasksForUserByDateAsync(userId, null);

        return Ok(tasks);
    }

    [HttpGet("tasks/{user_id}/{date}")]
    [ProducesResponseType(typeof(List<TaskDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTasksByDate(int userId, string date)
    {

        if (!DateTime.TryParseExact(date, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            return BadRequest("Invalid date format. Please use YYYY-MM-DD format.");
        }

        var tasks = await dayTaskService.GetTasksForUserByDateAsync(userId, parsedDate);

        return Ok(tasks);
    }
}
