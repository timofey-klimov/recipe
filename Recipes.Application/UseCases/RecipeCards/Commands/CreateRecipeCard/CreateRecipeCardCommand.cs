using MediatR;
using Microsoft.AspNetCore.Http;
using Recipes.Contracts.Recipes;

namespace Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard
{
    public record CreateRecipeCardCommand(string Title, string Remark, byte MealType, IFormFile File) 
        : IRequest<RecipeCardDto>;
}
