using Microsoft.AspNetCore.Mvc;
using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Services.Dtos;

namespace student_risk_hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService baseService;
        private readonly IBaseService<User> userService;

        public AuthController(IAuthService baseService, IBaseService<User> userService)
        {
            this.baseService = baseService;
            this.userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(CredentialDto credentials)
        {
            try
            {
                return Ok(baseService.Login(credentials));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("sign-in")]
        public IActionResult Post(User entity)
        {
            try
            {
                return Ok(userService.Add(entity));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
