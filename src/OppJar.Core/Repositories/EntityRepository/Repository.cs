using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using OppJar.Domain;

namespace OppJar.Core.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        private readonly OppJarContext _context;

        public Repository(OppJarContext context)
        {
            _context = context;
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> pression = null)
        {
            IQueryable<TEntity> query = null;

            if (pression == null) query = _context.Set<TEntity>();

            else query = _context.Set<TEntity>().Where(pression);

            return query;
        }

        public virtual TEntity FindBy(params object[] keyValues)
        {
            return _context.Set<TEntity>().Find(keyValues);
        }

        public virtual async Task<TEntity> FindByAsync(params object[] keyValues)
        {
            return await _context.Set<TEntity>().FindAsync(keyValues);
        }

        public IQueryable<TEntity> AsNoTracking(Expression<Func<TEntity, bool>> pression = null)
        {
            if (pression == null)
            {
                return _context.Set<TEntity>().AsNoTracking();
            }

            return _context.Set<TEntity>().AsNoTracking().Where(pression);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
