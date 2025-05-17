using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services;

internal class DayTaskService : IDayTaskService
{
    private readonly IDayTaskRepository repository;
    
    public DayTaskService(IDayTaskRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task CreateAsync(TaskCreateDto taskDto, int userId, CancellationToken cancellationToken = default)
    {
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
            CategoryId = taskDto.CategoryId,
            ScheduleId = taskDto.ScheduleId,
            UserId = userId
        };

        await repository.CreateAsync(dayTask, cancellationToken);
    }
    
    public async Task<IEnumerable<TaskDto>> GetTasksForUserByDateAsync(int userId, DateTime? date, CancellationToken cancellationToken)
    {
        var tasks = await repository.GetForUserByDateAsync(userId, date, cancellationToken);
        
        return tasks.Select(task => new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Img = task.Img,
            IsCompleted = task.IsCompleted,
            Reward = task.Reward,
            CheckPoint = task.CheckPoint,
            CheckPoints = task.CheckPoints,
            IsDeleted = task.IsDeleted,
            IsArchived = task.IsArchived,
            IsRepeat = task.IsRepeat,
            CategoryId = task.CategoryId
        });
    }
    
    public async Task UpdateAsync(TaskDto taskDto, CancellationToken cancellationToken = default)
    {
        var dayTask = new DayTask
        {
            Id = taskDto.Id,
            Title = taskDto.Title,
            Img = taskDto.Img,
            IsCompleted = taskDto.IsCompleted,
            Reward = taskDto.Reward,
            CheckPoint = taskDto.CheckPoint,
            CheckPoints = taskDto.CheckPoints,
            IsDeleted = taskDto.IsDeleted,
            IsArchived = taskDto.IsArchived,
            IsRepeat = taskDto.IsRepeat,
            CategoryId = taskDto.CategoryId
        };

        await repository.UpdateAsync(dayTask, cancellationToken);
    }
    
    public async Task<TaskDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var dayTask = await repository.GetByIdAsync(id, cancellationToken);
        
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
            IsDeleted = dayTask.IsDeleted,
            IsArchived = dayTask.IsArchived,
            IsRepeat = dayTask.IsRepeat,
            CategoryId = dayTask.CategoryId
        };
    }
    
    public async Task DeleteByIdAsync(int id, int userId, CancellationToken cancellationToken = default)
    {
        var userTasks = await repository.GetByUserIdAsync(userId, cancellationToken);
        var taskToDelete = userTasks.FirstOrDefault(t => t.Id == id);
        if (taskToDelete == null)
        {
            throw new InvalidOperationException($"Задача с ID {id} не найдена или не принадлежит пользователю");
        }
    
        taskToDelete.IsDeleted = true;
    
        await repository.UpdateAsync(taskToDelete, cancellationToken);
    }
}
