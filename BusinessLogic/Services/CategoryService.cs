
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services;

internal class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILvLService _lvlService;
    private readonly IAchievementRepository _achievementRepository;

    public CategoryService(ICategoryRepository repository, ICurrentUserService currentUserService, ILvLService lvlService, IAchievementRepository achievementRepository)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _lvlService = lvlService;
        _achievementRepository = achievementRepository;
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

        var reward = await _achievementRepository.UpdateProgressAndReturnRewardAsync
       (
           user.Id,
           new[] { "Создать 1 категорию", "Создать 3 категории", "Создать 5 категорий" },
           cancellationToken
       );

        if (reward > 0)
        {
            await _lvlService.AddRewardToLvLAsync(reward, cancellationToken);
        }
    }
}
