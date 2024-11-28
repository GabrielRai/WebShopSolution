using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Repositories.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly MyDbContext _context;
        private readonly DbSet<Product> _dbSet;
        public ProductRepository(MyDbContext context, DbSet<Product> dbSet) : base(context, dbSet)
        {
            _context = context;
            _dbSet = context.Set<Product>();
        }
        public bool UpdateProductStock(Product product, int quantity)
        {
            if (product == null || quantity < 0)
            {
                return false;
            }

            _context.Update(product.Stock = quantity);
            return true;
        }
    }
}
