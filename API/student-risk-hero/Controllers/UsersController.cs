using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Services.Dtos;

namespace student_risk_hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IBaseService<User> baseService;

        public UsersController(IBaseService<User> baseService)
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

            if (entity == null) return NotFound($"The user with id {id} was not found");

            return Ok(entity);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, UserDto data)
        {
            var entity = baseService.Get(id);

            if (entity == null) return NotFound($"The user with id {id} was not found");

            entity.Firstname = data.Firstname;
            entity.Lastname = data.Lastname;
            entity.Username = data.Username;
            entity.Gender = data.Gender;

            return Ok(baseService.Update(entity));
        }
    }
}
