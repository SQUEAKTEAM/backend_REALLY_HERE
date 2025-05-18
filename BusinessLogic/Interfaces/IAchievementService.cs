namespace BusinessLogic.Interfaces;

public interface IAchievementService
{
    Task<IEnumerable<AchievementDto>> GetAchievementsAsync(CancellationToken cancellationToken = default);
}
