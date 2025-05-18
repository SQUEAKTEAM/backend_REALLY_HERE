namespace BusinessLogic;

public class LevelDto
{
    public int CurrentLvl { get; set; }
    public int CurrentExp { get; set; }
    public int UpperBoundExp { get; set; }
}

public class StatisticsDto
{
    public int Id { get; set; }
    public int CountSuccess { get; set; }
    public int CountMiddle { get; set; }
    public int CountFailure { get; set; }
    public string Title { get; set; }
}

public class DailyStatisticsDto
{
    public int CountSuccess { get; set; }
    public int CountMiddle { get; set; }
    public int CountFailure { get; set; }
    public int Reward { get; set; }
}

public class TaskCreateDto
{
    public string Title { get; set; } = null!;
    public string Img { get; set; } = null!;
    public int Reward { get; set; }
    public int CheckPoints { get; set; }
    public bool IsArchived { get; set; }
    public bool IsRepeat { get; set; }
    public string CategoryTitle { get; set; }
    public DateTime? Date { get; set; }
}

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Img { get; set; } = null!;
    public bool IsCompleted { get; set; }
    public int Reward { get; set; }
    public int CheckPoint { get; set; }
    public int CheckPoints { get; set; }
    public bool IsArchived { get; set; }
    public bool IsRepeat { get; set; }
    public string CategoryTitle { get; set; }
    public DateTime? Date { get; set; }
}

public class AchievementDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int CurrentXp { get; set; }
    public int UpperBounds { get; set; }
    public int Reward { get; set; }
    public bool IsCompleted { get; set; }
}

public class AuthUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
