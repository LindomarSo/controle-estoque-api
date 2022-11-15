using CasaAzul.Domain.Models.Identity;

namespace CasaAzul.Domain.Interfaces
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string email);
    }
}
