using AutoMapper;
using CasaAzul.Api.Services.Interfaces;
using CasaAzul.Api.ViewModels;
using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CasaAzul.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _useManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AccountService(UserManager<User> useManager,
                              SignInManager<User> signInManager,
                              IMapper mapper,
                              IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _signInManager = signInManager;
            _useManager = useManager;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateViewModel userView, string password)
        {
            try
            {
                var user = await _useManager.Users
                                            .SingleOrDefaultAsync(user => user.UserName == userView.UserName.ToLower());

                // O parâmetro booleano é passado como falso para que ele não bloquei o usuário caso não bata a senha.
                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar a senha do usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateViewModel> CreateAccountAsync(UserUpdateViewModel userView)
        {
            try
            {
                var user = _mapper.Map<User>(userView);
                var result = await _useManager.CreateAsync(user, userView.Password);

                if (result.Succeeded)
                {
                    var returnDto = _mapper.Map<UserUpdateViewModel>(user);
                    return returnDto;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar criar conta. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateViewModel> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await _userRepository.GetUserByUserNameAsync(userName);

                if (user == null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateViewModel>(user);
                return userUpdateDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar buscar username. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateViewModel> UpdateAccountAsync(UserUpdateViewModel userView)
        {
            try
            {
                var user = await _userRepository.GetUserByUserNameAsync(userView.UserName);

                if (user is null) return null;

                userView.Id = user.Id;

                _mapper.Map(userView, user);

                if (userView.Password != null)
                {
                    var token = await _useManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _useManager.ResetPasswordAsync(user, token, userView.Password);
                }

                _userRepository.Update<User>(user);

                if (await _userRepository.SaveChangesAsync())
                {
                    return _mapper.Map<UserUpdateViewModel>(await _userRepository.GetUserByUserNameAsync(user.UserName));
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar o usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExistAsync(string username)
        {
            try
            {
                return await _useManager.Users
                                            .AnyAsync(user => user.UserName == username.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao veficar existência do usuário. Erro: {ex.Message}");
            }
        }
    }
}
