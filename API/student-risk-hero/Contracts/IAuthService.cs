using student_risk_hero.Services.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace student_risk_hero.Contracts
{
    public interface IAuthService
    {
        public string Login(CredentialDto credentials);
        public JwtPayload GetData(string token);
    }
}
