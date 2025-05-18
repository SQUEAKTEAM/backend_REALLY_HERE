

using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services;

internal class AchievementService: IAchievementService
{
    private readonly IAchievementRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public AchievementService(IAchievementRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }
    public async Task<IEnumerable<AchievementDto>> GetAchievementsAsync(CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var achievements = await _repository.GetByUserIdAsync(user.Id, cancellationToken);

        return achievements.Select(achievement => {
            return new AchievementDto
            {
                Id = achievement.Id, 
                Title = achievement.Title,
                CurrentXp = achievement.CurrentXp,
                UpperBounds = achievement.UpperBounds,
                Reward = achievement.Reward,
                IsCompleted = achievement.IsCompleted
            };
        });
    }
}
