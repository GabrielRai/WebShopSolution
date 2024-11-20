using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.OrderItems
{
    public class OrderItemRepository(MyDbContext context, DbSet<OrderItem> dbSet) : Repository<OrderItem>(context, dbSet), IOrderItemRepository
    {
       
    }
}
