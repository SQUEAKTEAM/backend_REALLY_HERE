using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class AppContext(DbContextOptions<AppContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<DaySchedule> DaySchedules { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<DayTask> DayTasks { get; set; }
}
