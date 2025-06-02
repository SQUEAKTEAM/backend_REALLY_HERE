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
    private readonly IAchievementRepository achievementRepository;
    private readonly ICategoryRepository categoryRepository;
    private readonly IJwtService jwtService;
    
    public UserService(IUserRepository repository, IAchievementRepository achievementRepository, ICategoryRepository categoryRepository, IJwtService jwtService)
    {
        this.repository = repository;
        this.achievementRepository = achievementRepository;
        this.categoryRepository = categoryRepository;
        this.jwtService = jwtService;
    }

    public async Task<bool> IsEmailRegisteredAsync(string email, CancellationToken cancellationToken = default)
    {
        return await repository.GetByEmailAsync(email, cancellationToken) != null;
    }
    
    public async Task Register(AuthUserDto authDto, CancellationToken cancellationToken = default)
    {
        if (await IsEmailRegisteredAsync(authDto.Email, cancellationToken))
        {
            throw new ArgumentException("Email уже занят");
        }
        
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

        var defaultAchievements = GetDefaultAchievements(user.Id);
        var defaultCategories = GetDefaultCategories(user.Id);

        foreach (var achievement in defaultAchievements)
        {
            await achievementRepository.CreateAsync(achievement, cancellationToken);
        }

        foreach (var category in defaultCategories)
        {
            await categoryRepository.CreateAsync(category, cancellationToken);
        }
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

    public List<Achievement> GetDefaultAchievements(int userId)
    {
        return new List<Achievement>
        {
            Achievement.CreateDefault(userId, "Создать 3 задачи", 3, 10),
            Achievement.CreateDefault(userId, "Создать 9 задач", 9, 30),
            Achievement.CreateDefault(userId, "Создать 12 задач", 12, 50),
            Achievement.CreateDefault(userId, "Удалить 1 задачу", 1, 10),
            Achievement.CreateDefault(userId, "Удалить 3 задачи", 3, 30),
            Achievement.CreateDefault(userId, "Удалить 6 задач", 6, 50),

            Achievement.CreateDefault(userId, "Создать 1 категорию", 1, 10),
            Achievement.CreateDefault(userId, "Создать 3 категории", 3, 30),
            Achievement.CreateDefault(userId, "Создать 5 категорий", 5, 50),

            Achievement.CreateDefault(userId, "Получить 5 лвл", 5, 20),
            Achievement.CreateDefault(userId, "Получить 20 лвл", 20, 100),
            Achievement.CreateDefault(userId, "Получить 50 лвл", 50, 500),

            Achievement.CreateDefault(userId, "Успешно выполнить 3 задачи", 5, 10),
            Achievement.CreateDefault(userId, "Успешно выполнить 10 задач", 20, 50),
            Achievement.CreateDefault(userId, "Успешно выполнить 50 задач", 50, 250),
            Achievement.CreateDefault(userId, "Успешно выполнить 100 задач", 50, 500),
        };
    }

    public List<Category> GetDefaultCategories(int userId)
    {
        return new List<Category>
        {
            Category.CreateDefault(userId, "Работа"),
            Category.CreateDefault(userId, "Учеба"),
            Category.CreateDefault(userId, "Спорт"),
            Category.CreateDefault(userId, "Личное"),
            Category.CreateDefault(userId, "Другое")
        };
    }
}
