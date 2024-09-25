

namespace DemoPresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        //private readonly IGenericRepository<Department> _repo;
        private IDepartmentRepository _repo;
        public DepartmentsController(IDepartmentRepository repo)
        { 
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Retrieve All Departments
            var departments = await _repo.GetAllAsync();
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
			await _repo.AddAsync(department);
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
                    _repo.Update(department);
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
                    _repo.Delete(department);
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
            var department = await _repo.GetAsync(id.Value);
            if (department is null) return NotFound();
            return View(viewName, department);
        }
    }
}
