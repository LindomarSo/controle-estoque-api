using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Identity;
using CasaAzul.Infra.Context;
using CasaAzul.Infra.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CasaAzul.Api.Extensions
{
    public static class AddRepositoryExtensions
    {
        public static void AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EntityContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DbContext"));
            });

            services.AddIdentityCore<User>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleValidator<RoleValidator<Role>>()
            .AddEntityFrameworkStores<EntityContext>()
            .AddDefaultTokenProviders(); // Para gerar e atualizar o token

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
