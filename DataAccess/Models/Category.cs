using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int Priority { get; set; }

    [Required]
    public int CountSuccess { get; set; }

    [Required]
    public int CountFailed { get; set; }

    [Required]
    public int CountMiddle { get; set; }

    [Required]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public ICollection<DayTask> Tasks { get; set; } = new List<DayTask>();
}
