using Recipes.Domain.Core.Services;
using System.Text;

namespace Recipes.Infrustructure.Security
{
    public class SaltGenerator : ISaltGenerator
    {
        public string Generate(string email, string login)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}{email}"));
        }
    }
}
