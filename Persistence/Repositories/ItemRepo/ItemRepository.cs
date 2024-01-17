using ISession = NHibernate.ISession;
using WebStore.Models;
using WebStore.Persistence.Repositories.Generic;
using NHibernate.Linq;

namespace WebStore.Persistence.Repositories.ItemRepo
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(ISession session) : base(session)
        {
        }

        public async Task<IEnumerable<Item>> GetPage(int pageIndex, int pageSize)
        {
            return await _session.Query<Item>()
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }
    }
}
