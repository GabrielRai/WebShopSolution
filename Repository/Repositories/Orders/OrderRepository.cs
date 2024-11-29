using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;

namespace Repository.Repositories.Orders
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly MyDbContext _context;
        private readonly DbSet<Order> _dbSet;
        public OrderRepository(MyDbContext context, DbSet<Order> dbSet) : base(context, dbSet)
        {
            _context = context;
            _dbSet = context.Set<Order>();
        }
        public bool createOrder(Order order)
        {
            if (order == null)
            {
                return false;
            }

            _context.Add(order);

            return true;
        }
    }
}
