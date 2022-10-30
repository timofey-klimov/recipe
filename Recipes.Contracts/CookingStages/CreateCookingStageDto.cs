using Microsoft.AspNetCore.Http;

namespace Recipes.Contracts.CookingStages
{
    public record CreateCookingStageDto(string Description, IFormFile? Image);
}
