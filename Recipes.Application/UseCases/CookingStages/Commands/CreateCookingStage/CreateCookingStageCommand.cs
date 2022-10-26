using MediatR;
using Recipes.Contracts.Recipes;

namespace Recipes.Application.UseCases.CookingStages.Commands.CreateCookingStage
{
    public record CreateCookingStageCommand(CreateCookingStageDto Stage, int RecipeCardId) : IRequest<CookingStageDto>;
}
