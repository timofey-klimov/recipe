using Recipes.Domain.Core;
using Recipes.Domain.Enums;
using Recipes.Domain.ValueObjects;

namespace Recipes.Domain.Entities
{
    public class RecipeCardDetails : Entity
    {
        public static string EntityName => nameof(RecipeCardDetails);
        private RecipeCardDetails() { }

        internal RecipeCardDetails(RecipeCard card, string remark, MealType mealType, List<Hashtag>? hashtags, List<Ingredient> ingredients)
        {
            _hashtags = hashtags ?? new List<Hashtag>();
            _ingredients = ingredients;
            RecipeCardId = card.Id;
            MealType = mealType;
            Remark = remark;
        }
        public int RecipeCardId { get; private set; }

        public string Remark { get; private set; }

        public MealType MealType { get; private set; }

        private List<CookingStage> _stages;
        public IReadOnlyCollection<CookingStage> Stages => _stages;

        private List<Hashtag> _hashtags;
        public IReadOnlyCollection<Hashtag> Hashtags => _hashtags;

        private List<Ingredient> _ingredients;
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients;

        public CookingStage CreateCookingState(CookingStageImage? image, string description)
        {
            return new CookingStage(this, image, description);
        }

    }
}
