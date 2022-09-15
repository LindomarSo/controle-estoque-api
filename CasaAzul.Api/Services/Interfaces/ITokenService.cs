using CasaAzul.Api.ViewModels;

namespace CasaAzul.Api.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateViewModel userUpdate);
    }
}
