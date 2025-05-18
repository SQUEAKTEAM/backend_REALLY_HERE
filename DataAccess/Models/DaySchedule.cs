using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

public class DaySchedule
{
    [Key]
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    [Required]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public ICollection<DayTask> Tasks { get; set; } = new List<DayTask>();

    public static DaySchedule CreateDefault(int userId, DateTime? date = null)
    {
        return new DaySchedule
        {
            UserId = userId,
            Date = date?.Date,
            Tasks = new List<DayTask>()
        };
    }
}
