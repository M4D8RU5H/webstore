using WebStore.Models;
using WebStore.Persistence.Repositories.Generic;

namespace WebStore.Persistence.Repositories.CategoryRepo
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<IEnumerable<Category>> GetRootCategories();
    }
}
