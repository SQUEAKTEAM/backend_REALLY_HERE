using DataAccess.Models;

namespace BusinessLogic.Interfaces;

public interface ICurrentUserService
{
    Task<User?> GetCurrentUserAsync();
}
