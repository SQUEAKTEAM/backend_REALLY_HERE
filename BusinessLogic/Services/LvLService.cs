using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Npgsql.TypeMapping;

namespace BusinessLogic.Services;

internal class LvLService: ILvLService
{
    private readonly IUserRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public LvLService(IUserRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
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

        await _repository.UpdateAsync(user);
    }
}
