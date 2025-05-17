using System.Security.Claims;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services;

internal class CurrentUserService : ICurrentUserService
{
    private readonly IUserRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IUserRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<User> GetCurrentUserAsync()
    {
        if (_httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated != true)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var userEmail = _httpContextAccessor.HttpContext.User.FindFirstValue("Email");
        
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedAccessException("Email claim not found in token");
        }

        var user = await _repository.GetByEmailAsync(userEmail);
        
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found in database");
        }

        return user;
    }
}
