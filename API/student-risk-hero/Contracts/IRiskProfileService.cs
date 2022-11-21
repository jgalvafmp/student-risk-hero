using student_risk_hero.Data.Models.RiskProfiles;
using student_risk_hero.Services.Dtos;

namespace student_risk_hero.Contracts
{
    public interface IRiskProfileService: IBaseService<RiskProfile>
    {
        void Approve(Guid id, RiskProfileApprovalDto entity);
        void UpdateEntry(Guid riskProfileId, Guid entryId, RiskProfileEntries entity);
        void AddEntry(Guid riskProfileId, RiskProfileEntries entity);
        void AddEvidence(Guid riskProfileId, RiskProfileEvidence entity);
        void AddClosingReason(Guid riskProfileId, RiskProfileClosingDto entity);
    }
}
