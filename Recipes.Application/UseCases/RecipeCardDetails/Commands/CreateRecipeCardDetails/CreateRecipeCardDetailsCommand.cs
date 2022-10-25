using MediatR;
using Recipes.Contracts.Recipes;

namespace Recipes.Application.UseCases.RecipeCardInfo.Commands.CreateRecipeCardDetails
{
    public record CreateRecipeCardDetailsCommand(RecipeCardDetailsDto RecipeInfo, int RecipeCardId) 
        : IRequest<RecipeCardDetailsDto>;
    
}
