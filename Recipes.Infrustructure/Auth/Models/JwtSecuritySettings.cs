using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Recipes.Infrustructure.Auth.Models
{
    public class JwtSecuritySettings
    {
        public string SecurityKey { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }


        public SymmetricSecurityKey GetSymmetricKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
    }
}
