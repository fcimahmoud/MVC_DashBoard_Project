
using DemoDataAccessLayer.Models;

namespace DemoBusinessLogicLayer.Interfaces
{
    internal interface IEmployeeRepository
    {
        int Create(Employee entity);
        int Delete(Employee entity);
        Employee? Get(int id);
        IEnumerable<Employee> GetAll();
        int Update(Employee entity);
    }
}
