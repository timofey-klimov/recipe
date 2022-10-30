using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.Ingredients.Commands.AddIngredientsToRecipe;
using Recipes.Contracts.Ingredients;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    [Route("api/ingredients")]
    public class IngredientController : ApplicationController
    {
        public IngredientController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Добавление ингредиентов в рецепт
        /// </summary>
        /// <returns></returns>
        [HttpPost("addInRecipe/{recipeId}")]
        public async Task<Response<IReadOnlyCollection<IngredientDto>>> AddIngredientsToRecipe(
            [FromBody] RecipeIngredientsDto data, int recipeId, CancellationToken token)
        {
            return Created(await Mediator.Send(new AddIngredientsToRecipeCommand(data.Ingredients, recipeId), token));
        }
    }
}
