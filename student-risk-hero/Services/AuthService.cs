using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Services.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace student_risk_hero.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> baseRepository;

        public IConfiguration Configuration { get; }

        public AuthService(IBaseRepository<User> baseRepository, IConfiguration configuration)
        {
            this.baseRepository = baseRepository;
            Configuration = configuration;
        }

        public string Login(CredentialDto credentials)
        {
            if (string.IsNullOrEmpty(credentials.Username))
                throw new ArgumentNullException(nameof(credentials.Username));

            if (string.IsNullOrEmpty(credentials.Password))
                throw new ArgumentNullException(nameof(credentials.Password));

            var user = baseRepository.Get(user => user.Username == credentials.Username);

            if (user == null || user.Password != Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials.Password))) 
                throw new ArgumentException("Invalid credentials");

            return GenerateJWT(user);
        }

        private string GenerateJWT(User user)
        {
            List<Claim> claims = new()
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var expires = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                issuer: Configuration["Authentication:Issuer"],
                audience: Configuration["Authentication:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtPayload GetData(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;
        }
    }
}
