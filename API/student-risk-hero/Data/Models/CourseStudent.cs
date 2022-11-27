using System.ComponentModel.DataAnnotations.Schema;

namespace student_risk_hero.Data.Models
{
    public class CourseStudent: BaseEntity
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}
