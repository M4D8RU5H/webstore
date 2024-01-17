using ISession = NHibernate.ISession;
using WebStore.Models;
using WebStore.Persistence.Repositories.Generic;

namespace WebStore.Persistence.Repositories.CartItemRepo
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ISession session) : base(session)
        {
        }
    }
}
