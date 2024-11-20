using Repository.Repositories;

namespace Repository
{
    // Gränssnitt för produktrepositoryt enligt Repository Pattern
    public interface IProductRepository : IRepository<Product>
    {
        bool UpdateProductStock(Product product, int quantity);
    }
}
