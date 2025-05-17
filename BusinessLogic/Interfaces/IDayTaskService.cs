namespace BusinessLogic.Interfaces;

public interface IDayTaskService
{
    Task CreateAsync(TaskCreateDto taskDto, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskDto>> GetTasksForUserByDateAsync(int userId, DateTime? date, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskDto taskDto, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int id, int userId, CancellationToken cancellationToken = default);
}
