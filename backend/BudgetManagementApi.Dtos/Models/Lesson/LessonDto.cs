using Microsoft.AspNetCore.Http;

namespace BudgetManagementApi.Dtos.Models.Lesson;

public class LessonDto
{
    public required Guid LessonId { get; set; }

    public required Guid LevelLessonId { get; set; }

    public byte[]? CoverImage { get; set; }

    public required IFormFile CoverImageFile { get; set; }

    public byte[]? FileText { get; set; }

    public required IFormFile ContentLesson { get; set; }

    public required string LessonContentName { get; set; }
}

