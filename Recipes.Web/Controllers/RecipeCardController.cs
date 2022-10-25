using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard;
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

        [HttpPost("create")]
        public async Task<Response<RecipeCardDto>> CreateRecipeCard([FromForm] CreateRecipeCardDto recipeCardDto, CancellationToken token)
        {
            return Created(await Mediator.Send(
                new CreateRecipeCardCommand(recipeCardDto.Title, recipeCardDto.Image), cancellationToken: token)); 
        }
    }
}
