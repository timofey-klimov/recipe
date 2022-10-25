using MediatR;
using Microsoft.AspNetCore.Http;
using Recipes.Contracts.Recipes;

namespace Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard
{
    public record CreateRecipeCardCommand(string Title, IFormFile Image) : IRequest<RecipeCardDto>;
}
