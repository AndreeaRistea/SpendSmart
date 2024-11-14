using BudgetManagementAPI.Entities.Enums;

namespace BudgetManagementApi.Dtos.Models.Lesson;

public class LevelLessonDto
{
    public required Guid LevelLessonId { get; set; }
    public required Level Level { get; set; }

    public required double MinValue { get; set; }

    public required double MaxValue { get; set; }
}

