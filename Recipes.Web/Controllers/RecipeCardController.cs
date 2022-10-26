using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard;
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
        public async Task<Response<RecipeCardDto>> CreateRecipeCard([FromForm] CreateRecipeCardDto recipeCardDto, CancellationToken token)
        {
            return Created(await Mediator.Send(
                new CreateRecipeCardCommand(recipeCardDto.Title, recipeCardDto.Image), cancellationToken: token)); 
        }

        /// <summary>
        /// Получение пагинации карточек рецепта
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<PaginationResponse<RecipeCardDto>> GetRecipeCards(CancellationToken token) => 
            Pagination(await Mediator.Send(new GetRecipeCardsQuery(), token));
    }
}
