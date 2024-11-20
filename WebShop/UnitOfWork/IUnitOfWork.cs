using Repository.Repositories;
using Repository.Repositories.Products;


namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        int Complete();
    }
}

