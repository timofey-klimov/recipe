using MediatR;
using Microsoft.AspNetCore.Http;

namespace Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeImage
{
    public record CreateRecipeImageCommand(IFormFile File, int RecipeId) : IRequest;
}
