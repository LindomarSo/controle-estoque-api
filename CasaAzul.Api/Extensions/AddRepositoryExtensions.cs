using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Identity;
using CasaAzul.Domain.Uow;
using CasaAzul.Infra.Context;
using CasaAzul.Infra.Repository;
using CasaAzul.Infra.Uow;
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
            .AddEntityFrameworkStores<EntityContext>()
            .AddRoleManager<RoleManager<Role>>()
            .AddRoleValidator<RoleValidator<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders(); // Para gerar e atualizar o token

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IEntidadeRepository, EntidadeRepository>();
            services.AddScoped<IDoacaoRepository, DoacaoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
