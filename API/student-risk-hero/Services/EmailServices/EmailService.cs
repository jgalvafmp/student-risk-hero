using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Utills;
using System.Net.Mail;

namespace student_risk_hero.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        public void SendNewUserMail(User user, string subject, EmailTypesEnum emailType)
        {
            try
            {
                MailMessage newMail = new MailMessage();
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                newMail.From = new MailAddress("student.risk.hero@gmail.com", "STUDENT RISK HERO");

                newMail.To.Add(user.Email);

                newMail.Subject = subject;

                newMail.IsBodyHtml = true;

                switch(emailType)
                {
                    case EmailTypesEnum.Welcoming:
                        newMail.Body = EmailTemplates.BasicTemplate("Welcome to Student Risk Hero",
                             new string[] {
                                 $"Hello {user.Firstname}",
                                 "Thanks for joining our family, we're glad to have you here; Currently, you need to active your account by clicking on the link below",
                                 "After activating your accouunt, you can login with the following credentials:",
                                $"<ul><li>{user.Username}</li><li>Your password is the one that you entered</li></ul>"
                             }
                            , $"{ENV.WEBAPP_URL}/validate-account/{Cryptography.Encode(user.Username)}");
                        break;
                    case EmailTypesEnum.ForgettingPassword:
                        newMail.Body = EmailTemplates.BasicTemplate("Welcome to Student Risk Hero",
                             new string[] {
                                 $"Hello {user.Firstname}",
                                 "We're sorry that you forgot your password, but we're here to help, click the following link to change your password."
                             }
                            , $"{ENV.WEBAPP_URL}/forgot-password/{Cryptography.Encode(user.Username)}");
                        break;
                    default:
                        newMail.Body = "<h1> Error email </h1>";
                        break;
                }


                client.EnableSsl = true;
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("student.risk.hero@gmail.com", "finjdrhfgjdgfpau");

                client.Send(newMail);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error trying to send the email");
            }
        }
    }
}
