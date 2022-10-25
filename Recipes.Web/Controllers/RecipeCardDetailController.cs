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

        [HttpPost("create/{recipeId}")]
        public async Task<Response<RecipeCardDetailsDto>> Create(
            [FromBody] RecipeCardDetailsDto cardInfo, int recipeId, CancellationToken token)
        {
            return Created(await Mediator.Send(
                new CreateRecipeCardDetailsCommand(cardInfo, recipeId), token));
        }
    }
}
