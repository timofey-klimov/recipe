using MediatR;

namespace Recipes.Application.UseCases.RecipeCards.Commands.AddToFavourite
{
    public record AddToFavouriteCommand(int RecipeId) : IRequest;
}
