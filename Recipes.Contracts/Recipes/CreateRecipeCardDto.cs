using Microsoft.AspNetCore.Http;

namespace Recipes.Contracts.Recipes
{
    public record CreateRecipeCardDto(string Title, string Remark, byte MealType, IFormFile File);
    
}
