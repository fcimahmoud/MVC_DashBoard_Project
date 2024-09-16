

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

        public void Create(TEntity entity) => _dbSet.Add(entity);
        public void Delete(TEntity entity) => _dbSet.Remove(entity);
        public TEntity? Get(int id) => _dbSet.Find(id);
        public IEnumerable<TEntity> GetAll() => _dbSet.ToList();
        public void Update(TEntity entity) => _dbSet.Update(entity);
    }
}
