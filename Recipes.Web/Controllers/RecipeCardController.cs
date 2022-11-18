using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.RecipeCards.Commands.AddToFavourite;
using Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard;
using Recipes.Application.UseCases.RecipeCards.Commands.RemoveFromFavourites;
using Recipes.Application.UseCases.RecipeCards.Queries.GetBySearchString;
using Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeCardDetails;
using Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeCards;
using Recipes.Contracts.Recipes;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    [Route("api/recipe-cards")]
    public class RecipeCardController : ApplicationController
    {
        public RecipeCardController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Создание карточки рецепта
        /// </summary>
        /// <param name="recipeCardDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [Authorize]
        [AllowAnonymous]
        public async Task<Response<RecipeCardDto>> CreateRecipeCard(
            [FromForm] CreateRecipeCardDto recipeCardDto, CancellationToken token)
        {
            return Created(await Mediator.Send(
                new CreateRecipeCardCommand(
                    recipeCardDto.Title, recipeCardDto.Remark, recipeCardDto.MealType, recipeCardDto.File), cancellationToken: token)); 
        }


        /// <summary>
        /// Получение страницы с рецептами
        /// </summary>
        /// <param name="token"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet("pages")]
        public async Task<PaginationResponse<RecipeCardDto>> GetRecipesPage(CancellationToken token, int pageNumber = 1)
        {
            return Pagination(await Mediator.Send(new GetRecipeCardsQuery(pageNumber), token));
        }

        /// <summary>
        /// Получение деталей рецепта
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("details/{recipeId}")]
        public async Task<Response<RecipeCardDetailsDto>> GetDetails(int recipeId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetRecipeCardDetailsQuery(recipeId), token));
        }

        /// <summary>
        /// Добавить рецепт в избранное
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("like/{recipeId}")]
        public async Task<IActionResult> AddToFavourite(int recipeId, CancellationToken token)
        {
            await Mediator.Send(new AddToFavouriteCommand(recipeId), token);
            return Ok();
        }

        /// <summary>
        /// Удалить из избранного
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("dislike/{recipeId}")]
        public async Task<IActionResult> RemoveFromFavourites(int recipeId, CancellationToken token)
        {
            await Mediator.Send(new RemoveFromFavouritesCommand(recipeId), token);

            return Ok();
        }

        /// <summary>
        /// Прлучение рецептов для поиска
        /// </summary>
        /// <param name="search"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<Response<IReadOnlyCollection<RecipeCardDto>>> GetBySearchString(string search, CancellationToken token)
        {
            if (string.IsNullOrEmpty(search))
                return OkCollection(new List<RecipeCardDto>());

            return OkCollection(await Mediator.Send(new GetBySearchStringQuery(search), token));
        }
    }
}
