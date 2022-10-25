using MediatR;
using Recipes.Contracts.Recipes;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;

namespace Recipes.Application.UseCases.RecipeCardInfo.Commands.CreateRecipeCardDetails
{
    public class CreateRecipeCardIDetailsHandler : IRequestHandler<CreateRecipeCardDetailsCommand, RecipeCardDetailsDto>
    {
        private readonly IRecipeCardIDetailRepository _recipeCardInfoRepository;
        private readonly IRecipeCardRepository _recipeCardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRecipeCardIDetailsHandler(
            IRecipeCardRepository recipeCardRepository,
            IRecipeCardIDetailRepository recipeCardInfoRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeCardInfoRepository = recipeCardInfoRepository;
            _recipeCardRepository = recipeCardRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RecipeCardDetailsDto> Handle(CreateRecipeCardDetailsCommand request, CancellationToken cancellationToken)
        {
            var recipeCard = await _recipeCardRepository.GetByIdWithDetailsAsync(request.RecipeCardId, cancellationToken);

            if (recipeCard is null)
                Guard.NotFound(RecipeCard.EntityName);

            var hashtags = request.RecipeInfo?.HashTags?.Select(hashtag => new Hashtag(hashtag)).ToList();
            var ingredients = request.RecipeInfo!.Ingredients.Select(x => new Ingredient(x.Name, x.Quantity)).ToList();

            var recipeInfoResult = recipeCard!.CreateRecipeDetails(
                request.RecipeInfo.CookingProcess,
                hashtags,
                ingredients);

            if (recipeInfoResult.HasError)
                Guard.ThrowBuisnessError(recipeInfoResult.Error);

            _recipeCardInfoRepository.Add(recipeInfoResult.Entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return request.RecipeInfo with { Id = recipeInfoResult.Entity.Id };
        }
    }
}
