using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Data.Models.RiskProfiles;
using student_risk_hero.Data.Repositories;

namespace student_risk_hero.Data
{
    public static partial class DataConfiguration
    {
        public static void AddDataConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Security
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();

            // Main Actors
            services.AddScoped<IBaseRepository<Student>, BaseRepository<Student>>();
            services.AddScoped<IBaseRepository<Teacher>, BaseRepository<Teacher>>();
            services.AddScoped<IBaseRepository<Director>, BaseRepository<Director>>();
            services.AddScoped<IBaseRepository<Counselor>, BaseRepository<Counselor>>();

            // School related
            services.AddScoped<IBaseRepository<Course>, BaseRepository<Course>>();
            services.AddScoped<IBaseRepository<CourseGrades>, BaseRepository<CourseGrades>>();

            // Main Process
            services.AddScoped<IBaseRepository<RiskProfile>, BaseRepository<RiskProfile>>();
            services.AddScoped<IBaseRepository<RiskProfileEntries>, BaseRepository<RiskProfileEntries>>();
            services.AddScoped<IBaseRepository<RiskProfileEvidence>, BaseRepository<RiskProfileEvidence>>();
        }
    }
}
