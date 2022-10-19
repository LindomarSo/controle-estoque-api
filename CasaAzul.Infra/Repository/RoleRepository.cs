using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Identity;
using CasaAzul.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CasaAzul.Infra.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EntityContext _context;

        public RoleRepository(EntityContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
