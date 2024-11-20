using Repository.Repositories;

namespace Repository.Repositories.Products
{
    // Gränssnitt för produktrepositoryt enligt Repository Pattern
    public interface IProductRepository : IRepository<Product>
    {
        bool UpdateProductStock(Product product, int quantity);
    }
}
