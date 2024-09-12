
namespace DemoBusinessLogicLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Employee> GetAll(string Address)
        {
            return _dbSet.Where(e => e.Address.ToLower() == Address.ToLower()).ToList();
        }

    }
}
