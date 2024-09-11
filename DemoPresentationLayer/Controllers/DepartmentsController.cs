using DemoBusinessLogicLayer.Interfaces;
using DemoDataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoPresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _repository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Retrieve All Departments
            var departments = _repository.GetAll();
            return View(departments);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            // Server Side Validation
            if (!ModelState.IsValid) return View();
            _repository.Create(department);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) 
            => DepartmentControllerHandler(id, nameof(Details));

        public IActionResult Edit(int? id) 
            => DepartmentControllerHandler(id, nameof(Edit));
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if(id != department.Id) return BadRequest();
             
            if(ModelState.IsValid)
            {
                try
                {
                    _repository.Update(department);
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

        public IActionResult Delete(int? id) 
            => DepartmentControllerHandler(id, nameof(Delete));
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Delete(department);
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

        public IActionResult DepartmentControllerHandler(int? id, string viewName)
        {
            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);
            if (department is null) return NotFound();
            return View(viewName, department);
        }
    }
}
