using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Repository.Models;
using Repository.Repositories.Categories;
using Repository.Repositories.Customers;
using Repository.Repositories.OrderItems;
using Repository.Repositories.Orders;
using Repository.Repositories.Products;


namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MyDbContext _context;
        private readonly DbSet<Product> _dbSetProduct;
        private readonly DbSet<Order> _dbSetOrder;
        private readonly DbSet<OrderItem> _dbSetOrderItem;
        private readonly DbSet<Category> _dbSetCategory;
        private readonly DbSet<Customer> _dbSetCustomer;

        public IProductRepository Products { get; set; }
        public IOrderRepository Orders { get; set; }
        public IOrderItemRepository OrderItems { get; set; }
        public ICategoryRepository Categories { get; set; }
        public ICustomerRepository Customers { get; set; }
        public UnitOfWork(MyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = new ProductRepository(_context, _dbSetProduct);
            Orders = new OrderRepository(_context, _dbSetOrder);
            OrderItems = new OrderItemRepository(_context, _dbSetOrderItem);
            Categories = new CategoryRepository(_context, _dbSetCategory);
            Customers = new CustomerRepository(_context, _dbSetCustomer);
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
