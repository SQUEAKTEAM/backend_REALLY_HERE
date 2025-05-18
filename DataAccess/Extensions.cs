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
        
        serviceCollection.AddDbContext<AppContext>(x =>
        {
            x.UseNpgsql(connectionString ??
                        "Host=localhost;Port=5432;Database=squake;Username=postgres;Password=Wizard2310;");
        });
        
        return serviceCollection;
    }
}
