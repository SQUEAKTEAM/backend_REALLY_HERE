using DataAccess.Models;

namespace BusinessLogic.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user, CancellationToken cancellationToken = default);
}
