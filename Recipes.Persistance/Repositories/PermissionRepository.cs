using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Persistance.Repositories
{
    public class PermissionRepository : AggregateRootRepository<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public async Task<Permission?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await Entities().SingleOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
    }
}
