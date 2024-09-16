
namespace DemoBusinessLogicLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Employee> GetAll(string name)
            => _dbSet.Where(e => e.Name.ToLower().Contains(name.ToLower())).Include(e => e.Department).ToList();

        public IEnumerable<Employee> GetAllWithDepartments()
            => _dbSet.Include(e => e.Department).ToList();
    }
}
