using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Services.EmailServices;

namespace student_risk_hero.Services
{
    public static partial class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseService<User>, UserService>();
            services.AddScoped<IBaseService<Student>, BaseService<Student>>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
