using MediatR;
using Recipes.Contracts.Recipes;

namespace Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeCardDetails
{
    public record GetRecipeCardDetailsQuery(int RecipeId) : IRequest<RecipeCardDetailsDto>;
}
