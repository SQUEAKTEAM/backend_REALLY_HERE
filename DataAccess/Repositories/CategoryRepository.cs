using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

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
}
