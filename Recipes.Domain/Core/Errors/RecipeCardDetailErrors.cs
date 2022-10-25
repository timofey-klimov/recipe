using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class RecipeCardDetailErrors
    {
        public static Error CantCreateRecipeCardWithExistringRecipeInfo() =>
            new Error("RecipeCardInfo.RecipeCardInfoExists", "Recipe info already exists");
    }
}
