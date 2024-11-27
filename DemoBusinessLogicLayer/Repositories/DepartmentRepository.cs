

namespace DemoBusinessLogicLayer.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
