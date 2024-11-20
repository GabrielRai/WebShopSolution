using Repository.Repositories;
using Repository.Repositories.OrderItems;
using Repository.Repositories.Orders;
using Repository.Repositories.Products;


namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }

        int Complete();
    }
}

