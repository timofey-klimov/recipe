using MediatR;

namespace Recipes.Application.UseCases.RecipeCards.Commands.RemoveFromFavourites
{
    public record RemoveFromFavouritesCommand(int RecipeId): IRequest;
}
