using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Repositories;

internal class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppContext context) : base(context) { }

    public async Task<IEnumerable<Category>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await context.Categories
            .Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetOrCreateCategoryIdByTitleAsync(int userId, string title, CancellationToken cancellationToken = default)
    {
        var existingCategoryId = await context.Categories
            .Where(c => c.UserId == userId && c.Title.ToLower() == title.ToLower())
            .Select(c => c.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingCategoryId > 0)
        {
            return existingCategoryId;
        }

        var newCategory = Category.CreateDefault(userId, title);
        await CreateAsync(newCategory, cancellationToken);
        return newCategory.Id;
    }

    public async Task<string> GetTitleAsync(int userId, int catId, CancellationToken cancellationToken = default) {
        var categoryTitle = await context.Categories
            .Where(c => c.UserId == userId && c.Id == catId)
            .Select(c => c.Title)
            .FirstOrDefaultAsync(cancellationToken);

        if (categoryTitle == null)
        {
            throw new KeyNotFoundException($"Category with ID {catId} not found for user {userId}");
        }

        return categoryTitle;
    }

    public async Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct)
    {
        return await context.Categories
            .Where(s => ids.Contains(s.Id))
            .ToListAsync(ct);
    }
}
