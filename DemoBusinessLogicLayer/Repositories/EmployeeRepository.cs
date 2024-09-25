
namespace DemoBusinessLogicLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(string name)
            => await _dbSet.Where(e => e.Name.ToLower().Contains(name.ToLower())).Include(e => e.Department).ToListAsync();

        public async Task<IEnumerable<Employee>> GetAllWithDepartmentsAsync()
            => await _dbSet.Include(e => e.Department).ToListAsync();
    }
}
