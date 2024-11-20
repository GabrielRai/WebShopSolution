using Repository.Models;

namespace Repository.Repositories
{
    // Gränssnitt för produktrepositoryt enligt Repository Pattern
    public interface IOrderRepository : IRepository<Order>
    {
        bool ChangeOrderStatus(Order order);
    }
}
