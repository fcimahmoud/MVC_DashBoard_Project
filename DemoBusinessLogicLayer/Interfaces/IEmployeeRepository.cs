
namespace DemoBusinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Task<IEnumerable<Employee>> GetAllAsync(string Address);
        public Task <IEnumerable<Employee>> GetAllWithDepartmentsAsync();

    }
}
