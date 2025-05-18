using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
[Authorize]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{

    [HttpGet("categories/")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories()
    {
        var titles = await categoryService.GetCategoriesTitleAsync();
        return Ok(titles);
    }
}
