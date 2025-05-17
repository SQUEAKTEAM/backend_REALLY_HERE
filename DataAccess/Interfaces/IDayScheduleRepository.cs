using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IDayScheduleRepository : IGenericRepository<DaySchedule>
{
    Task<IEnumerable<DaySchedule>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<DaySchedule?> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default);
}
