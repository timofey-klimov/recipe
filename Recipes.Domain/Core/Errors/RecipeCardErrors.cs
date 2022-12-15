using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class RecipeCardErrors
    {
        public static Error IngredientAlreadyExists(string name, string quantity) =>
            new Error("RecipeCard.IngredientAlreadyExists", "Ингредиенты повторяются");

        public static Error CookingStageAlreadyExists() =>
            new Error("RecipeCard.CookingStageAlreadyExists", "Шаги приготовления повторяются");

        public static Error ImageIsNotCreated() =>
            new Error("RecipeCard.ImageIsNotCreated", "Картинка рецепта не создана");

        public static Error IngredientsAreNotCreated() =>
            new Error("RecipeCard.IngredientAreNotCreated", "Ингредиенты не созданы");

        public static Error StagesAreNotCreated() =>
            new Error("RecipeCard.StagesAreNotCreated", "Шаги приготовления не созданы");

        public static Error IncorrectMealType() =>
            new Error("RecipeCard.IncorrectMealType", "Тип рецепта не верный");

        public static Error InvalidRequestStatus() =>
            new Error("RecipeCard.InvalidRequestStatus", "Не валидный статус запроса на подтверждение");
    }
}
