using MediatR;
using Recipes.Contracts;

namespace Recipes.Application.UseCases.Recipes.Queries.GetRecipeById
{
    public record GetRecipeByIdQuery(int Id) : IRequest<RecipeDto>;
}
