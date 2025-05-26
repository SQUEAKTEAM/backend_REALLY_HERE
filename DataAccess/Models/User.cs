using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string HashPass { get; set; } = string.Empty;
    
    [Required]
    public int CurrentLvl { get; set; }

    [Required]
    public float CurrentXp { get; set; }

    [Required]
    public float UpperBounds { get; set; }
    
    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public ICollection<DaySchedule> Schedules { get; set; } = new List<DaySchedule>();

    public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    public ICollection<DayTask> Tasks { get; set; } = new List<DayTask>();
}
