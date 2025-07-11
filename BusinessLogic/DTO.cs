﻿namespace BusinessLogic;

public class LevelDto
{
    public int CurrentLvl { get; set; }
    public float CurrentExp { get; set; }
    public float UpperBoundExp { get; set; }
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
    public float Reward { get; set; }
}

public class TaskCreateDto
{
    public string Title { get; set; } = null!;
    public string Img { get; set; } = null!;
    public int Reward { get; set; }
    public int CheckPoints { get; set; }
    public bool IsArchived { get; set; }
    public bool IsRepeat { get; set; }
    public CategoryDto Category { get; set; }
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
    public CategoryDto Category { get; set; }
    public DateTime? Date { get; set; }
}

public class TaskGetDto
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
    public CategoryDto Category { get; set; }
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

public class EmailRequestDto
{
    public string Email { get; set; }
}

public class ResetPasswordDto
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
}

public class CategoryCreateDto
{
    public string Title { get; set; }
}

public class CategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}