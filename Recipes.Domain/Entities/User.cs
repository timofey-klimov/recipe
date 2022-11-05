using Recipes.Domain.Core;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Core.Services;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Domain.Entities
{
    public class User : AggregateRoot
    {
        private string _salt;
        public static string EntityName => nameof(User);

        private List<FavouriteRecipe> _favouriteRecipes;
        public IReadOnlyCollection<FavouriteRecipe> FavouriteRecipes => _favouriteRecipes.AsReadOnly();

        private User() { }

        protected User(string login, string email, string password, string salt)
        {
            Login = login;
            Email = email;
            Password = password;
            _salt = salt;
            _favouriteRecipes = new List<FavouriteRecipe>();
        }
        public string Login { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public DateTime CreateDate { get; private set; }

        public static async Task<Result<User>> CreateAsync(string login, 
            string email, 
            string password, 
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher,
            ISaltGenerator saltGenerator)
        {
            if (await userRepository.IsUserEmailExistsAsync(email))
                return UserErrors.EmailAlreadyExists();

            if (await userRepository.IsUserLoginExistsAsync(login))
                return UserErrors.LoginAlreadyExists();

            var salt = saltGenerator.Generate(email, login);
            var hashedPassword = passwordHasher.Hash(password, salt);

            return new User(login, email, hashedPassword, salt);
        }

        public Result CheckUserPassword(string passwordForCheck, IPasswordHasher passwordHasher)
        {
            var hashedPassword = passwordHasher.Hash(passwordForCheck, _salt);

            if (hashedPassword != Password)
                return UserErrors.LoginOrPasswordIsInvalid();

            return Result.Success();
        }
        
        public FavouriteRecipe AddRecipeToFavourites(RecipeCard recipe)
        {
            var favourite = new FavouriteRecipe(recipe, this);
            _favouriteRecipes.Add(favourite);
            return favourite;
        }
    }
}
