using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.Recipes.Commands.CreateRecipe;
using Recipes.Application.UseCases.Recipes.Queries.GetRecipeById;
using Recipes.Contracts;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    [Route("api/recipes")]
    public class RecipeController : ApplicationController
    {
        public RecipeController(IMediator mediator)
            :base (mediator)
        {
        }

        [HttpPost("create")]
        public async Task<Response<RecipeDto>> CreateRecipe([FromBody]RecipeDto recipeDto, CancellationToken token)
        {
            return Created(await Mediator.Send(new CreateRecipeCommand(recipeDto), token));
        }

        [HttpGet("getById")]
        public async Task<Response<RecipeDto>> GetRecipeById(int id, CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetRecipeByIdQuery(id), token));
        }
    }
}
