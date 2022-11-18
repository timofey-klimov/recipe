using MediatR;
using Recipes.Contracts.Recipes;
using Recipes.Domain.Repositories;

namespace Recipes.Application.UseCases.RecipeCards.Queries.GetBySearchString
{
    public class GetBySearchStringHandler : IRequestHandler<GetBySearchStringQuery, IReadOnlyCollection<RecipeCardDto>>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        public GetBySearchStringHandler(IRecipeCardRepository recipeCardRepository)
        {
            _recipeCardRepository = recipeCardRepository;
        }
        public async Task<IReadOnlyCollection<RecipeCardDto>> Handle(GetBySearchStringQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _recipeCardRepository
                .GetRecipesForQueryAsync(request.SearchQuery, cancellationToken);

            return recipes.Select(recipe => new RecipeCardDto(
                Id: recipe.Id,
                Title: recipe.Title,
                MealType: recipe.MealType.Name,
                CreatedAt: recipe.CreateDate.ToShortDateString(),
                ImageSource: recipe.ImageSource
                ))
                .ToList();
        }
    }
}
