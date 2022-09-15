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

        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUserNameAsync(userName);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar pegar o conta. Erro: {ex.Message}"
                );
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Rgister(UserUpdateViewModel userView)
        {
            try
            {
                if (await _accountService.UserExistAsync(userView.UserName))
                {
                    return BadRequest("O usuário já existe!");
                }

                var user = await _accountService.CreateAccountAsync(userView);

                return (user != null)
                                    ? Ok(new
                                    {
                                        UserName = user.UserName,
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
                    UserName = user.UserName,
                    PrimeiroNome = user.NomeCompleto,
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

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel userView)
        {
            try
            {
                if (userView.UserName != User.GetUserName())
                    return Unauthorized("Usuário inválido");

                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());

                if (user == null) return Unauthorized("Usuário inválido");

                var userRetorno = await _accountService.UpdateAccountAsync(userView);

                return (userRetorno != null)
                                    ? Ok(new
                                    {
                                        UserName = userRetorno.UserName,
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
