using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;
internal class StatisticsService: IStatisticsService
{
    private readonly ICategoryRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDayTaskRepository _taskRepository;
    private readonly ILvLService _lvlService;
    private readonly IAchievementRepository _achievementRepository;

    public StatisticsService(ICategoryRepository repository, ICurrentUserService currentUserService, IDayTaskRepository taskRepository, ILvLService lvLService, IAchievementRepository achievementRepository)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _taskRepository = taskRepository;
        _lvlService = lvLService;
        _achievementRepository = achievementRepository;
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

    public async Task<DailyStatisticsDto> GetDailyStatisticsAsync(CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();

        // Используем вчерашнюю дату для проверки выполнения задач
        var dateToCheckUtc = DateTime.UtcNow.Date.AddDays(-1);

        var tasks = (await _taskRepository.GetForUserByDateAsync(user.Id, dateToCheckUtc, cancellationToken))
            .Where(task => !task.IsArchived)
            .ToList();

        var stat = _repository.GetDailyStatsByUserIdAsync(user.Id, tasks, cancellationToken);
        var k = 0;
        while (stat.CountSuccess >= k)
        {
            var reward = await _achievementRepository.UpdateProgressAndReturnRewardAsync
            (
                user.Id,
                new[] { "Успешно выполнить 3 задачи", "Успешно выполнить 10 задач", "Успешно выполнить 50 задач", "Успешно выполнить 100 задач" },
                cancellationToken
            );

            if (reward > 0)
            {
                await _lvlService.AddRewardToLvLAsync(reward, cancellationToken);
            }

            k++;
        }
    

        return new DailyStatisticsDto
        {
            CountSuccess = stat.CountSuccess,
            CountMiddle = stat.CountMiddle,
            CountFailure = stat.CountFailed,
            Reward = stat.Priority
        };
    }
}

