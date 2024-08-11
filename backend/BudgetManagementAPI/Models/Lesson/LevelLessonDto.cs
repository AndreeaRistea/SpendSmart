using BudgetManagementAPI.Enums;

namespace BudgetManagementAPI.Models.Lesson;

public class LevelLessonDto
{
    public Guid LevelLessonId { get; set; }
    public Level Level { get; set; }

    public required double MinValue { get; set; }

    public required double MaxValue { get; set; }
}

