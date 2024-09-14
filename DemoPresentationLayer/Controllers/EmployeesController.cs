
using DemoDataAccessLayer.Models;

namespace DemoPresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeRepository _employeeRepo;
        private IDepartmentRepository _departmentRepo;
        public EmployeesController(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo)
        {
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
        }

        public IActionResult Index()
        {
            // ViewData => Dictionary<string,object>
            //ViewData["Message"] = "Hello From ViewData";

            // C# Feature ViewBag
            //ViewBag.Message = "Hello From ViewBag";

            var employees = _employeeRepo.GetAllWithDepartments();
            return View(employees);
        }

        public IActionResult Create()
        {
            var departments = _departmentRepo.GetAll();
            SelectList listItems = new SelectList(departments , "Id", "Name");
            ViewBag.Departments = listItems;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid) return View();
            _employeeRepo.Create(employee);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details([FromRoute] int? id)
            => EmployeeControllerHandler(id, nameof(Details));

        public IActionResult Edit([FromRoute] int? id)
            => EmployeeControllerHandler(id , nameof(Edit));
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (_employeeRepo.Update(employee) > 0)
                        TempData["Message"] = "Employee Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // log Exception
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employee);

        }

        public IActionResult Delete([FromRoute] int? id)
            => EmployeeControllerHandler(id, nameof(Delete));
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int? id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepo.Delete(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employee);
        }

        public IActionResult EmployeeControllerHandler(int? id, string viewName)
        {
            if(viewName == nameof(Edit))
            {
                var departments = _departmentRepo.GetAll();
                SelectList listItems = new SelectList(departments, "Id", "Name");
                ViewBag.Departments = listItems;
            }
            if (!id.HasValue) return BadRequest();
            var department = _employeeRepo.Get(id.Value);
            if (department is null) return NotFound();
            return View(viewName, department);
        }

    }
}
