using Microsoft.AspNetCore.Mvc;
using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Services.Dtos;
using System.Text;

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

        [HttpGet("validate/user/{token}")]
        public IActionResult ValidateUser(string token)
        {
            var username = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var entity = userService.Get(user => user.Username == username);

            if (entity == null) return NotFound($"The user with name {username} was not found");

            entity.IsValidated = true;

            return Ok(userService.Update(entity));
        }

        [HttpPost("forgot-password/user/{token}")]
        public IActionResult ForgotPassword(string token, string password)
        {
            var username = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var entity = userService.Get(user => user.Username == username);

            if (entity == null) return NotFound($"The user with name {username} was not found");

            entity.Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(password));

            return Ok(userService.Update(entity));
        }
    }
}
