using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class DayScheduleRepository : GenericRepository<DaySchedule>, IDayScheduleRepository
{
    public DayScheduleRepository(AppContext context) : base(context) { }

    public async Task<IEnumerable<DaySchedule>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await context.DaySchedules
            .Where(ds => ds.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<DaySchedule?> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await context.DaySchedules
            .FirstOrDefaultAsync(ds => ds.Date == date.Date, cancellationToken);
    }
}
