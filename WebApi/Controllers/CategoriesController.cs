using BusinessLogic;
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
    [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await categoryService.GetCategoriesAsync();
        return Ok(categories);
    }

    [HttpPost("category/")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto category)
    {
        await categoryService.CreateCategoryAsync(category.Title);
        return Ok();
    }
}
