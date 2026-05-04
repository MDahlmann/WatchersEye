using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PoeLeagueTracker.Application.ServiceInterfaces;
using PoeLeagueTracker.Domain.Users;
using System.Security.Claims;
using System.Text;

namespace PoeLeagueTracker.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _settings;
        private readonly JsonWebTokenHandler _handler;

        public JwtTokenService(IOptions<JwtSettings> options, JsonWebTokenHandler handler)
        {
            _settings = options.Value;
            _handler = handler;
        }

        string IJwtTokenService.GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_settings.Secret);
            var symSecKey = new SymmetricSecurityKey(key);
            var signCredentials = new SigningCredentials(symSecKey, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> claims = [];
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

            var claimsIdentity = new ClaimsIdentity(claims);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = signCredentials
            };

            return _handler.CreateToken(descriptor);
        }
    }
}
