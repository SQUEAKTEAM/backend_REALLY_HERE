using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

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
        Console.WriteLine("cron work!!!");
        var users = await _userRepository.GetAllAsync(cancellationToken);

        foreach (var user in users)
        {
            // Используем вчерашнюю дату для проверки выполнения задач
            var dateToCheck = DateTime.UtcNow.Date.AddDays(-1);

            var tasks = await _taskRepository.GetForUserByDateAsync(user.Id, dateToCheck, cancellationToken);

            await _repository.UpdateStatisticsAndLvLAsync(user.Id, tasks, cancellationToken);
        }
    }
}
