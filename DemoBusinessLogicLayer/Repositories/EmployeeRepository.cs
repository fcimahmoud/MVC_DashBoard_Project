
namespace DemoBusinessLogicLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Employee> GetAll(string Address)
            => _dbSet.Where(e => e.Address.ToLower() == Address.ToLower()).ToList();

        public IEnumerable<Employee> GetAllWithDepartments()
            => _dbSet.Include(e => e.Department).ToList();
    }
}
