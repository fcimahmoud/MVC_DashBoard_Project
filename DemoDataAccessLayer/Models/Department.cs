
using System.ComponentModel.DataAnnotations;

namespace DemoDataAccessLayer.Models
{
    public class Department
    {
        public int Id { get; set; } // Pk
        public int Code { get; set; }
        public string Name { get; set; }
        [Display(Name = "Created At")]
        public DateTime DateOfCreation { get; set; }
    }
}
