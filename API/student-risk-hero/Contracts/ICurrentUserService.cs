namespace student_risk_hero.Contracts
{
    public interface ICurrentUserService
    {
        public Guid? UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
