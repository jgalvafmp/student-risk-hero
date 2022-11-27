namespace student_risk_hero.Services.Dtos
{
    public class CourseStudentDto
    {
        public Guid CourseId { get; set; }
        public List<Guid> StudentsId { get; set; }
    }
}
