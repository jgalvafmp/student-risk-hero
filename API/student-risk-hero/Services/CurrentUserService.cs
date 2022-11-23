using student_risk_hero.Contracts;
using System.Security.Claims;

namespace student_risk_hero.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var currentContext = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(currentContext)) UserId = new Guid(currentContext);
            else UserId = null;
            IsAuthenticated = UserId.HasValue;
        }

        public Guid? UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
