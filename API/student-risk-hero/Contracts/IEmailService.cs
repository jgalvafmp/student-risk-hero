using student_risk_hero.Data.Models;
using student_risk_hero.Services.EmailServices;

namespace student_risk_hero.Contracts
{
    public interface IEmailService
    {
        public void SendNewUserMail(User user, string subject, EmailTypes emailType);
    }
}
