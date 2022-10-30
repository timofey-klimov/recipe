using MediatR;
using Microsoft.AspNetCore.Http;
using Recipes.Contracts.CookingStages;

namespace Recipes.Application.UseCases.CookingStages.CreateCookingStage
{
    public record CreateCookingStageCommand(string Description, int RecipeId, IFormFile? Image) 
        : IRequest<CookingStageDto>;
}
