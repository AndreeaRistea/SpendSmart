using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace BudgetManagementAPI.Entities.Entities
{
    public class Lesson
    {
        [Key]
        public Guid LessonId { get; set; }

        [ForeignKey(nameof(LevelLesson))]
        public required Guid LevelLessonId { get; set; }

        public virtual LevelLesson Level { get; set; }

        public virtual List<User>? Users { get; set; }

        public byte[]? CoverImage { get; set; }

        [DataType(DataType.Upload)]
        [NotMapped]
        public required IFormFile CoverImageFile { get; set; }
        
        public byte[]? FileText { get; set; }

        [DataType(DataType.Upload)]
        [NotMapped]
        public required IFormFile ContentLesson { get; set; }    

        public string? LessonContentName {get; set; }
    }
}
