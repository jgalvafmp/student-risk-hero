using student_risk_hero.Contracts;
using System.Linq.Expressions;

namespace student_risk_hero.Services
{
    public class BaseService<T> : IBaseService<T> where T : class, IAuditable
    {
        private readonly IBaseRepository<T> baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public virtual T Add(T entity)
        {
            return baseRepository.Add(entity);
        }

        public  bool Exists(Guid id)
        {
            return baseRepository.Exists(id);
        }

        public bool Exists(Expression<Func<T, bool>>? filter = null)
        {
            return baseRepository.Exists(filter);
        }

        public T? Get(Guid id)
        {
            return baseRepository.Get(id);
        }

        public T? Get(Expression<Func<T, bool>>? filter = null)
        {
            return baseRepository.Get(filter);
        }

        public TResult? Get<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>>? filter = null)
        {
            return baseRepository.Get(transform, filter);
        }

        public IEnumerable<T> GetAll()
        {
            return baseRepository.GetAll();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>>? filter = null)
        {
            return baseRepository.GetAll(transform, filter);
        }

        public IEnumerable<TResult> GetAll<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>>? filter = null)
        {
            return baseRepository.GetAll(transform, filter);
        }

        public void Remove(T entity)
        {
            baseRepository.Remove(entity);
        }

        public void Remove(Guid id)
        {
            baseRepository.Remove(id);
        }

        public T Update(T entity)
        {
            return baseRepository.Update(entity);
        }
    }
}
