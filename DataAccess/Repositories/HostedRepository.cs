using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class HostedRepository : IHostedRepository
{
    protected readonly AppContext context;

    public HostedRepository(AppContext context)
    {
        this.context = context;
    }
    public async Task UpdateStatisticsAndLvLAsync(int userId, IEnumerable<DayTask> tasks, CancellationToken cancellationToken = default)
    {
        var tasksByCategory = tasks
            .Where(t => !t.IsDeleted)
            .GroupBy(t => t.CategoryId);

        int failureReward = 0;

        foreach (var categoryGroup in tasksByCategory)
        {
            var category = categoryGroup.First().Category;

            foreach (var task in categoryGroup)
            {
                if (task.IsCompleted)
                {
                    category.CountSuccess++;
                }
                else
                {
                    double completionRatio = (double)task.CheckPoint / task.CheckPoints;

                    int taskValue = (int)(task.Reward * (1 - completionRatio));
                    failureReward += taskValue;

                    if (completionRatio >= 0.5)
                    {
                        category.CountMiddle++;
                    }
                    else
                    {
                        category.CountFailed++;
                    }
                }
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        await UpdateLvLAsync(userId, failureReward, cancellationToken);
    }

    private async Task UpdateLvLAsync(int userId, int failureReward, CancellationToken cancellationToken)
    {

        var user = await context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new ArgumentException($"User with ID {userId} not found");
        }

        while (user.CurrentXp < failureReward)
        {
            user.CurrentLvl--;

            if (user.CurrentLvl == 0)
            {
                user.CurrentXp = 0;
                user.CurrentLvl = 1;
                user.UpperBounds = CalculateUpperBounds(user.CurrentLvl);
                break;
            }

            user.UpperBounds = CalculateUpperBounds(user.CurrentLvl);

            user.CurrentXp += user.UpperBounds;
            
        }

        user.CurrentXp = Math.Max(0, user.CurrentXp - failureReward);

        await context.SaveChangesAsync(cancellationToken);
    }

    private int CalculateUpperBounds(int level)
    {
        return level * 10;
    }

    public async Task ResetProgressForRepeatedTasksAsync(int userId, IEnumerable<DayTask> tasks, CancellationToken cancellationToken = default)
    {
        foreach (var task in tasks)
        {
            task.CheckPoint = 0;
            task.IsCompleted = false;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
