
namespace DemoBusinessLogicLayer.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity?> GetAsync(int id);
		Task<IEnumerable<TEntity>> GetAllAsync();
        void Update(TEntity entity);
    }
}
