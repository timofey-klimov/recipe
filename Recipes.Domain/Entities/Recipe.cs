using Recipes.Domain.Core;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;

namespace Recipes.Domain.Entities
{
    public class Recipe : AggregateRoot
    {
        public static string EntityName => nameof(Recipe);
        private Recipe() { }

        protected Recipe(string title, string cookingProcess, List<Hashtag> hashtags, List<Ingredient> ingredients)
        {
            Title = title;
            _hashtags = hashtags;
            _ingredients = ingredients;
            CookingProcess = cookingProcess;
        }

        public static Result<Recipe> Create(string title, string cookingProcess, List<Hashtag>? hashtags, List<Ingredient> ingredients)
        {
            hashtags ??= new List<Hashtag>();
            return new Recipe(title, cookingProcess, hashtags, ingredients);
        }

        public string Title { get; private set; }
        public string CookingProcess { get; private set; }

        private List<Hashtag> _hashtags;
        public IReadOnlyCollection<Hashtag> Hashtags => _hashtags;

        private List<Ingredient> _ingredients;
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients;
    }
}
