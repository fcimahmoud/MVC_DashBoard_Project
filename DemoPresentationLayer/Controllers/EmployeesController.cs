
using DemoPresentationLayer.Utilities;

namespace DemoPresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string? searchValue)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchValue))
                employees = _unitOfWork.Employees.GetAllWithDepartments();
            else
                employees = _unitOfWork.Employees.GetAll(searchValue);

            var employeesViewModel = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(employeesViewModel);
        }

        public IActionResult Create()
        {
            var departments = _unitOfWork.Departments.GetAll();
            SelectList listItems = new SelectList(departments , "Id", "Name");
            ViewBag.Departments = listItems;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid) return View(employeeVM);

            if (employeeVM.Image is not null)
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");

            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            _unitOfWork.Employees.Create(employee);
            _unitOfWork.SaveChanges();
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
                    if (employeeVM.Image is not null)
                        employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");

                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.Employees.Update(employee);
                    if (_unitOfWork.SaveChanges() > 0)
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
                    _unitOfWork.Employees.Delete(employee);
                    if (_unitOfWork.SaveChanges() > 0 && employee.ImageName is not null)
                        DocumentSettings.DeleteFile("Images", employee.ImageName);
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
                var departments = _unitOfWork.Departments.GetAll();
                SelectList listItems = new SelectList(departments, "Id", "Name");
                ViewBag.Departments = listItems;
            }
            if (!id.HasValue) return BadRequest();
            var employee = _unitOfWork.Employees.Get(id.Value);
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
