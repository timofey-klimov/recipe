using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.Recipes.Commands.CreateRecipe;
using Recipes.Contracts;

namespace Recipes.Web.Controllers
{
    [Route("api/recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody]RecipeDto recipeDto)
        {
            return Ok(await _mediator.Send(new CreateRecipeCommand(recipeDto)));
        }
    }
}
