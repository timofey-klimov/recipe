using Recipes.Domain.Core;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Enums;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;

namespace Recipes.Domain.Entities
{
    public class RecipeCard : AggregateRoot
    {
        public static string EntityName => nameof(RecipeCard);

        public string Title { get; private set; }

        public string Remark { get; private set; }
        public DateTime CreateDate { get; private set; }

        public MealType MealType { get; private set; }
        public RecipeMainImage Image { get; private set; }

        private List<Hashtag>? _hashtags;
        public IReadOnlyCollection<Hashtag> Hashtags => _hashtags;

        private List<Ingredient> _ingredients;
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients;

        private List<CookingStage> _stages;
        public IReadOnlyCollection<CookingStage> Stages => _stages;

        private RecipeCard() { }

        protected RecipeCard(
            string title, string remark, MealType mealType, List<Hashtag>? hashtags)
        {
            Title = title;
            Remark = remark;
            MealType = mealType;
            _ingredients = new List<Ingredient>();
            _stages = new List<CookingStage>();
            _hashtags = hashtags;
        }
        
        public static Result<RecipeCard> Create(
            string title, string remark, byte mealType, List<string>? hashtags)
        {
            var tags = hashtags?.Select(tag => new Hashtag(tag)).ToList();
            return new RecipeCard(title, remark, (MealType)mealType, tags);
        }

        /// <summary>
        /// Добавить ингредиент в рецепт
        /// </summary>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Result<Ingredient> AddIngredient(string name, string quantity)
        {
            if (_ingredients.Any(x => x.Name == name && x.Quantity == quantity))
                return RecipeCardErrors.IngredientAlreadyExists(name, quantity);

            var ingredient = Ingredient.Create(name, quantity);
            if (ingredient.HasError)
                return ingredient.Error;
            _ingredients.Add(ingredient.Entity);

            return ingredient.Entity;
        }

        /// <summary>
        /// Добавить шаг приготовления
        /// </summary>
        /// <param name="description"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public Result<CookingStage> CreateRecipeStage(string description, CookingStageImage? image)
        {
            if (_stages.Any(x => x.Description == description))
                return RecipeCardErrors.CookingStageAlreadyExists();

            var stage = new CookingStage(this, image, description);
            _stages.Add(stage);
            return stage;
        }
    }   
}
