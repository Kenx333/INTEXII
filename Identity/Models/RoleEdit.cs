using Microsoft.AspNetCore.Identity;

namespace INTEXII.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<Customer> Members { get; set; }
        public IEnumerable<Customer> NonMembers { get; set; }
    }
}
