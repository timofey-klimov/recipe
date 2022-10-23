namespace Recipes.Contracts
{
    public record RecipeDto(
        int Id,
        string Title,
        string CookingProcess,
        IEnumerable<HashTagDto> HashTags, 
        IEnumerable<IngredientDto> Ingredients);
    
}
