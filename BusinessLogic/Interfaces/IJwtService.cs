using System.Security.Claims;
using DataAccess.Models;

namespace BusinessLogic.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(User user);
    public ClaimsPrincipal? GetPrincipalFromToken(string token, bool validateLifetime = false);
}
