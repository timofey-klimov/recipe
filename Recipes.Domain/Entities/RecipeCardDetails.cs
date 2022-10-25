using Recipes.Domain.Core;
using Recipes.Domain.ValueObjects;

namespace Recipes.Domain.Entities
{
    public class RecipeCardDetails : Entity
    {
        public static string EntityName => nameof(RecipeCardDetails);
        private RecipeCardDetails() { }

        internal RecipeCardDetails(RecipeCard card, string cookingProcess, List<Hashtag>? hashtags, List<Ingredient> ingredients)
        {
            _hashtags = hashtags ?? new List<Hashtag>();
            _ingredients = ingredients;
            CookingProcess = cookingProcess;
            RecipeCardId = card.Id;
        }

        public int RecipeCardId { get; private set; }
        public string CookingProcess { get; private set; }

        private List<Hashtag> _hashtags;
        public IReadOnlyCollection<Hashtag> Hashtags => _hashtags;

        private List<Ingredient> _ingredients;
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients;

    }
}
