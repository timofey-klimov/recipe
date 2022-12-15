using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Application.Core.Identity
{
    public interface IGuidProvider
    {
        Guid Create();
    }
}
