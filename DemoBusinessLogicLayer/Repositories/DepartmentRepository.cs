
using DemoDataAccessLayer.Date;
using DemoDataAccessLayer.Models;

namespace DemoBusinessLogicLayer.Repositories
{
    internal class DepartmentRepository
    {

        /*
         * Get , Get All , Create , Update , Delete
         */

        // Dependency Injection
        // Method Injection => Method ([FromServices]DataContext dataContext)
        // Property Injection => 
        //  [FromServices]
        //  public DataContex DataContext { get , Set }

        private readonly DataContext _dataContext;

        // CTOR Injection
        public DepartmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //public Department Get(int id)
        //{
        //    var dept1 = _dataContext.Departments.FirstOrDefault(e => e.Id == id);
        //    return dept1;
        //}
    }
}
