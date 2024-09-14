
namespace DemoBusinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IEnumerable<Employee> GetAll(string Address);
        public IEnumerable<Employee> GetAllWithDepartments();

    }
}
