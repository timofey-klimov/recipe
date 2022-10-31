using MediatR;
using Recipes.Contracts.CookingStages;
using Recipes.Contracts.Ingredients;
using Recipes.Contracts.Recipes;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Domain.Shared;

namespace Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeCardDetails
{
    public class GetRecipeCardDetailsQueryHandler : IRequestHandler<GetRecipeCardDetailsQuery, RecipeCardDetailsDto>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        public GetRecipeCardDetailsQueryHandler(IRecipeCardRepository recipeCardRepository)
        {
            _recipeCardRepository = recipeCardRepository;
        }

        public async Task<RecipeCardDetailsDto> Handle(GetRecipeCardDetailsQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeCardRepository.GetByIdWithDetailsAsync(request.RecipeId, cancellationToken);

            if (recipe is null)
                Guard.NotFound(RecipeCard.EntityName);

            if (recipe.Ingredients?.Any() == false)
                Guard.ThrowBuisnessError(RecipeCardErrors.IngredientsAreNotCreated());

            if (recipe.Stages?.Any() == false)
                Guard.ThrowBuisnessError(RecipeCardErrors.StagesAreNotCreated());

            return new RecipeCardDetailsDto(
                Ingredients: recipe.Ingredients!.Select(i => new IngredientDto(i.Id, i.Name, i.Quantity)).ToArray(),
                Stages: recipe.Stages!.Select(s => new CookingStageDto(s.Id, s.Description)).ToArray());
        }
    }
}
