using CasaAzul.Domain.Models.Identity;

namespace CasaAzul.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoles();
    }
}
