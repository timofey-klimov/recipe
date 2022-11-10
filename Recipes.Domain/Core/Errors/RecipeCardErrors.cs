using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class RecipeCardErrors
    {
        public static Error IngredientAlreadyExists(string name, string quantity) =>
            new Error("RecipeCard.IngredientAlreadyExists", $"Ingredient {name}:{quantity} already exists");

        public static Error CookingStageAlreadyExists() =>
            new Error("RecipeCard.CookingStageAlreadyExists", "Cooking stage already exists");

        public static Error ImageIsNotCreated() =>
            new Error("RecipeCard.ImageIsNotCreated", "Main recipe image is not created");

        public static Error IngredientsAreNotCreated() =>
            new Error("RecipeCard.IngredientAreNotCreated", "Ingredients are not created");

        public static Error StagesAreNotCreated() =>
            new Error("RecipeCard.StagesAreNotCreated", "Stages are not created");

        public static Error IncorrectMealType() =>
            new Error("RecipeCard.IncorrectMealType", "Mealtype not found");
    }
}
