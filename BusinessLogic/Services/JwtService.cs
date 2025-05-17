using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Services;

internal class JwtService(IOptions<AuthSettings> options) : IJwtService
{
    public string GenerateToken(User user, CancellationToken cancellationToken = default)
    {
        var claims = new List<Claim>
        {
            new Claim("Email", user.Email),
            new Claim("CurrentLvl", user.CurrentLvl.ToString()),
        };
        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials:
            new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
