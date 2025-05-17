using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}
