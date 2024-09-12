
using DemoDataAccessLayer.Models;
using System.CodeDom;

namespace DemoPresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeRepository _repo;
        public EmployeesController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            // ViewData => Dictionary<string,object>
            //ViewData["Message"] = "Hello From ViewData";

            // C# Feature ViewBag
            //ViewBag.Message = "Hello From ViewBag";

            var employees = _repo.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid) return View();
            _repo.Create(employee);
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
                    if (_repo.Update(employee) > 0)
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
                    _repo.Delete(employee);
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
            if (!id.HasValue) return BadRequest();
            var department = _repo.Get(id.Value);
            if (department is null) return NotFound();
            return View(viewName, department);
        }

    }
}
