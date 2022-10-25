using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class RecipeCardDetailErrors
    {
        public static Error CantCreateRecipeCardWithExistringRecipeInfo() =>
            new Error("RecipeCardDetails.RecipeCardDetailsExists", "Recipe details already exists");
    }
}
