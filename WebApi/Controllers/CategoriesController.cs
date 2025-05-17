using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class CategoriesController : ControllerBase
{
    /// <summary>
    /// Get user categories
    /// </summary>
    /// <param name="user_id">User ID</param>
    [HttpGet("categories/{user_id}")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public IActionResult GetCategories(int user_id)
    {
        // Implementation
        return Ok();
    }
}
