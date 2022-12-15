using Recipes.Application.Core.Identity;

namespace Recipes.Infrustructure.Identity
{
    public class GuidProvider : IGuidProvider
    {
        public Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}
