using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Recipes.Application.Core.Auth;
using Recipes.Domain.Entities;
using Recipes.Infrustructure.Auth.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Recipes.Infrustructure.Auth
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtSecuritySettings _settings;
        public JwtTokenProvider(IOptions<JwtSecuritySettings> settings)
        {
            _settings = settings.Value;
        }
        public string CreateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var signingCredintials = new SigningCredentials(
                _settings.GetSymmetricKey(), SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
            };

            var identity = new ClaimsIdentity(claims);

            var token = handler.CreateJwtSecurityToken(subject: identity,
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                expires: DateTime.UtcNow.AddDays(360),
                signingCredentials: signingCredintials);

            return handler.WriteToken(token);
        }
    }
}
