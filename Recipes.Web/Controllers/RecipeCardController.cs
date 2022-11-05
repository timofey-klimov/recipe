using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.RecipeCards.Commands.AddToFavourite;
using Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard;
using Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeImage;
using Recipes.Application.UseCases.RecipeCards.Commands.RemoveFromFavourites;
using Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeCardDetails;
using Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeCards;
using Recipes.Application.UseCases.RecipeCards.Queries.GetRecipeImage;
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
            [FromBody] CreateRecipeCardDto recipeCardDto, CancellationToken token)
        {
            return Created(await Mediator.Send(
                new CreateRecipeCardCommand(
                    recipeCardDto.Title, recipeCardDto.Remark, recipeCardDto.MealType, recipeCardDto.Hashtags), cancellationToken: token)); 
        }

        /// <summary>
        /// Создание картинки рецепта
        /// </summary>
        /// <param name="data"></param>
        /// <param name="recipeId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("image/{recipeId}")]
        public async Task<IActionResult> CreateImage([FromForm] FileRequest data, int recipeId, CancellationToken token)
        {
            await Mediator.Send(new CreateRecipeImageCommand(data.File, recipeId), token);
            return Created();
            
        }

        /// <summary>
        /// Получение картинки рецепта
        /// </summary>
        /// <returns></returns>
        [HttpGet("image/{recipeId}")]
        public async Task<FileResult> GetImage(int recipeId, CancellationToken token)
        {
            var result = await Mediator.Send(new GetRecipeImageQuery(recipeId), token);
            return File(result.Content, result.ContentType);
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
    }
}
