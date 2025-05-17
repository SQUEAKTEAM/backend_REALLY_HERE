using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository repository;
    private readonly IJwtService service;
    
    public UserService(IUserRepository repository, IJwtService service)
    {
        this.repository = repository;
        this.service = service;
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
    
    public async Task<string> Login(AuthUserDto authDto, CancellationToken cancellationToken = default)
    {
        var user = await repository.GetByEmailAsync(authDto.Email);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var result = new PasswordHasher<User>()
            .VerifyHashedPassword(user, user.HashPass, authDto.Password);
        if (result == PasswordVerificationResult.Success)
        {
            return service.GenerateToken(user);
        }
        else
        {
            throw new Exception("Unauthorized");
        }
    }
}
