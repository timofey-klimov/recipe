using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("create/{recipeId}")]
        public async Task<Response<int>> CreateStage(
            [FromForm] CreateCookingStageDto form, int recipeId, CancellationToken token)
        {

        }
    }
}
