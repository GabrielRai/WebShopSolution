using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Categories
{
    public class CategoryRepository(MyDbContext context, DbSet<Category> dbSet) : Repository<Category>(context, dbSet), ICategoryRepository
    {
    }
}
