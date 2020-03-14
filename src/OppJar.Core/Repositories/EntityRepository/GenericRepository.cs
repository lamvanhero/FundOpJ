using OppJar.Domain;

namespace OppJar.Core.Repositories
{
    public sealed class GenericRepository<TEntity> : Repository<TEntity>, IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        public GenericRepository(OppJarContext context) : base(context) { }
    }
}
