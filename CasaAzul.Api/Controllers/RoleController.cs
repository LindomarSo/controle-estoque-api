using CasaAzul.Api.Services.Interfaces;
using CasaAzul.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasaAzul.Api.Controllers
{
    [Route("api/v1/role")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Lista todos os perfis 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _roleService.GetAllRoles());
        }

        /// <summary>
        /// Criar um novo perfil 
        /// </summary>
        /// <param name="roleView"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Post(RoleViewModel roleView)
        {
            if (string.IsNullOrEmpty(roleView.Name))
                return BadRequest("Preencha o nome do perfil");

            var result = await _roleService.CreateRoleAsync(roleView);

            if (result == null)
                return Unauthorized();

            return Created("", result);
        }

        /// <summary>
        /// Adiciona ou remove uma permissão de um usuário
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Put(UpdateUserRoleViewModel updateUser)
        {
            return Ok(await _roleService.UpdateUserRoleAsync(updateUser));
        }
    }
}
