using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class AchievementRepository : GenericRepository<Achievement>, IAchievementRepository
{
    public AchievementRepository(AppContext context) : base(context) { }

    public async Task<IEnumerable<Achievement>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await context.Achievements
            .Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddProgressAsync(int achievementId, int xpToAdd, CancellationToken cancellationToken = default)
    {
        var achievement = await context.Achievements.FindAsync(new object[] { achievementId }, cancellationToken);
        if (achievement != null && !achievement.IsCompleted)
        {
            achievement.CurrentXp += xpToAdd;
            if (achievement.CurrentXp >= achievement.UpperBounds)
            {
                achievement.CurrentXp = achievement.UpperBounds;
                achievement.IsCompleted = true;
            }
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task CompleteAchievementAsync(int achievementId, CancellationToken cancellationToken = default)
    {
        var achievement = await context.Achievements.FindAsync(new object[] { achievementId }, cancellationToken);
        if (achievement != null)
        {
            achievement.IsCompleted = true;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    public async Task<int> UpdateProgressAndReturnRewardAsync(int userId, string[] titles, CancellationToken cancellationToken, int currentProgress = -1)
    {
        var taskCreationAchievements = await context.Achievements
            .Where(a => a.UserId == userId && titles.Contains(a.Title))
            .ToListAsync(cancellationToken);

        int reward = 0;
        foreach (var achievement in taskCreationAchievements)
        {
            if (!achievement.IsCompleted)
            {
                if (currentProgress != -1)
                {
                    await AddProgressAsync(achievement.Id, currentProgress - achievement.CurrentXp, cancellationToken);
                }
                else
                {
                    await AddProgressAsync(achievement.Id, 1, cancellationToken);
                }

                if (achievement.CurrentXp >= achievement.UpperBounds)
                {
                    await CompleteAchievementAsync(achievement.Id, cancellationToken);
                    reward += achievement.Reward;
                }
            }
        }
        return reward;
    }
}
