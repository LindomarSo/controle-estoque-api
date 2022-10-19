using CasaAzul.Api.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CasaAzul.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UserExistAsync(string email);
        Task<UserUpdateViewModel> GetUserByUserNameAsync(string email);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateViewModel userUpdateDto, string password);
        Task<UserUpdateViewModel> CreateAccountAsync(UserUpdateViewModel userDto);
        Task<UserUpdateViewModel> UpdateAccountAsync(UserUpdateViewModel userUpdateDto);
    }
}
