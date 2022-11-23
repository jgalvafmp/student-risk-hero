using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;

namespace student_risk_hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CounselorsController : BaseController<Counselor>
    {
        public CounselorsController(IBaseService<Counselor> baseService) : base(baseService)
        {
        }
    }
}
