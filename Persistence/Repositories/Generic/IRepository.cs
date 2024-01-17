using System.Linq.Expressions;

namespace WebStore.Persistence.Repositories.Generic
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate); //lambda

        Task Add(TEntity entity);
        //Task AddRange(IEnumerable<TEntity> entities);

        Task Delete(TEntity entity);
        //Task RemoveRange(IEnumerable<TEntity> entities);
    }
}
