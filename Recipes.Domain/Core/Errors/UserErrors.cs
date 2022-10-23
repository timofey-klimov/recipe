using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class UserErrors
    {
        public static Error EmailAlreadyExists() => 
            new Error("User.EmailAlreadyExists", "User with such email already exists");

        public static Error LoginAlreadyExists() =>
            new Error("User.LoginAlreadyExists", "User with such login already exists");
    }
}
