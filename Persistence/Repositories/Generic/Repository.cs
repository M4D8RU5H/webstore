using NHibernate.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using ISession = NHibernate.ISession;

namespace WebStore.Persistence.Repositories.Generic
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public async Task<TEntity> Get(int id)
        {
            return await _session.GetAsync<TEntity>(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _session.Query<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _session.Query<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _session.SaveAsync(entity);
        }

        /*
        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _session.SaveAsync(entities);
        }
        */
        public async Task Delete(TEntity entity)
        {
            await _session.DeleteAsync(entity);
        }

        /*
        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            await _session.DeleteAsync(entities);
        }
        */
    }
}
