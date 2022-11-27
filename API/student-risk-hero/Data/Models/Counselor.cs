namespace student_risk_hero.Data.Models
{
    public class Counselor: BaseEntity
    {
        public string? Identification { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Specialty { get; set; }
        public Guid? UserId { get; set; }
    }
}
