using Microsoft.AspNetCore.Mvc;
using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Services.Dtos;
using student_risk_hero.Services.EmailServices;
using student_risk_hero.Utills;
using System.Text;

namespace student_risk_hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService baseService;
        private readonly IBaseService<User> userService;
        private readonly IEmailService EmailService;

        public AuthController(
            IAuthService baseService, 
            IBaseService<User> userService,
            IEmailService emailService)
        {
            this.baseService = baseService;
            this.userService = userService;
            EmailService = emailService;
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

        [HttpPost("validate/user/{token}")]
        public IActionResult ValidateUser(string token)
        {
            var username = Cryptography.Decode(token);

            var entity = userService.Get(user => user.Username == username);

            if (entity == null) return NotFound($"The user with name {username} was not found");

            entity.IsValidated = true;

            return Ok(userService.Update(entity));
        }

        [HttpPost("forgot-password/user/{username}")]
        public IActionResult ForgotPassword(string username)
        {
            var entity = userService.Get(user => user.Username == username);

            if (entity == null) return NotFound($"The user with name {username} was not found");

            EmailService.SendNewUserMail(entity, "Forgot your password in Student Risk Hero", EmailTypesEnum.ForgettingPassword);

            return Ok();
        }

        [HttpPut("forgot-password")]
        public IActionResult ForgotPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var username = Cryptography.Decode(forgetPasswordDto.Token);

            var entity = userService.Get(user => user.Username == username);

            if (entity == null) return NotFound($"The user with name {username} was not found");

            entity.Password = Cryptography.Encode(forgetPasswordDto.Password);

            return Ok(userService.Update(entity));
        }
    }
}
