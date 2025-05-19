using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Category GetDailyStatsByUserIdAsync(int userId, IEnumerable<DayTask> tasks, CancellationToken cancellationToken = default);
    Task<int> GetOrCreateCategoryIdByTitleAsync(int userId, string title, CancellationToken cancellationToken = default);
    Task<string> GetTitleAsync(int userId, int catId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct);
}
