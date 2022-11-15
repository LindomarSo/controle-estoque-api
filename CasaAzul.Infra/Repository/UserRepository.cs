using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Identity;
using CasaAzul.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CasaAzul.Infra.Repository
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        private readonly EntityContext _context;

        public UserRepository(EntityContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(
                user => user.Email == email.ToLower()
            );
        }
    }
}
