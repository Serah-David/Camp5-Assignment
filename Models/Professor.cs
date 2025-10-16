using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAssignmentTwo.Models
{
    public class Professor
    {
        [Key]
        public int ProfessorId { get; set; }

        public int? ManagerId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be positive.")]
        [Required]
        public decimal? Salary { get; set; }

        [DataType(DataType.Date)]
        public DateTime? JoiningDate { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(10)]
        public string? Gender { get; set; }

        [ForeignKey("Department")]
        public int DeptNo { get; set; }

        public Department? Department { get; set; }
    }
}
