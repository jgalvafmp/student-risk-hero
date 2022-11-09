using System.Linq.Expressions;

namespace student_risk_hero.Contracts
{
    public interface IBaseService<T>
    {
        T Add(T entity);

        void Remove(T entity);

        void Remove(Guid id);

        T Update(T entity);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>>? filter = null);

        IEnumerable<TResult> GetAll<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>>? filter = null);

        T? Get(Guid id);

        T? Get(Expression<Func<T, bool>>? filter = null);

        TResult? Get<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>>? filter = null);

        bool Exists(Guid id);

        bool Exists(Expression<Func<T, bool>>? filter = null);
    }
}
