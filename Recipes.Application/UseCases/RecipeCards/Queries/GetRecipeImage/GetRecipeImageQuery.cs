using MediatR;

namespace Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeImage
{
    public record GetRecipeImageQuery(int RecipeId) : IRequest<(byte[] Content, string ContentType)>;
}
