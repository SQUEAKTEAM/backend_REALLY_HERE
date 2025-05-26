using System.Security.Claims;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository repository;
    private readonly IJwtService jwtService;
    
    public UserService(IUserRepository repository, IJwtService jwtService)
    {
        this.repository = repository;
        this.jwtService = jwtService;
    }


    public async Task Register(AuthUserDto authDto, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Email = authDto.Email,
            CurrentLvl = 0,
            CurrentXp = 0,
            UpperBounds = 0,
        };
        var passHash = new PasswordHasher<User>().HashPassword(user, authDto.Password);
        user.HashPass = passHash;
        await repository.CreateAsync(user);
    }
    
    public async Task<TokenDto> Login(AuthUserDto authDto, CancellationToken cancellationToken = default)
    {
        var user = await repository.GetByEmailAsync(authDto.Email);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var result = new PasswordHasher<User>()
            .VerifyHashedPassword(user, user.HashPass, authDto.Password);
        
        if (result != PasswordVerificationResult.Success)
        {
            throw new Exception("Unauthorized");
        }
        
        return new TokenDto
        {
            AccessToken = jwtService.GenerateAccessToken(user),
            RefreshToken = jwtService.GenerateRefreshToken(user)
        };
    }

    public async Task<TokenDto> RefreshTokens(string accessToken, string refreshToken)
    {
        var principal = jwtService.GetPrincipalFromToken(accessToken, validateLifetime: false);
        var userEmail = principal.FindFirstValue("Email");
    
        if (string.IsNullOrEmpty(userEmail))
            throw new SecurityTokenException("Invalid token - email not found");

        var refreshPrincipal = jwtService.GetPrincipalFromToken(refreshToken, validateLifetime: true);
        var refreshUserEmail = refreshPrincipal.FindFirstValue("Email");
        var isRefreshToken = refreshPrincipal.Claims.Any(c => c.Type == "RefreshToken" && c.Value == "true");
    
        if (string.IsNullOrEmpty(refreshUserEmail) || !isRefreshToken || userEmail != refreshUserEmail)
            throw new SecurityTokenException("Invalid refresh token");

        var user = await repository.GetByEmailAsync(userEmail);
        if (user == null)
            throw new Exception("User not found");

        return new TokenDto
        {
            AccessToken = jwtService.GenerateAccessToken(user),
            RefreshToken = jwtService.GenerateRefreshToken(user)
        };
    }
}
