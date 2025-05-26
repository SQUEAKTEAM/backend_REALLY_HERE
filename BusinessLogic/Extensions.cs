using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.HostedServices;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.MemoryStorage;

namespace BusinessLogic;

public static class Extensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDayTaskService, DayTaskService>();
        serviceCollection.AddScoped<IStatisticsService, StatisticsService>();
        serviceCollection.AddScoped<ICategoryService, CategoryService>();
        serviceCollection.AddScoped<IAchievementService, AchievementService>();
        serviceCollection.AddScoped<ILvLService, LvLService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IJwtService, JwtService>();
        serviceCollection.AddScoped<ICurrentUserService, CurrentUserService>();
        serviceCollection.AddScoped<IHangfireHostedService, HangfireHostedService>();
        
        serviceCollection.AddHostedService<HangfireHostedService>();

        serviceCollection.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseMemoryStorage() //Только для РАЗРАБОТКИ (.UsePostgreSqlStorage() .UseRedisStorage())
        .UseRecommendedSerializerSettings());

        serviceCollection.AddHangfireServer();

        return serviceCollection;
    }
}
