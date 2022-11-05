using MediatR;
using Recipes.Application.Core.Auth;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Application.UseCases.RecipeCards.Commands.RemoveFromFavourites
{
    public class RemoveFromFavouritesHandler : IRequestHandler<RemoveFromFavouritesCommand>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFavouriteRecipeRepository _favouriteRecipeRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveFromFavouritesHandler(
            IRecipeCardRepository recipeCardRepository,
            IUserRepository userRepository,
            IFavouriteRecipeRepository favouriteRecipeRepository,
            ICurrentUserProvider currentUserProvider,
            IUnitOfWork unitOfWork)
        {
            _recipeCardRepository = recipeCardRepository;
            _userRepository = userRepository;
            _favouriteRecipeRepository = favouriteRecipeRepository;
            _currentUserProvider = currentUserProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveFromFavouritesCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeCardRepository.GetByIdAsync(request.RecipeId, cancellationToken);

            if (recipe is null)
                Guard.NotFound(RecipeCard.EntityName);

            var user = await _userRepository
                .GetByIdWithFavouriteRecipesAsync(_currentUserProvider.UserId!.Value, cancellationToken);

            if (user is null)
                Guard.NotFound(RecipeCard.EntityName);

            var favouriteRecipe = user.FavouriteRecipes.FirstOrDefault(x => x.RecipeId == recipe.Id);

            if (favouriteRecipe is null)
                Guard.NotFound(FavouriteRecipe.EntityName);

            if (favouriteRecipe.Dislike)
                Guard.ThrowBuisnessError(FavouriteRecipeErrors.RecipeWasRemovedFromFavourites());

            favouriteRecipe.RemoveFromFavourites();
            _favouriteRecipeRepository.Update(favouriteRecipe);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
