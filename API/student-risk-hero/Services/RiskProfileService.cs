using student_risk_hero.Contracts;
using student_risk_hero.Data.Models.RiskProfiles;
using student_risk_hero.Services.Dtos;

namespace student_risk_hero.Services
{
    public class RiskProfileService : BaseService<RiskProfile>, IRiskProfileService
    {
        public RiskProfileService(IBaseRepository<RiskProfile> baseRepository) : base(baseRepository)
        {
        }

        public void AddClosingReason(Guid riskProfileId, RiskProfileClosingDto entity)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(Guid riskProfileId, RiskProfileEntries entity)
        {
            throw new NotImplementedException();
        }

        public void AddEvidence(Guid riskProfileId, RiskProfileEvidence entity)
        {
            throw new NotImplementedException();
        }

        public void Approve(Guid id, RiskProfileApprovalDto entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntry(Guid riskProfileId, Guid entryId, RiskProfileEntries entity)
        {
            throw new NotImplementedException();
        }
    }
}
