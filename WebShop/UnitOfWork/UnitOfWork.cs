using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Repository.Repositories.Products;


namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MyDbContext _context;
        private readonly DbSet<Product> _dbSet;
        public IProductRepository Products { get; set; }
        public UnitOfWork(MyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = new ProductRepository(_context, _dbSet);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
