using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Data.Repositories;

namespace student_risk_hero.Data
{
    public static partial class DataConfiguration
    {
        public static void AddDataConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<IBaseRepository<Student>, BaseRepository<Student>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
