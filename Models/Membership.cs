using System.ComponentModel.DataAnnotations;

namespace MVCAssignmentsOne.Models
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }

        [Required, StringLength(50)]
        public string MembershipDesc { get; set; }

        [Required, Range(1000, 10000000)]
        public decimal InsuredAmount { get; set; }
    }
}
