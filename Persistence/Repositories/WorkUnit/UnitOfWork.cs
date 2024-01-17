using NHibernate;
using WebStore.Persistence.Repositories.CartItemRepo;
using WebStore.Persistence.Repositories.CategoryRepo;
using WebStore.Persistence.Repositories.ItemRepo;
using WebStore.Persistence.Repositories.OrderRepo;
using ISession = NHibernate.ISession;

namespace WebStore.Persistence.Repositories.WorkUnit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public IItemRepository Items { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public ICartItemRepository CartItems { get; private set; }

        public UnitOfWork(ISession session)
        {
            _session = session;
            _transaction = _session.BeginTransaction();

            Items = new ItemRepository(_session);
            Categories = new CategoryRepository(_session);
            Orders = new OrderRepository(_session);
            CartItems = new CartItemRepository(_session);
        }

        public async Task Complete()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
           _session.Dispose();
        }
    }
}
