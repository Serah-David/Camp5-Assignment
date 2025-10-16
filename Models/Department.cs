using System.ComponentModel.DataAnnotations;

namespace MVCAssignmentTwo.Models
{
    public class Department
    {
        [Key]
        public int DeptNo { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100)]
        public string DeptName { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100)]
        public string Location { get; set; }
    }
}
