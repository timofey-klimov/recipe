namespace Recipes.Contracts
{
    public record RecipeDto(
        int? Id,
        string Title,
        string CookingProcess,
        IEnumerable<HashtagDto> HashTags, 
        IEnumerable<IngredientDto> Ingredients);
    
}
