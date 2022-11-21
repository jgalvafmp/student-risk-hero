using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Data.Models.RiskProfiles;
using student_risk_hero.Services.Dtos;

namespace student_risk_hero.Services
{
    public class RiskProfileService : BaseService<RiskProfile>, IRiskProfileService
    {
        private readonly IBaseRepository<RiskProfileEntries> entriesRepository;
        private readonly IBaseRepository<RiskProfileEvidence> evidenceRepository;

        public RiskProfileService(
            IBaseRepository<RiskProfile> baseRepository,
            IBaseRepository<RiskProfileEntries> entriesRepository,
            IBaseRepository<RiskProfileEvidence> evidenceRepository
            ) : base(baseRepository)
        {
            this.entriesRepository = entriesRepository;
            this.evidenceRepository = evidenceRepository;
        }

        public override RiskProfile Add(RiskProfile entity)
        {
            if (entity.StudentId > 0) throw new ArgumentNullException("StudentId cannot be null or 0");
            if (string.IsNullOrEmpty(entity.Risk)) throw new ArgumentNullException("Risk cannot be null");

            entity.State = nameof(RiskProfileStatesEnum.Draft);

            return base.Add(entity);
        }

        public void AddClosingReason(Guid riskProfileId, RiskProfileClosingDto entity)
        {
            var riskProfile = base.Get(riskProfileId);

            if(riskProfile == null) throw new ArgumentException("Risk profile does not exist");

            if (riskProfile.State != nameof(RiskProfileStatesEnum.Closing))
                throw new ArgumentException("Risk profile should be in closing state to add a closing reason");

            riskProfile.State = nameof(RiskProfileStatesEnum.Closed);

            var lastEntry = new RiskProfileEntries()
            {
                RiskProfileId = riskProfileId,
                Finding = entity.Finding,
                AssistantType = entity.AssistantType,
                IsClosingFinding = true,
                Description = entity.Description,
            };

            if (entity.AssistantType == nameof(RoleTypes.Teacher))
            {
                lastEntry.TeacherId = new Guid(entity.ActionerId);
            }

            if (entity.AssistantType == nameof(RoleTypes.Director))
            {
                lastEntry.DirectorId = new Guid(entity.ActionerId);
            }

            entriesRepository.Add(lastEntry);
            base.Update(riskProfile);
        }

        public void AddEntry(Guid riskProfileId, RiskProfileEntries entity)
        {
            var riskProfile = base.Get(riskProfileId);

            if (riskProfile == null) throw new ArgumentException("Risk profile does not exist");

            if (riskProfile.State != nameof(RiskProfileStatesEnum.InProgress))
                throw new ArgumentException("Risk profile should be in progress state to add entries");

            var entry = new RiskProfileEntries()
            {
                RiskProfileId = riskProfileId,
                Finding = entity.Finding,
                AssistantType = entity.AssistantType,
                IsClosingFinding = false,
                Description = entity.Description,
                Date = entity.Date
            };

            if (entity.AssistantType == nameof(RoleTypes.Teacher))
            {
                entry.TeacherId = new Guid(entity.ActionerId);
            }

            if (entity.AssistantType == nameof(RoleTypes.Director))
            {
                entry.DirectorId = new Guid(entity.ActionerId);
            }

            if (entity.AssistantType == nameof(RoleTypes.Counselor))
            {
                entry.CounselorId = new Guid(entity.ActionerId);
            }

            entriesRepository.Add(entity);
        }

        public void AddEvidence(Guid riskProfileId, RiskProfileEvidenceDto entity)
        {
            var riskProfile = base.Get(riskProfileId);

            if (riskProfile == null) throw new ArgumentException("Risk profile does not exist");

            if (riskProfile.State == nameof(RiskProfileStatesEnum.Closed))
                throw new ArgumentException("Risk profile should not be in closed state to add evidence");

            var blobUri = "";

            var evidence = new RiskProfileEvidence()
            {
                RiskProfileId = riskProfileId,
                BlobUrl = blobUri
            };

            evidenceRepository.Add(evidence);
        }

        public void Approve(Guid riskProfileId, RiskProfileApprovalDto entity)
        {
            var riskProfile = base.Get(riskProfileId);

            if (riskProfile == null) throw new ArgumentException("Risk profile does not exist");

            if (riskProfile.State != nameof(RiskProfileStatesEnum.Draft))
                throw new ArgumentException("Risk profile should be in draft state to be approve");

            if (entity.ApprovalType == nameof(RoleTypes.Teacher))
            {
                riskProfile.TeachersApproval = entity.TeachersApproval;
                riskProfile.TeachersApprovalDate = entity.TeachersApprovalDate;
            }

            if (entity.ApprovalType == nameof(RoleTypes.Director))
            {
                riskProfile.DirectorApproval = entity.DirectorApproval;
                riskProfile.DirectorApprovalDate = entity.DirectorApprovalDate;
            }

            if (entity.ApprovalType == nameof(RoleTypes.Counselor))
            {
                riskProfile.ParentsApproval = entity.ParentsApproval;
                riskProfile.ParentsApprovalDate = entity.ParentsApprovalDate;
            }

            if (
                riskProfile.DirectorApproval.HasValue && riskProfile.DirectorApproval.Value &&
                riskProfile.TeachersApproval.HasValue && riskProfile.TeachersApproval.Value &&
                riskProfile.ParentsApproval.HasValue && riskProfile.ParentsApproval.Value) 
            {
                riskProfile.State = nameof(RiskProfileStatesEnum.Approved);
            }

            base.Update(riskProfile);
        }

        public void UpdateEntry(Guid riskProfileId, Guid entryId, RiskProfileEntryDto entity)
        {
            var riskProfile = base.Get(riskProfileId);

            var entry = entriesRepository.Get(entryId);

            if (riskProfile == null) throw new ArgumentException("Risk profile does not exist");

            if (entry == null) throw new ArgumentException("Entry does not exist");

            if (riskProfile.State != nameof(RiskProfileStatesEnum.InProgress))
                throw new ArgumentException("Risk profile should be in progress state to add entries");

            entry.Finding = entity.Finding;

            entry.Description = entity.Description;

            entry.Date = entity.Date;

            entriesRepository.Update(entry);
        }
    }
}
