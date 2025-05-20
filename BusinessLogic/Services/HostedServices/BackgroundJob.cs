using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services.HostedServices;

internal class BackgroundJob
{
    private readonly IHostedRepository _repository;
    private readonly IDayTaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public BackgroundJob(IHostedRepository repository, IDayTaskRepository taskRepository, IUserRepository userRepository)
    {
        _repository = repository;
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task UpdateStatisticsAndLvLAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        // Используем вчерашнюю дату для проверки выполнения задач
        var dateToCheckUtc = DateTime.UtcNow.Date.AddDays(-1);

        foreach (var user in users)
        {
            var tasks = await _taskRepository.GetForUserByDateAsync(user.Id, dateToCheckUtc, cancellationToken);
            await _repository.UpdateStatisticsAndLvLAsync(user.Id, tasks, cancellationToken);

            var dateToResetRepeatedTasksUtc = DateTime.UtcNow.Date.AddDays(-2);
            WeekDay dayOfWeek = (WeekDay)dateToResetRepeatedTasksUtc.DayOfWeek;
            var repeatedTasks = await _taskRepository.GetForUserByDayOfWeekAsync(user.Id, dayOfWeek, cancellationToken);
            await _repository.ResetProgressForRepeatedTasksAsync(user.Id, repeatedTasks, cancellationToken);
        }
    }
}
