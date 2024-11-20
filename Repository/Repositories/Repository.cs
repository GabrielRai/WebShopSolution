using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Consume dbContext
        private readonly MyDbContext _dbContext;

        // Use dbSet
        private readonly DbSet<T> _dbSet;

        public Repository(MyDbContext context, DbSet<T> dbSet)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
           return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);

        }
    }
}
