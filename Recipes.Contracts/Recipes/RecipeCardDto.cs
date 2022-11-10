namespace Recipes.Contracts.Recipes
{
    public record RecipeCardDto(
        int? Id,
        string Title,
        string MealType,
        string CreatedAt,
        string ImageSource);
}
