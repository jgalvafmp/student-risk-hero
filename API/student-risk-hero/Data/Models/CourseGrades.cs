namespace student_risk_hero.Data.Models
{
    public class CourseGrades : BaseEntity
    {
        public int StudentId { get; set; }
        public string Subject { get; set; }
        public decimal Grade { get; set; }
    }
}
