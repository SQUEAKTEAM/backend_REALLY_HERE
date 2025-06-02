namespace BusinessLogic.Interfaces;

public interface ILvLService
{
    Task<LevelDto> GetLvLAsync(CancellationToken cancellationToken = default);
    Task UpdateLvLAsync(LevelDto lvl, CancellationToken cancellationToken = default);
    Task AddRewardToLvLAsync(int reward, CancellationToken cancellationToken = default);
}
