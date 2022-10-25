using Microsoft.AspNetCore.Http;

namespace Recipes.Contracts.Recipes
{
    public record CreateRecipeCardDto(string Title, IFormFile Image);
    
}
