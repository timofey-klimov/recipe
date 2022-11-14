using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class UserErrors
    {
        public static Error EmailAlreadyExists() => 
            new Error("User.EmailAlreadyExists", "Пользователь с таким email уже существует");

        public static Error LoginAlreadyExists() =>
            new Error("User.LoginAlreadyExists", "Пользователь с таким логином уже существует");

        public static Error LoginOrPasswordIsInvalid() =>
            new Error("User.LoginOrPasswordIsInvalid", "Не верный пользователь или пароль");

        public static Error RecipeAlreadyAddedToFavourites() =>
            new Error("User.RecipeAlreadyFavourite", "Рецепт уже добавлен в избранное");
    }
}
