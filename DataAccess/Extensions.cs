using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection serviceCollection,
        string? connectionString = null
    )
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IAchievementRepository, AchievementRepository>();
        serviceCollection.AddScoped<IDayScheduleRepository, DayScheduleRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IDayTaskRepository, DayTaskRepository>();
        serviceCollection.AddScoped<IHostedRepository, HostedRepository>();

        serviceCollection.AddDbContext<AppContext>(x =>
        {
            x.UseNpgsql(connectionString);
        });
        
        return serviceCollection;
    }
}
