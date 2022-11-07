using Recipes.Domain.Core;

namespace Recipes.Domain.Entities
{
    public class CookingStage : Entity
    {
        public static string EntityName => nameof(CookingStage);

        public string? ImageSource { get; private set; }
        public string Description { get; private set; }
        public int RecipeCardId { get; private set; }
        private CookingStage() { }
        internal CookingStage(RecipeCard recipeCard, string? imageSource, string description)
        {
            Description = description;
            RecipeCardId = recipeCard.Id;
            ImageSource = imageSource;
        }
    }
}
