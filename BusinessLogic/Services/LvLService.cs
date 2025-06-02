using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Npgsql.TypeMapping;

namespace BusinessLogic.Services;

internal class LvLService: ILvLService
{
    private readonly IUserRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAchievementRepository _achievementRepository;

    public LvLService(IUserRepository repository, ICurrentUserService currentUserService, IAchievementRepository achievementRepository)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _achievementRepository = achievementRepository;
    }
    public async Task<LevelDto> GetLvLAsync(CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();

        return new LevelDto
        {
            CurrentLvl = user.CurrentLvl,
            CurrentExp = user.CurrentXp,
            UpperBoundExp = user.UpperBounds
        };
    }
    public async Task UpdateLvLAsync(LevelDto lvl, CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        user.CurrentLvl = lvl.CurrentLvl;
        user.CurrentXp = lvl.CurrentExp;
        user.UpperBounds = lvl.UpperBoundExp;

        await _repository.UpdateAsync(user, cancellationToken);

        var reward = await _achievementRepository.UpdateProgressAndReturnRewardAsync
        (
            user.Id,
            new[] { "Получить 5 лвл", "Получить 20 лвл", "Получить 50 лвл" },
            cancellationToken,
            user.CurrentLvl
        );

        if (reward > 0)
        {
            await AddRewardToLvLAsync(reward, cancellationToken);
        }
    }
    public async Task AddRewardToLvLAsync(int reward, CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();

        user.CurrentXp += reward;
        while (user.CurrentXp >= user.UpperBounds)
        {
            user.CurrentLvl += 1;
            user.CurrentXp -= user.UpperBounds;
            user.UpperBounds = user.CurrentLvl * 10;
        }

        await _repository.UpdateAsync(user, cancellationToken);
    }
}
