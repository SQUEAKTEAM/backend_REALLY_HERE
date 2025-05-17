using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class DayTaskRepository : GenericRepository<DayTask>, IDayTaskRepository
{
    public DayTaskRepository(AppContext context) : base(context) { }

    public async Task<IEnumerable<DayTask>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return await context.DayTasks
            .Where(dt => dt.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DayTask>> GetByScheduleIdAsync(int scheduleId, CancellationToken cancellationToken = default)
    {
        return await context.DayTasks
            .Where(dt => dt.ScheduleId == scheduleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DayTask>> GetCompletedTasksAsync(CancellationToken cancellationToken = default)
    {
        return await context.DayTasks
            .Where(dt => dt.IsCompleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DayTask>> GetForUserByDateAsync(int userId, DateTime? date,
        CancellationToken cancellationToken = default)
    {
        return await context.DayTasks
            .Include(dt => dt.Schedule)
            .Where(dt =>
                dt.Schedule != null &&
                dt.Schedule.UserId == userId &&
                (!date.HasValue || dt.Schedule.Date == date.Value.Date))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<DayTask>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await context.DayTasks
            .Where(dt => dt.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}
