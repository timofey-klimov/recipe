using MediatR;

namespace Recipes.Application.UseCases.CookingStages.Queries
{
    public record GetStageImageQuery(int RecipeId, int StageId) 
        : IRequest<(byte[] Content, string ContentType)>;
}
