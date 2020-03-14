using OppJar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OppJar.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> pression = null);

        TEntity FindBy(params object[] keyValues);

        Task<TEntity> FindByAsync(params object[] keyValues);

        IQueryable<TEntity> AsNoTracking(Expression<Func<TEntity, bool>> pression = null);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

    }
}
