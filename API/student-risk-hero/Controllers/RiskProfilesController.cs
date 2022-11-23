using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Data.Models.RiskProfiles;
using student_risk_hero.Services.Dtos;

namespace student_risk_hero.Controllers
{
    [Route("api/risk-profile")]
    [ApiController]
    [Authorize]
    public class RiskProfilesController : ControllerBase
    {
        private readonly IRiskProfileService baseService;

        public RiskProfilesController(IRiskProfileService baseService)
        {
            this.baseService = baseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(baseService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var entity = baseService.Get(id);

            if (entity == null) return NotFound($"The {nameof(RiskProfile)} with id {id} was not found");

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Post(RiskProfile entity)
        {
            return Ok(baseService.Add(entity));
        }

        [HttpPost("{id}")]
        public IActionResult Approve(
            [FromRoute] Guid id, 
            [FromBody] RiskProfileApprovalDto entity)
        {
            baseService.Approve(id, entity);

            return Ok();
        }

        [HttpPost("{riskProfileId}/entry")]
        public IActionResult AddEntry(
            [FromRoute] Guid riskProfileId,
            [FromBody] RiskProfileEntries entity)
        {
            baseService.AddEntry(riskProfileId, entity);

            return Ok();
        }

        [HttpPatch("{riskProfileId}/entry/{entryId}")]
        public IActionResult UpdateEntry(
            [FromRoute] Guid riskProfileId,
            [FromRoute] Guid entryId,
            [FromBody] RiskProfileEntryDto entity)
        {
            baseService.UpdateEntry(riskProfileId, entryId, entity);

            return Ok();
        }

        [HttpPost("{riskProfileId}/evidence")]
        public IActionResult AddEvidence(
            [FromRoute] Guid riskProfileId,
            [FromBody] RiskProfileEvidenceDto entity)
        {
            baseService.AddEvidence(riskProfileId, entity);

            return Ok();
        }

        [HttpPost("{riskProfileId}/Closing")]
        public IActionResult AddClosingReason(
            [FromRoute] Guid riskProfileId,
            [FromBody] RiskProfileClosingDto entity)
        {
            baseService.AddClosingReason(riskProfileId, entity);

            return Ok();
        }

        [HttpPatch("{riskProfileId}")]
        public IActionResult Close([FromRoute] Guid riskProfileId)
        {
            var entity = baseService.Get(riskProfileId);

            if (entity == null) return NotFound("Risk profile not found");

            entity.State = nameof(RiskProfileStatesEnum.Closed);

            baseService.Update(entity);

            return Ok();
        }
    }
}
