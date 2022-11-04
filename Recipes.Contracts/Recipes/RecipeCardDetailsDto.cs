using Recipes.Contracts.CookingStages;
using Recipes.Contracts.Ingredients;

namespace Recipes.Contracts.Recipes
{
    public record RecipeCardDetailsDto(
        string Remark,
        IEnumerable<HashtagDto> Hashtags,
        IEnumerable<IngredientDto> Ingredients, 
        IEnumerable<CookingStageDto> Stages);
    
}
