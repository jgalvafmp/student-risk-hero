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
        private readonly IBaseService<Student> StudentService;
        private readonly IBlobStorageService storageService;

        public AssignmentsController(
            IBaseService<Assignment> baseService,
            IBaseService<AssignmentStudent> assignmentStudentService,
            IBlobStorageService StorageService,
            IBaseService<Student> studentService) : base(baseService)
        {
            this.baseService = baseService;
            this.assignmentStudentService = assignmentStudentService;
            storageService = StorageService;
            this.StudentService = studentService;
        }

        [HttpPost("SubmitHomeWork")]
        public IActionResult SubmitHomeWork([FromBody] HomeworkDto homework, [FromForm] IFormFile asset)
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

        //[HttpGet("student/{assignmentId}")]
        //public IActionResult GetHomeWork(Guid assignmentId)
        //{
        //    var homework = assignmentStudentService.Get(x => x.Include(x => x.Assignment), x => x.AssignmentId == assignmentId);
        //    return Ok(homework);
        //}
        [HttpGet("student/{studentId}")]
        public IActionResult GetHomeWorkByStudent(Guid studentId)
        {
            var homework = assignmentStudentService.GetAll(x => x.Include(x => x.Assignment), x => x.StudentId == studentId);
            return Ok(homework);
        }
        [HttpPost("student/{studentId}/assignment/{assignmentId}")]
        public IActionResult GetAssignmentHomeWork(Guid studentId, Guid assignmentId)
        {
            var assignment = baseService.Get(x => x.Id == assignmentId);
            var student = StudentService.Get(x => x.Id == studentId);
            if (assignment == null || student == null) BadRequest();

            assignmentStudentService.Add(new AssignmentStudent()
            {
                //Assignment = assignment,
                StudentId = studentId,
                BlobUrl= "https://www.google.com/imgres?imgurl=https%3A%2F%2Fstatic.guiainfantil.com%2Fmedia%2F27022%2Fc%2Flas-tareas-cuento-corto-para-ninos-que-no-quieren-hacer-los-deberes-lg.jpg&imgrefurl=https%3A%2F%2Fwww.guiainfantil.com%2Focio%2Fcuentos-infantiles%2Flas-tareas-cuento-corto-para-ninos-que-no-quieren-hacer-los-deberes%2F&tbnid=PxkapYNE7mlJkM&vet=12ahUKEwi21MGlheT7AhVFO98KHUapCHEQMygDegUIARDlAQ..i&docid=w8CUGgZ28Uuh5M&w=1000&h=1000&q=tarea&ved=2ahUKEwi21MGlheT7AhVFO98KHUapCHEQMygDegUIARDlAQ",
                AssignmentId = assignmentId,
                Comments = assignment.Description
            });

            //var homework = assignmentStudentService.GetAll(x => x.Include(x => x.Assignment), x => x.StudentId == studentId);
            //return Ok(homework);
            return Ok();
        }
    }
}
