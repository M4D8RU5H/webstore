using WebStore.Persistence.Repositories.CartItemRepo;
using WebStore.Persistence.Repositories.CategoryRepo;
using WebStore.Persistence.Repositories.ItemRepo;
using WebStore.Persistence.Repositories.OrderRepo;

namespace WebStore.Persistence.Repositories.WorkUnit
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository Items { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        ICartItemRepository CartItems { get; }

        Task Complete();
    }
}
