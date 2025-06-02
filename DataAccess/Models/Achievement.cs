using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

public class Achievement
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int CurrentXp { get; set; }

    [Required]
    public int UpperBounds { get; set; }

    [Required]
    public int Reward { get; set; }

    [Required]
    public bool IsCompleted { get; set; }

    [Required]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public static Achievement CreateDefault(int userId, string title, int upperBounds, int reward)
    {
        return new Achievement
        {
            Title = title.Trim(),
            CurrentXp = 0,
            UpperBounds = upperBounds,
            Reward = reward,
            IsCompleted = false,
            UserId = userId
        };
    }
}
