using System.Threading;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public async Task<int> GetOrCreateScheduleIdByDateAsync(int userId, DateTime? date, CancellationToken ct)
    {
        var existingId = await GetIdByDateAsync(userId, date, ct);

        if (existingId > 0) return existingId;

        var newSchedule = DaySchedule.CreateDefault(userId, date);
        await CreateAsync(newSchedule, ct);
        return newSchedule.Id;
    }

    public async Task<int> GetIdByDateAsync(int userId, DateTime? date, CancellationToken cancellationToken = default)
    {
        var query = context.DaySchedules
            .Where(s => s.UserId == userId);

        if (!date.HasValue)
        {
            return await query
                .Where(s => !s.Date.HasValue && !s.DayOfWeek.HasValue)
                .Select(s => s.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        var targetDate = date.Value.Date;
        return await query
            .Where(s => s.Date.HasValue && s.Date.Value.Date == targetDate)
            .Select(s => s.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DateTime?> GetDateAsync(int userId, int scheduleId, CancellationToken cancellationToken = default)
    {
        var date = await context.DaySchedules
            .Where(c => c.UserId == userId && c.Id == scheduleId)
            .Select(c => c.Date)
            .FirstOrDefaultAsync(cancellationToken);

        return date;
    }

    public async Task<IEnumerable<DaySchedule>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct)
    {
        return await context.DaySchedules
            .Where(s => ids.Contains(s.Id))
            .ToListAsync(ct);
    }

    public async Task<int> GetOrCreateScheduleIdByDayOfWeekAsync(int userId, WeekDay dayOfWeek, CancellationToken cancellationToken = default)
    {
        var existingId = await context.DaySchedules
            .Where(s => s.UserId == userId && s.DayOfWeek == dayOfWeek)
            .Select(s => s.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingId > 0) return existingId;

        var newSchedule = DaySchedule.CreateDefault(userId, null, dayOfWeek);
        await CreateAsync(newSchedule, cancellationToken);
        return newSchedule.Id;
    }
}
