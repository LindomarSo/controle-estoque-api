using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CasaAzul.Api.Extensions
{
    public static class AddJwtConfigurationExtensions
    {
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["tokenKey"])),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
        }
    }
}
