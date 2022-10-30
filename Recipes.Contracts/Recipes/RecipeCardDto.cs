namespace Recipes.Contracts.Recipes
{
    public record RecipeCardDto(
        int? Id,
        string Title,
        string Remark,
        byte MealType,
        string CreatedAt,
        IReadOnlyCollection<string>? hashtags
        );
    
}
