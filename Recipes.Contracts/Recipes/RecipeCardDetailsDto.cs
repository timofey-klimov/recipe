namespace Recipes.Contracts.Recipes
{
    public record RecipeCardDetailsDto(
        int? Id,
        string Remark,
        byte MealType,
        IEnumerable<string> HashTags, 
        IEnumerable<IngredientDto> Ingredients);
    
}
