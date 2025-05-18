using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;

public static class Extensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDayTaskService, DayTaskService>();
        serviceCollection.AddScoped<IStatisticsService, StatisticsService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IJwtService, JwtService>();
        serviceCollection.AddScoped<ICurrentUserService, CurrentUserService>();
        return serviceCollection;
    }
}
