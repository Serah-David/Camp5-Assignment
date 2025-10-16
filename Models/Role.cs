using System.ComponentModel.DataAnnotations;

namespace MVCAssignmentThree.Models
{
    public class Role
    {
        [Required(ErrorMessage = "Role is required.")]
        public int? RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
