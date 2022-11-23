namespace student_risk_hero.Data.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public bool? IsValidated { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Gender { get; set; }

        public string FullName => $"{Firstname} {Lastname}";

        private string role;
        public string Role { get => role; 
            set {
                
                if(value == nameof(RoleTypes.Teacher) ||
                   value == nameof(RoleTypes.Student) ||
                   value == nameof(RoleTypes.Counselor) ||
                   value == nameof(RoleTypes.Director))
                    role = value;
                else
                    role = null;
            } 
        }
    }
}
