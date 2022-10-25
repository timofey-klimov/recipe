using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.RecipeCardInfo.Commands.CreateRecipeCardDetails;
using Recipes.Contracts.Recipes;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    [Route("api/recipe-details")]
    public class RecipeCardDetailController : ApplicationController
    {
        public RecipeCardDetailController(IMediator mediator)
            :base (mediator)
        {
        }

        /// <summary>
        /// Создание основной информации рецепта
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <param name="recipeId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("create/{recipeId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Response<RecipeCardDetailsDto>> Create(
            [FromBody] RecipeCardDetailsDto cardInfo, int recipeId, CancellationToken token)
        {
            return Created(await Mediator.Send(
                new CreateRecipeCardDetailsCommand(cardInfo, recipeId), token));
        }
    }
}
