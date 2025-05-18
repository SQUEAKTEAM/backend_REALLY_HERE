using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;
internal class StatisticsService: IStatisticsService
{
    private readonly ICategoryRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public StatisticsService(ICategoryRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }
    public async Task<IEnumerable<StatisticsDto>> GetStatisticsAsync(CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var categories = await _repository.GetByUserIdAsync(user.Id, cancellationToken);

        return categories.Select(category => {
            return new StatisticsDto
            {
                Id = category.Id,
                CountSuccess = category.CountSuccess,
                CountMiddle = category.CountMiddle,
                CountFailure = category.CountFailed,
                Title = category.Title
            };
        });
    }
}

