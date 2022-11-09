using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;

namespace student_risk_hero.Services
{
    public static partial class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseService<User>, BaseService<User>>();
        }
    }
}
