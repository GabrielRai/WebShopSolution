using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Products
{
    public class ProductRepository(MyDbContext context, DbSet<Product> dbSet) : Repository<Product>(context, dbSet), IProductRepository
    {
        public bool UpdateProductStock(Product product, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
