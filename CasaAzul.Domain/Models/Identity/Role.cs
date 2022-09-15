using Microsoft.AspNetCore.Identity;

namespace CasaAzul.Domain.Models.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
