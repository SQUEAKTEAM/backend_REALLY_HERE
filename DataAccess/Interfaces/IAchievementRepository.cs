using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IAchievementRepository : IGenericRepository<Achievement>
{
    Task<IEnumerable<Achievement>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task AddProgressAsync(int achievementId, int progressAmount, CancellationToken cancellationToken = default);
    Task CompleteAchievementAsync(int achievementId, CancellationToken cancellationToken = default);
}
