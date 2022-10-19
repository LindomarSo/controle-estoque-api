using CasaAzul.Api.ViewModels;

namespace CasaAzul.Api.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetAllRoles();
        Task<RoleViewModel> CreateRoleAsync(RoleViewModel role);
        Task<bool> UpdateUserRoleAsync(UpdateUserRoleViewModel role);
    }
}
