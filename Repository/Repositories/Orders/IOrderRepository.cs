using Repository.Models;

namespace Repository.Repositories.Orders
{
    // Gränssnitt för produktrepositoryt enligt Repository Pattern
    public interface IOrderRepository : IRepository<Order>
    {
        bool CreateOrder(Order order);
    }
}
