using ISession = NHibernate.ISession;
using WebStore.Models;
using WebStore.Persistence.Repositories.Generic;
using System.Security.Cryptography.X509Certificates;
using NHibernate.Linq;

namespace WebStore.Persistence.Repositories.CategoryRepo
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ISession session) : base(session)
        {         
        }

        public async Task<IEnumerable<Category>> GetRootCategories()
        {
            return await _session.Query<Category>()
                                 .Where(x => x.ParentCategory == null)
                                 .ToListAsync();
        }
    }
}
