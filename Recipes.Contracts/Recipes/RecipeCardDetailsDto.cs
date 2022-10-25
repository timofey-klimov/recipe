namespace Recipes.Contracts.Recipes
{
    public record RecipeCardDetailsDto(
        int? Id,
        string CookingProcess,
        IEnumerable<string> HashTags, 
        IEnumerable<IngredientDto> Ingredients);
    
}
