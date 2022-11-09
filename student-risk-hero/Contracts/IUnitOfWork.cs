using Microsoft.EntityFrameworkCore.Storage;

namespace student_risk_hero.Contracts
{
    public interface IUnitOfWork
    {
        IDbContextTransaction CreateTransaction();
        int SaveChanges();
    }
}
