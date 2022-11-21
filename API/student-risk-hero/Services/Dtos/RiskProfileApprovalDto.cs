using student_risk_hero.Data.Models;

namespace student_risk_hero.Services.Dtos
{
    public class RiskProfileApprovalDto
    {
        public int RequestId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int? TeacherId { get; set; }
        public int? CounselorId { get; set; }
        public int? DirectorId { get; set; }

        private string approvalType;
        public string ApprovalType
        {
            get => approvalType;
            set
            {

                if (value == nameof(RoleTypes.Teacher) ||
                   value == nameof(RoleTypes.Counselor) ||
                   value == nameof(RoleTypes.Director))
                    approvalType = value;
                else
                    approvalType = null;
            }
        }
    }
}
