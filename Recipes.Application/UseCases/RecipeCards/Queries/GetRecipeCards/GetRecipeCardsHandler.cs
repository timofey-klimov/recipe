using Recipes.Application.Core.Pagination;
using Recipes.Application.Shared.Extensions;
using Recipes.Application.Shared.Handlers;
using Recipes.Contracts.Recipes;
using Recipes.Contracts.Web;
using Recipes.Domain.Core.Specifications;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;

namespace Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeCards
{
    public class GetRecipeCardsHandler : GetCollectionRequestHandler<GetRecipeCardsQuery, RecipeCardDto, RecipeCard>
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        public GetRecipeCardsHandler(
            IPaginationProvider<RecipeCard> paginationProvider,
            IRecipeCardRepository recipeCardRepository) 
            : base(paginationProvider)
        {
            _recipeCardRepository = recipeCardRepository;
        }

        public override async Task<PaginationResponse<RecipeCardDto>> GetColllectionAsync(
                GetRecipeCardsQuery request, CancellationToken cancellationToken)
        {
            var spec = new RecipeCardByNameSpecification(request.Search);
            var itemsOnPage = await PaginationProvider.ItemsCount();
            var count = await _recipeCardRepository.CountAsync(spec, token: cancellationToken);
            var cards = await _recipeCardRepository.GetRecipesForPageAsync(request.Page, itemsOnPage, request.Search, cancellationToken);

            var recipeDtos = cards.Select(card => new RecipeCardDto(
                    Id: card.Id,
                    Title: card.Title,
                    MealType: card.MealType.Name,
                    CreatedAt: card.CreateDate.ToShortDateString(),
                    ImageSource: card.ImageSource))
                .ToList();


            return recipeDtos.ToPagination(GetTotalPages(count, itemsOnPage));
        }
    }
}
