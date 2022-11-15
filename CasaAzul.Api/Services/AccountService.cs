using AutoMapper;
using CasaAzul.Api.Services.Interfaces;
using CasaAzul.Api.ViewModels;
using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Identity;
using CasaAzul.Domain.Uow;
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
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<User> useManager,
                              SignInManager<User> signInManager,
                              IMapper mapper,
                              IUserRepository userRepository,
                              IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _useManager = useManager;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateViewModel userView, string password)
        {
            try
            {
                var user = await _useManager.Users
                                            .SingleOrDefaultAsync(user => user.Email == userView.Email.ToLower());

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
                user.Email = user.Email.ToLower();
                user.UserName = user.Email;
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

        public async Task<UserUpdateViewModel> GetUserByUserNameAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByUserNameAsync(email);

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
                var user = await _userRepository.GetUserByUserNameAsync(userView.Email);

                if (user is null) return null;

                userView.Id = user.Id;

                _mapper.Map(userView, user);

                if (userView.Password != null)
                {
                    var token = await _useManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _useManager.ResetPasswordAsync(user, token, userView.Password);
                }

                _userRepository.Update(user);

                await _unitOfWork.CommitAsync();
                return _mapper.Map<UserUpdateViewModel>(await _userRepository.GetUserByUserNameAsync(user.UserName));
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar o usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExistAsync(string email)
        {
            try
            {
                return await _useManager.Users
                                            .AnyAsync(user => user.Email == email.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao veficar existência do usuário. Erro: {ex.Message}");
            }
        }
    }
}
