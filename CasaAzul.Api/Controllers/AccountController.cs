using CasaAzul.Api.Extensions;
using CasaAzul.Api.Services.Interfaces;
using CasaAzul.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasaAzul.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Pega o usuário logado 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var email = User.GetUserEmail();
                var user = await _accountService.GetUserByUserNameAsync(email);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar pegar o conta. Erro: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Cria um novo usuário 
        /// </summary>
        /// <param name="userView"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous] // TODO REMOVER 
        public async Task<IActionResult> Rgister(UserUpdateViewModel userView)
        {
            try
            {
                if (await _accountService.UserExistAsync(userView.Email))
                {
                    return BadRequest("O usuário já existe!");
                }

                var user = await _accountService.CreateAccountAsync(userView);

                return (user != null)
                                    ? Ok(new
                                    {
                                        UserName = user.Email,
                                        PrimeiroNome = user.NomeCompleto,
                                        token = _tokenService.CreateToken(user).Result
                                    })
                                    : BadRequest("Usuário não criado tente novamente mais tarde");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar cadastrar o usuário. Erro: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Faz login na plataforma 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(login.Username);
                if (user == null)
                    return Unauthorized("Usuário ou senha inválido!");

                var result = await _accountService.CheckUserPasswordAsync(user, login.Password);
                if (!result.Succeeded)
                {
                    return Unauthorized();
                }

                return Ok(new
                {
                    UserName = user.Email,
                    NomeCompleto = user.NomeCompleto,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar o login. Erro: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Atualiza informações do usuário
        /// </summary>
        /// <param name="userView"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel userView)
        {
            try
            {
                if (userView.Email != User.GetUserEmail())
                    return Unauthorized("Usuário inválido");

                var user = await _accountService.GetUserByUserNameAsync(User.GetUserEmail());

                if (user == null) return Unauthorized("Usuário inválido");

                var userRetorno = await _accountService.UpdateAccountAsync(userView);

                return (userRetorno != null)
                                    ? Ok(new
                                    {
                                        UserName = userRetorno.Email,
                                        PrimeiroNome = userRetorno.NomeCompleto,
                                        token = _tokenService.CreateToken(userRetorno).Result
                                    })
                                    : NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar o usuário. Erro: {ex.Message}"
                );
            }
        }
    }
}
