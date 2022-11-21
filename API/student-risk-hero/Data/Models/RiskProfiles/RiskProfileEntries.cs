namespace student_risk_hero.Data.Models.RiskProfiles
{
    public class RiskProfileEntries :BaseEntity
    {
        public int RiskProfileId { get; set; }
        public string Finding { get; set; }
        public string Description { get; set; }
        public bool IsClosingFinding { get; set; }

        public int? TeacherId { get; set; }
        public int? CounselorId { get; set; }
        public int? DirectorId { get; set; }

        private string assistantType;
        public string AssistantType
        {
            get => assistantType;
            set
            {

                if (value == nameof(RoleTypes.Teacher) ||
                   value == nameof(RoleTypes.Counselor) ||
                   value == nameof(RoleTypes.Director))
                    assistantType = value;
                else
                    assistantType = null;
            }
        }
    }
}
