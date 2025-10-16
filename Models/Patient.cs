using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAssignmentsOne.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required, StringLength(20)]
        public string RegistrationNo { get; set; }

        [Required, StringLength(50)]
        public string PatientName { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public string Gender { get; set; }

        public string? Address { get; set; }

        [Phone]
        public string? PhoneNo { get; set; }

        [ForeignKey("Membership")]
        public int MembershipId { get; set; }

        public Membership? Membership { get; set; }
    }
}
