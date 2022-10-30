using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.CookingStages.CreateCookingStage;
using Recipes.Contracts.CookingStages;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    [Route("api/stages")]
    public class CookingStageController : ApplicationController
    {
        public CookingStageController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Создание шага приготовления рецепта
        /// </summary>
        /// <param name="form"></param>
        /// <param name="recipeId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("create/{recipeId}")]
        public async Task<Response<CookingStageDto>> CreateStage(
            [FromForm] CreateCookingStageDto form, int recipeId, CancellationToken token)
        {
            return Created(await Mediator.Send(new CreateCookingStageCommand(form.Description, recipeId, form.Image), token));
        }
    }
}
