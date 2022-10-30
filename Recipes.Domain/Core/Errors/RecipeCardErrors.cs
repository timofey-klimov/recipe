using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class RecipeCardErrors
    {
        public static Error IngredientAlreadyExists(string name, string quantity) =>
            new Error("RecipeCard.IngredientAlreadyExists", $"Ingredient {name}:{quantity} already exists");

        public static Error CookingStageAlreadyExists() =>
            new Error("RecipeCard.CookingStageAlreadyExists", "Cooking stage already exists");
    }
}
