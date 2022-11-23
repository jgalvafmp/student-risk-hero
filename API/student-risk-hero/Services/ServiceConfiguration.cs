using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Data.Models.RiskProfiles;
using student_risk_hero.Services.EmailServices;

namespace student_risk_hero.Services
{
    public static partial class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            // General purpose
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // Security
            services.AddScoped<IBaseService<User>, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            // Main Actors
            services.AddScoped<IBaseService<Student>, BaseService<Student>>();
            services.AddScoped<IBaseService<Teacher>, BaseService<Teacher>>();
            services.AddScoped<IBaseService<Director>, BaseService<Director>>();
            services.AddScoped<IBaseService<Counselor>, BaseService<Counselor>>();

            // School related
            services.AddScoped<IBaseService<Course>, BaseService<Course>>();
            services.AddScoped<IBaseService<CourseGrades>, BaseService<CourseGrades>>();

            // Main Process
            services.AddScoped<IRiskProfileService, RiskProfileService>();
            services.AddScoped<IBaseService<RiskProfileEntries>, BaseService<RiskProfileEntries>>();
            services.AddScoped<IBaseService<RiskProfileEvidence>, BaseService<RiskProfileEvidence>>();
        }
    }
}
