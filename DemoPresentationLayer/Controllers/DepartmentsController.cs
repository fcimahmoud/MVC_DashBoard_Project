using DemoBusinessLogicLayer.Repositories;
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
        [HttpPost]
        public IActionResult Create(Department department)
        {
            // Server Side Validation
            if (!ModelState.IsValid) return View();
            _repository.Create(department);
            return RedirectToAction(nameof(Index));
        }
    }
}
