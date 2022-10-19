using AutoMapper;
using CasaAzul.Api.Services.Interfaces;
using CasaAzul.Api.ViewModels;
using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace CasaAzul.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(IRoleRepository roleRepository,
                           IMapper mapper,
                           UserManager<User> userManager,
                           RoleManager<Role> roleManager)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<RoleViewModel> CreateRoleAsync(RoleViewModel role)
        {
            var roleModel = await _roleManager.CreateAsync(new Role { Name = role.Name });

            if (roleModel.Succeeded)
                return new RoleViewModel { Name = role.Name};

            return null;
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRoles()
        {
            return _mapper.Map<IEnumerable<RoleViewModel>>(await _roleRepository.GetAllRoles());
        }

        public async Task<bool> UpdateUserRoleAsync(UpdateUserRoleViewModel role)
        {
            var user = await _userManager.FindByEmailAsync(role.Email);

            var tes = await _roleManager.FindByNameAsync(role.Role);

            if(user != null)
            {
                if (role.Delete)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, role.Role);
                }
                else
                    await _userManager.AddToRoleAsync(user, role.Role);
            }

            return true;
        }
    }
}
