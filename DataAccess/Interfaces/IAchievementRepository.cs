using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IAchievementRepository : IGenericRepository<Achievement>
{
    Task AddProgressAsync(int achievementId, int progressAmount, CancellationToken cancellationToken = default);
    Task CompleteAchievementAsync(int achievementId, CancellationToken cancellationToken = default);
}
