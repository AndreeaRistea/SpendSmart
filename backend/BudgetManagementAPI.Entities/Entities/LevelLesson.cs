using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetManagementAPI.Entities.Enums;

namespace BudgetManagementAPI.Entities.Entities
{
    public class LevelLesson
    {
        [Key]
        public Guid LevelLessonId { get; set; }

        [Column(TypeName = "text")]
        public Level Level { get; set; }

        public required double MinValue {  get; set; }

        public required double MaxValue { get; set;}

        public ICollection<Lesson>? Lessons { get; set; }
    }
}
