using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IHostedRepository
{
    Task UpdateStatisticsAndLvLAsync(int userId, IEnumerable<DayTask> tasks, CancellationToken cancellationToken = default);
}
