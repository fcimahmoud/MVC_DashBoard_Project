
using DemoDataAccessLayer.Date;
using DemoDataAccessLayer.Models;

namespace DemoBusinessLogicLayer.Repositories
{
    internal class DepartmentRepository
    {

        /*
         * Get , Get All , Create , Update , Delete

        // Dependency Injection
        // Method Injection => Method ([FromServices]DataContext dataContext)
        // Property Injection => 
        //  [FromServices]
        //  public DataContex DataContext { get , Set }
         */


        private readonly DataContext _dataContext;

        // CTOR Injection
        public DepartmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Department? Get(int id) 
            => _dataContext.Departments.Find(id);
        public IEnumerable<Department> GetAll() 
            => _dataContext.Departments.ToList();
        public int Create(Department entity)
        {
            _dataContext.Departments.Add(entity);
            return _dataContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dataContext.Departments.Update(entity);
            return _dataContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dataContext.Departments.Remove(entity);
            return _dataContext.SaveChanges();
        }
    }
}
