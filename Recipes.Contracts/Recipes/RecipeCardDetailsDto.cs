using Recipes.Contracts.CookingStages;
using Recipes.Contracts.Ingredients;

namespace Recipes.Contracts.Recipes
{
    public record RecipeCardDetailsDto(IEnumerable<IngredientDto> Ingredients, IEnumerable<CookingStageDto> Stages);
    
}
