using MediatR;
using Recipes.Contracts.Recipes;

namespace Recipes.Application.UseCases.RecipeCards.Queries.GetBySearchString
{
    public record GetBySearchStringQuery(string SearchQuery) : IRequest<IReadOnlyCollection<RecipeCardDto>>;
}
