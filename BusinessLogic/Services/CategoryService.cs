
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services;

internal class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public CategoryService(ICategoryRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var categories = await _repository.GetByUserIdAsync(user.Id, cancellationToken);

        return categories.Select(category => {
            return new CategoryDto
            {
                Id = category.Id,
                Title = category.Title
            };
        });
    }
    public async Task CreateCategoryAsync(string title, CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var category = Category.CreateDefault(user.Id, title);
        Console.WriteLine(category.Id);
        await _repository.CreateAsync(category);
    }
}
