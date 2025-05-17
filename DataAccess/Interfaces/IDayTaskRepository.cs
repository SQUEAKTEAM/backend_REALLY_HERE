using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IDayTaskRepository : IGenericRepository<DayTask>
{
    Task<IEnumerable<DayTask>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DayTask>> GetByScheduleIdAsync(int scheduleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DayTask>> GetCompletedTasksAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<DayTask>> GetForUserByDateAsync(int userId, DateTime? date, CancellationToken cancellationToken = default);
    Task<IEnumerable<DayTask>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}
