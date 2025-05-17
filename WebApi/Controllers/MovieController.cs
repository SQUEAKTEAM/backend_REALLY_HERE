using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
[Authorize]  // Проверка аутентификации на уровне контроллера
public class MovieController(ICurrentUserService currentUserService) : ControllerBase
{
    [HttpGet("movies")]
    public async Task<IActionResult> GetMovies()
    {
        var user = await currentUserService.GetCurrentUserAsync();  // Если пользователя нет — сервис выбросит исключение

        return Ok(new
        {
            UserId = user.Id,
            CurrentLevel = user.CurrentLvl,
            Movies = new List<object>
            {
                new { Name = "Зена королева воинов", Duration = TimeSpan.FromHours(2) },
                new { Name = "Самый лучший фильм", Duration = TimeSpan.FromHours(5) }
            }
        });
    }
}
