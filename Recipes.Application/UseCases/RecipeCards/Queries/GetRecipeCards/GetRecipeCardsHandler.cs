using Recipes.Application.Core.Pagination;
using Recipes.Application.Shared.Extensions;
using Recipes.Application.Shared.Handlers;
using Recipes.Contracts.Recipes;
using Recipes.Contracts.Web;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;

namespace Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeCards
{
    public class GetRecipeCardsHandler : GetCollectionRequestHandler<GetRecipeCardsQuery, RecipeCardDto, RecipeCard>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        public GetRecipeCardsHandler(IRecipeCardRepository recipeCardRepository, IPaginationProvider<RecipeCard> paginationProvider)
            :base(paginationProvider)
        {
            _recipeCardRepository = recipeCardRepository;
        }

        public override async Task<PaginationResponse<RecipeCardDto>> GetColllectionAsync(GetRecipeCardsQuery request, CancellationToken cancellationToken)
        {
            var recipesCount = _recipeCardRepository.GetAll().Count();

            var itemsCount = PaginationProvider.ItemsCount();

            var recipes = _recipeCardRepository.GetAllWithIncludes("Image")
                .OrderBy(x => x.CreateDate)
                .Take(itemsCount)
                .ToList();

            return recipes.Select(recipe =>
                new RecipeCardDto(recipe.Id, recipe.Title, recipe.Image.Content))
                .ToPagination(recipesCount);
        }
    }
}
