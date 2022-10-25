namespace Recipes.Contracts.Recipes
{
    public record RecipeCardDto(
        int Id,
        string Title,
        byte[] image
        );
    
}
