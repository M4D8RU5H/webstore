using ISession = NHibernate.ISession;
using WebStore.Models;
using WebStore.Persistence.Repositories.Generic;

namespace WebStore.Persistence.Repositories.OrderRepo
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ISession session) : base(session)
        {
        }
    }
}
