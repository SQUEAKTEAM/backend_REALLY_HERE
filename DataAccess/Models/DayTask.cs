using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

public class DayTask
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Img { get; set; } = string.Empty;

    [Required]
    public bool IsCompleted { get; set; }

    [Required]
    public int Reward { get; set; }

    [Required]
    public int CheckPoint { get; set; }

    [Required]
    public int CheckPoints { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    public bool IsArchived { get; set; }

    [Required]
    public bool IsRepeat { get; set; }

    [Required]
    public int ScheduleId { get; set; }

    [ForeignKey("ScheduleId")]
    public DaySchedule Schedule { get; set; } = null!;
    
    [Required]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = null!;
}
