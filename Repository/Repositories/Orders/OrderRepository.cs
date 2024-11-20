using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;

namespace Repository.Repositories.Orders
{
    public class OrderRepository(MyDbContext context, DbSet<Order> dbSet) : Repository<Order>(context, dbSet), IOrderRepository
    {
        public bool ChangeOrderStatus(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
