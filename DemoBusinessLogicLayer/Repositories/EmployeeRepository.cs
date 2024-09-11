

using DemoBusinessLogicLayer.Interfaces;
using DemoDataAccessLayer.Data;
using DemoDataAccessLayer.Models;

namespace DemoBusinessLogicLayer.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public int Create(Employee entity)
        {
            _context.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _context.Remove(entity);
            return _context.SaveChanges(); ;
        }

        public Employee? Get(int id)
            => _context.Employees.Find(id);

        public IEnumerable<Employee> GetAll()
            => _context.Employees.ToList();

        public int Update(Employee entity)
        {
            _context.Update(entity);
            return _context.SaveChanges();
        }
    }
}
