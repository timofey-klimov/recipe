using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.CookingStages.Commands.CreateCookingStage;
using Recipes.Contracts.Recipes;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    [Route("api/cooking-stages")]
    public class CookingStageController : ApplicationController
    {
        public CookingStageController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Создание шага рецепта
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="recipeCardId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("create/{recipeCardId}")]
        public async Task<Response<CookingStageDto>> Create(
            [FromForm] CreateCookingStageDto dto, int recipeCardId,CancellationToken token)
        {
            return Created(await Mediator.Send(new CreateCookingStageCommand(dto, recipeCardId), token));
        }
    }
}
