using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services;

internal class DayTaskService : IDayTaskService
{
    private readonly IDayTaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDayScheduleRepository _scheduleRepository;

    public DayTaskService(
        IDayTaskRepository taskRepository,
        ICurrentUserService currentUserService,
        ICategoryRepository categoryRepository,
        IDayScheduleRepository scheduleRepository)
    {
        _taskRepository = taskRepository;
        _currentUserService = currentUserService;
        _categoryRepository = categoryRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task CreateAsync(TaskCreateDto taskDto, CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        int scheduleId;
        if (taskDto.IsRepeat && taskDto.Date.HasValue)
        {
            WeekDay dayOfWeek = (WeekDay)taskDto.Date.Value.Date.DayOfWeek;
            scheduleId = await _scheduleRepository.GetOrCreateScheduleIdByDayOfWeekAsync(user.Id, dayOfWeek, cancellationToken);
        }
        else
        {
            scheduleId = await _scheduleRepository.GetOrCreateScheduleIdByDateAsync(user.Id, taskDto.Date, cancellationToken);
        }

        var dayTask = new DayTask
        {
            Title = taskDto.Title,
            Img = taskDto.Img,
            IsCompleted = false,
            Reward = taskDto.Reward,
            CheckPoint = 0,
            CheckPoints = taskDto.CheckPoints,
            IsDeleted = false,
            IsArchived = taskDto.IsArchived,
            IsRepeat = taskDto.IsRepeat,
            CategoryId = taskDto.Category.Id,
            ScheduleId = scheduleId,
            UserId = user.Id
        };

        await _taskRepository.CreateAsync(dayTask, cancellationToken);
    }

    public async Task<IEnumerable<TaskGetDto>> GetTasksForUserByDateAsync(DateTime? date, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var tasks = await _taskRepository.GetForUserByDateAsync(user.Id, date, cancellationToken);

        var scheduleIds = tasks.Select(t => t.ScheduleId).Distinct().ToList();
        var categoriesIds = tasks.Select(t => t.CategoryId).Distinct().ToList();

        var schedules = await _scheduleRepository.GetByIdsAsync(scheduleIds, cancellationToken);
        var categories = await _categoryRepository.GetByIdsAsync(categoriesIds, cancellationToken);

        var scheduleDict = schedules.ToDictionary(s => s.Id);

        return tasks.Select(task => {

            var schedule = scheduleDict.GetValueOrDefault(task.ScheduleId);
            var category = categories.FirstOrDefault(c => c.Id == task.CategoryId);
            
            //await _categoryRepository.GetByIdAsync(task.CategoryId, cancellationToken);

            CategoryDto categoryDto = new CategoryDto {
                Id = task.CategoryId,
                Title = category?.Title ?? ""
            };

            return new TaskGetDto
            {
                Id = task.Id,
                Title = task.Title,
                Img = task.Img,
                IsCompleted = task.IsCompleted,
                Reward = task.Reward,
                CheckPoint = task.CheckPoint,
                CheckPoints = task.CheckPoints,
                IsArchived = task.IsArchived,
                IsRepeat = task.IsRepeat,
                Category = categoryDto,
                Date = schedule.Date
            };
        });
    }

    public async Task UpdateAsync(TaskDto taskDto, CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var scheduleId = await _scheduleRepository.GetOrCreateScheduleIdByDateAsync(user.Id, taskDto.Date, cancellationToken);

        var dayTask = new DayTask
        {
            Id = taskDto.Id,
            Title = taskDto.Title,
            Img = taskDto.Img,
            IsCompleted = taskDto.IsCompleted,
            Reward = taskDto.Reward,
            CheckPoint = taskDto.CheckPoint,
            CheckPoints = taskDto.CheckPoints,
            IsArchived = taskDto.IsArchived,
            IsRepeat = taskDto.IsRepeat,
            CategoryId = taskDto.Category.Id,
            ScheduleId = scheduleId,
            UserId = user.Id
        };

        await _taskRepository.UpdateAsync(dayTask, cancellationToken);
    }

    public async Task<TaskDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var dayTask = await _taskRepository.GetByIdAsync(id, cancellationToken);
        var user = await _currentUserService.GetCurrentUserAsync();
        var date = await _scheduleRepository.GetDateAsync(user.Id, dayTask.ScheduleId, cancellationToken);
        var categoryDto = new CategoryDto
        {
            Id = dayTask.Category.Id,
            Title = dayTask.Category.Title
        };

        if (dayTask == null)
            return null;

        return new TaskDto
        {
            Id = dayTask.Id,
            Title = dayTask.Title,
            Img = dayTask.Img,
            IsCompleted = dayTask.IsCompleted,
            Reward = dayTask.Reward,
            CheckPoint = dayTask.CheckPoint,
            CheckPoints = dayTask.CheckPoints,
            IsArchived = dayTask.IsArchived,
            IsRepeat = dayTask.IsRepeat,
            Category = categoryDto,
            Date = date
        };
    }

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var userTasks = await _taskRepository.GetByUserIdAsync(user.Id, cancellationToken);
        var taskToDelete = userTasks.FirstOrDefault(t => t.Id == id);

        if (taskToDelete == null)
        {
            throw new InvalidOperationException($"Задача с ID {id} не найдена или не принадлежит пользователю");
        }

        taskToDelete.IsDeleted = true;

        await _taskRepository.UpdateAsync(taskToDelete, cancellationToken);
    }
}
