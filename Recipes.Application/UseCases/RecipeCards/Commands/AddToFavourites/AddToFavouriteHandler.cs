using MediatR;
using Recipes.Application.Core.Auth;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.RecipeCards.Commands.AddToFavourite
{
    public class AddToFavouriteHandler : IRequestHandler<AddToFavouriteCommand>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        private readonly IFavouriteRecipeRepository _favouriteRecipeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserProvider _userProvider;
        private readonly IUnitOfWork _unitOfWork;
        public AddToFavouriteHandler(
            IRecipeCardRepository recipeCardRepository, 
            IFavouriteRecipeRepository favouriteRecipeRepository,
            ICurrentUserProvider currentUserProvider,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeCardRepository = recipeCardRepository;
            _favouriteRecipeRepository = favouriteRecipeRepository;
            _userProvider = currentUserProvider;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(AddToFavouriteCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeCardRepository.GetByIdAsync(request.RecipeId, cancellationToken);

            if (recipe is null)
                Guard.NotFound(RecipeCard.EntityName);

            var user = await _userRepository
                .GetByIdWithFavouriteRecipesAsync(_userProvider.UserId!.Value, cancellationToken);

            if (user is null)
                Guard.NotFound(User.EntityName);

            var favouriteRecipe =
                    user.FavouriteRecipes.FirstOrDefault(x => x.RecipeId == request.RecipeId);

            if (favouriteRecipe is not null)
            {
                if (!favouriteRecipe.Dislike)
                    Guard.ThrowBuisnessError(UserErrors.RecipeAlreadyAddedToFavourites());
                else
                    favouriteRecipe.AddToFavourites();

                _favouriteRecipeRepository.Update(favouriteRecipe);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }

            var entity = user.AddRecipeToFavourites(recipe!);
            _favouriteRecipeRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
