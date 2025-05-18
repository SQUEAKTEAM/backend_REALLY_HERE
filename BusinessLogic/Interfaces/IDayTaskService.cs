using DataAccess.Interfaces;

namespace BusinessLogic.Interfaces;

public interface IDayTaskService
{
    Task CreateAsync(TaskCreateDto taskDto, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskDto>> GetTasksForUserByDateAsync(DateTime? date, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskDto taskDto, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
}
