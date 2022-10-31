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
            var itemsOnPage = await PaginationProvider.ItemsCount();
            var count = await _recipeCardRepository.CountAsync(cancellationToken);
            var cards = await _recipeCardRepository.GetRecipesForPageAsync(request.Page, itemsOnPage, cancellationToken);

            var recipeDtos = cards.Select(card => new RecipeCardDto(
                    Id: card.Id,
                    Title: card.Title,
                    Remark: card.Remark,
                    MealType: (byte)card.MealType,
                    CreatedAt: card.CreateDate.ToShortDateString(),
                    Hashtags: card.Hashtags.Select(hashtag => hashtag.Title)
                    .ToList()))
                .ToList();

            return recipeDtos.ToPagination(count);
        }
    }
}
