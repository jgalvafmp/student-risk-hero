namespace student_risk_hero.Data.Models
{
    public class Role : BaseEntity
    {
        public Role(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
