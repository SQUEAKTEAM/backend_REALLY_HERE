using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IDayScheduleRepository : IGenericRepository<DaySchedule>
{
    Task<IEnumerable<DaySchedule>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<int> GetOrCreateScheduleIdByDateAsync(int userId, DateTime? date, CancellationToken ct);
    Task<int> GetIdByDateAsync(int userId, DateTime? date, CancellationToken cancellationToken = default);
    Task<DateTime?> GetDateAsync(int userId, int scheduleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DaySchedule>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct);
    Task<int> GetOrCreateScheduleIdByDayOfWeekAsync(int userId, WeekDay dayOfWeek, CancellationToken cancellationToken = default);
    Task<DaySchedule?> GetByTaskIdAsync(int taskId, CancellationToken cancellationToken = default);
}
