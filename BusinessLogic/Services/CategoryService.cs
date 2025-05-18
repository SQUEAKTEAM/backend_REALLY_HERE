
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

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
    public async Task<IEnumerable<string>> GetCategoriesTitleAsync(CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var categories = await _repository.GetByUserIdAsync(user.Id, cancellationToken);

        return categories.Select(category => {
            return category.Title;
        });
    }
}
