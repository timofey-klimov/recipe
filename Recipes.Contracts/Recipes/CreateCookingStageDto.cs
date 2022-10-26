using Microsoft.AspNetCore.Http;

namespace Recipes.Contracts.Recipes
{
    public record CreateCookingStageDto(string Description, IFormFile? Image);
}
