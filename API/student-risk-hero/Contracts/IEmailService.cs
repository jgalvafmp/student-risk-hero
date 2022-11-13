using student_risk_hero.Data.Models;

namespace student_risk_hero.Contracts
{
    public interface IEmailService
    {
        public void SendNewUserMail(User user);
    }
}
