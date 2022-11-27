using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace student_risk_hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentsController : BaseController<Assignment>
    {
        private readonly IBaseService<Assignment> baseService;
        private readonly IBaseService<AssignmentStudent> assignmentStudentService;
        private readonly IBlobStorageService storageService;

        public AssignmentsController(
            IBaseService<Assignment> baseService, 
            IBaseService<AssignmentStudent> assignmentStudentService,
            IBlobStorageService StorageService) : base(baseService)
        {
            this.baseService = baseService;
            this.assignmentStudentService = assignmentStudentService;
            storageService = StorageService;
        }

        [HttpPost]
        public IActionResult SubmitHomeWork([FromBody]HomeworkDto homework, [FromForm] IFormFile asset)
        {
            if (asset == null) return BadRequest("File is not valid");

            var fileName = $"{homework.AssignmentId}_{homework.StudentId}_{asset.FileName}";
            Stream stream = asset.OpenReadStream();
            storageService.UploadDocument(fileName, stream);
            assignmentStudentService.Add(new AssignmentStudent()
            {
                StudentId = homework.StudentId,
                AssignmentId = homework.AssignmentId,
                BlobUrl = fileName
            });

            return Ok();
        }

        [HttpGet("student/{assignmentId}")]
        public IActionResult GetHomeWork(Guid assignmentId)
        {
            var homework = assignmentStudentService.Get(x => x.Include(x => x.Assignment), x => x.AssignmentId == assignmentId);
            return Ok(homework);
        }
    }
}
