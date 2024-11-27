
using System.ComponentModel.DataAnnotations;

namespace DemoDataAccessLayer.Models
{
    public class Department
    {
        public int Id { get; set; } // Pk
        [Range(0,500)]
        public int Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "Created At")]
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>(); 
    }
}
