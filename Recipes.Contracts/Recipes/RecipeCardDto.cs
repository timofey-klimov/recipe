namespace Recipes.Contracts.Recipes
{
    public record RecipeCardDto(
        int? Id,
        string Title,
        byte MealType,
        string CreatedAt,
        string ImageSource);
}
