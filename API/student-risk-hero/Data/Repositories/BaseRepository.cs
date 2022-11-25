using Microsoft.EntityFrameworkCore;
using student_risk_hero.Contracts;
using System.Linq.Expressions;

namespace student_risk_hero.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IAuditable
    {
        private readonly DbSet<T> _dbSet;
        private readonly StudentRiskHeroContext _context;

        public BaseRepository(StudentRiskHeroContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public T Add(T entity)
        {
            entity.IsDeleted = false;

            _dbSet.Add(entity);

            _context.SaveChanges();

            return entity;
        }

        public bool Exists(Guid id)
        {
            return _dbSet.Any(x => x.Id == id);
        }

        public bool Exists(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null) return false;

            return _dbSet.Any(filter);
        }

        public T? Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public T? Get(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null) return null;

            return _dbSet.FirstOrDefault(filter);
        }

        public TResult? Get<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>>? filter = null)
        {
            var query = filter == null ? _dbSet : _dbSet.Where(filter);

            var results = transform(query);

            return results.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().Where(x => x.IsDeleted == false).ToList();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>>? filter = null)
        {
            var query = filter == null ? _dbSet.AsNoTracking() : _dbSet.AsNoTracking().Where(filter);

            var results = transform(query);

            return results.ToArray().ToList();
        }

        public IEnumerable<TResult> GetAll<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>>? filter = null)
        {
            var query = filter == null ? _dbSet.AsNoTracking() : _dbSet.AsNoTracking().Where(filter);

            var results = transform(query);

            return results.ToArray().ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry<T>(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Remove(Guid id)
        {
            T? entity = _dbSet.Find(id);

            if(entity == null) throw new NullReferenceException("Entity not found");

            _context.Entry<T>(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public T Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry<T>(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }
    }
}
