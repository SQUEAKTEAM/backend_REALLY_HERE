using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
[Authorize]
public class TasksController(IDayTaskService dayTaskService) : ControllerBase
{

    [HttpDelete("task/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await dayTaskService.DeleteByIdAsync(id);
        return Ok();
    }

    [HttpPut("task/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTask([FromBody] TaskDto task)
    {
        await dayTaskService.UpdateAsync(task);
        return Ok();
    }

    [HttpPost("task/")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto task)
    {
        await dayTaskService.CreateAsync(task);
        return Created();
    }


    [HttpGet("tasks/")]
    [ProducesResponseType(typeof(List<TaskGetDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await dayTaskService.GetTasksForUserByDateAsync(null);

        return Ok(tasks);
    }

    [HttpGet("tasks/{date}")]
    [ProducesResponseType(typeof(List<TaskGetDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTasksByDate(string date)
    {
        if (!DateTime.TryParseExact(date, "yyyy-MM-dd",
             CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            return BadRequest("Invalid date format. Use YYYY-MM-DD.");
        }

        // Приводим дату к UTC (без изменения времени)
        var dateUtc = DateTime.SpecifyKind(parsedDate.Date, DateTimeKind.Utc);
        
        var tasks = await dayTaskService.GetTasksForUserByDateAsync(dateUtc);
        return Ok(tasks);
    }
}
