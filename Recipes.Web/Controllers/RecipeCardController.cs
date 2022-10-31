using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard;
using Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeImage;
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
    }
}
