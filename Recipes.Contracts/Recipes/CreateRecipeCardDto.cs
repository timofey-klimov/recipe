namespace Recipes.Contracts.Recipes
{
    public record CreateRecipeCardDto(string Title, string Remark, byte MealType, IEnumerable<string>? Hashtags);
    
}
