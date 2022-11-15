using AutoMapper;
using CasaAzul.Api.Services.Interfaces;
using CasaAzul.Api.ViewModels;
using CasaAzul.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CasaAzul.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _useManager;
        private readonly IMapper _mapper;
        private readonly SymmetricSecurityKey _Key;

        public TokenService(IConfiguration configuration,
                            UserManager<User> useManager,
                            IMapper mapper)
        {
            _configuration = configuration;
            _useManager = useManager;
            _mapper = mapper;
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["tokenKey"]));
        }
        public async Task<string> CreateToken(UserUpdateViewModel userUpdateDto)
        {
            var user = _mapper.Map<User>(userUpdateDto);

            // Claims são afirmações sobre o usuário por exemplo nome, email, id etc
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.NomeCompleto)
            };

            var roles = await _useManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
