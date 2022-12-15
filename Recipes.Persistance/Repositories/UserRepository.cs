using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;
using System.Collections.Immutable;

namespace Recipes.Persistance.Repositories
{
    public class UserRepository : AggregateRootRepository<User, int>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public async Task<User?> GetByIdWithFavouriteRecipesAsync(int id, CancellationToken token = default)
        {
            return await Entities()
                .Include(x => x.FavouriteRecipes)
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<HashSet<string>> GetPermissionsAsync(int id, CancellationToken token = default)
        {
            var user = await Entities()
                .Include(x => x.UserPermissions)
                    .ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(x => x.Id == id);

            var permissions = user.UserPermissions.Select(x => x.Permission).ToList();

            return permissions.Select(x => x.Name).ToHashSet();
        }


        public async Task<User?> GetUserByEmailOrLoginAsync(string? login, string? email, CancellationToken token = default)
        {
            return await Entities()
                .Where(x => (x.Login == login || x.Email == email)).FirstOrDefaultAsync(token);
        }


        public async Task<bool> IsUserEmailExistsAsync(string email, CancellationToken token = default)
        {
            return await Entities()
                .AnyAsync(x => x.Email == email, token);
        }

        public async Task<bool> IsUserLoginExistsAsync(string login, CancellationToken token = default)
        {
            return await Entities()
                .AnyAsync(x => x.Login == login, token);
        }
    }
}
