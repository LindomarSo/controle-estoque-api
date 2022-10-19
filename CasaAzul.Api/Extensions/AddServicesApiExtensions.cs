using CasaAzul.Api.Services;
using CasaAzul.Api.Services.CasaAzul;
using CasaAzul.Api.Services.CasaAzul.Interfaces;
using CasaAzul.Api.Services.Interfaces;

namespace CasaAzul.Api.Extensions
{
    public static class AddServicesApiExtensions
    {
        public static void AddServiceApi(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEntidadeService, EntidadeService>();
            services.AddScoped<IDoacaoService, DoacaoService>();
        }
    }
}
