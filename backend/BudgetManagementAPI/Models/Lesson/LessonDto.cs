using BudgetManagementAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManagementAPI.Models.Lesson;

public class LessonDto
{
    public Guid LessonId { get; set; }

    public Guid LevelLessonId { get; set; }

    public byte[]? CoverImage { get; set; }

    public IFormFile CoverImageFile { get; set; }

    public byte[]? FileText { get; set; }

    public IFormFile ContentLesson { get; set; }

    public string LessonContentName { get; set; }
}

