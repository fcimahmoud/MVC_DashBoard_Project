
namespace DemoPresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeRepository _employeeRepo;
        private IDepartmentRepository _departmentRepo;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            // ViewData => Dictionary<string,object>
            //ViewData["Message"] = "Hello From ViewData";

            // C# Feature ViewBag
            //ViewBag.Message = "Hello From ViewBag";

            var employees = _employeeRepo.GetAllWithDepartments();

            var employeesViewModel = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(employeesViewModel);
        }

        public IActionResult Create()
        {
            var departments = _departmentRepo.GetAll();
            SelectList listItems = new SelectList(departments , "Id", "Name");
            ViewBag.Departments = listItems;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            if (!ModelState.IsValid) return View(employeeVM);
            _employeeRepo.Create(employee);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details([FromRoute] int? id)
            => EmployeeControllerHandler(id, nameof(Details));

        public IActionResult Edit([FromRoute] int? id)
            => EmployeeControllerHandler(id , nameof(Edit));
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) return BadRequest();



            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
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
            return View(employeeVM);

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
            var employee = _employeeRepo.Get(id.Value);
            if (employee is null) return NotFound();

            #region Manual Mapping
            /*            var employeeVM = new EmployeeViewModel()
                        {
                            Address = employee.Address,
                            Department = employee.Department,
                            DepartmentId = employee.DepartmentId,
                            Name = employee.Name,
                            Id = employee.Id,
                            IsActive = employee.IsActive,
                            Phone = employee.Phone,
                            Salary = employee.Salary,
                            Age = employee.Age,
                            Email = employee.Email,
                        };*/
            #endregion

            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

            return View(viewName, employeeVM);
        }

    }
}
