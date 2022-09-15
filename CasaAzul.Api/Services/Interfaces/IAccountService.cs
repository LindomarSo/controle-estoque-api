using CasaAzul.Api.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CasaAzul.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UserExistAsync(string username);
        Task<UserUpdateViewModel> GetUserByUserNameAsync(string username);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateViewModel userUpdateDto, string password);
        Task<UserUpdateViewModel> CreateAccountAsync(UserUpdateViewModel userDto);
        Task<UserUpdateViewModel> UpdateAccountAsync(UserUpdateViewModel userUpdateDto);
    }
}
