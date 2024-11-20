using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Consume dbContext
        private readonly DbContext _dbContext;
        // Use dbSet
        private readonly DbSet<T> _dbSet;

        public Repository(MyDbContext context, DbSet<T> dbSet)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }
        public Task Add(T entity)
        {
            _dbSet.Add(entity);
            return _dbContext.SaveChangesAsync();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
