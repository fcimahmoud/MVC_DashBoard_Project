

namespace DemoPresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
		//private readonly IGenericRepository<Department> _repo;
		//private IDepartmentRepository _repo;
		private readonly IUnitOfWork _unitOfWork;

		public DepartmentsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
            // Retrieve All Departments
            var departments = await _unitOfWork.Departments.GetAllAsync();
            return View(departments);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            // Server Side Validation
            if (!ModelState.IsValid) return View();
			await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id) 
            => await DepartmentControllerHandlerAsync(id, nameof(Details));

        public async Task<IActionResult> Edit(int? id) 
            => await DepartmentControllerHandlerAsync(id, nameof(Edit));
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if(id != department.Id) return BadRequest();
             
            if(ModelState.IsValid)
            {
                try
                {
					_unitOfWork.Departments.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    // log Exception
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id) 
            => await DepartmentControllerHandlerAsync(id, nameof(Delete));
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
					_unitOfWork.Departments.Delete(department);
                    _unitOfWork.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // log Exception
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> DepartmentControllerHandlerAsync(int? id, string viewName)
        {
            if (!id.HasValue) return BadRequest();
            var department = await _unitOfWork.Departments.GetAsync(id.Value);
            if (department is null) return NotFound();
            return View(viewName, department);
        }
    }
}
