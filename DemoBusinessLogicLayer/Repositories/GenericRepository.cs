

using Microsoft.EntityFrameworkCore;

namespace DemoBusinessLogicLayer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DataContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public int Create(TEntity entity)
        {
            _dbSet.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return _dbContext.SaveChanges();
        }
        public TEntity? Get(int id)
            => _dbSet.Find(id);
        public IEnumerable<TEntity> GetAll()
            => _dbSet.ToList();  
        public int Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
