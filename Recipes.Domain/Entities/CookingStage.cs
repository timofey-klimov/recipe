using Recipes.Domain.Core;
using Recipes.Domain.ValueObjects;

namespace Recipes.Domain.Entities
{
    public class CookingStage : Entity
    {
        public static string EntityName => nameof(CookingStage);

        public CookingStageImage? Image { get; private set; }

        public string Description { get; private set; }

        public int RecipeDetailsId { get; private set; }

        private CookingStage() { }
        internal CookingStage(RecipeCardDetails details, CookingStageImage? image, string description)
        {
            Image = image;
            Description = description;
            RecipeDetailsId = details.Id;
        }
    }
}
