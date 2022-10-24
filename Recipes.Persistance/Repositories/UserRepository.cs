using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.Repositories
{
    public class UserRepository : AggregateRootRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public async Task<bool> IsUserEmailExistsAsync(string email, CancellationToken token = default)
        {
            return await DbContext.Set<User>().AnyAsync(x => x.Email == email, token);
        }

        public async Task<bool> IsUserLoginExistsAsync(string login, CancellationToken token = default)
        {
            return await DbContext.Set<User>().AnyAsync(x => x.Login == login, token);
        }
    }
}
