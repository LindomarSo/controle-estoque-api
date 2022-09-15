using Microsoft.AspNetCore.Identity;

namespace CasaAzul.Domain.Models.Identity
{
    public class User : IdentityUser<int>
    {
        public string NomeCompleto { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
