using Recipes.Domain.Core;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Shared;
using Recipes.Domain.ValueObjects;

namespace Recipes.Domain.Entities
{
    public class RecipeCard : AggregateRoot
    {
        public static string EntityName => nameof(RecipeCard);

        public string Title { get; private set; }

        public DateTime CreateDate { get; private set; }

        public RecipeMainImage Image { get; private set; }

        public RecipeCardDetails Details { get; private set; }

        private RecipeCard() { }

        protected RecipeCard(string title, RecipeMainImage image)
        {
            Title = title;
            Image = image;
        }

        public static Result<RecipeCard> Create(string title, RecipeMainImage image)
        {
            return new RecipeCard(title, image);
        }

        public Result<RecipeCardDetails> CreateRecipeDetails(string cookingProccess, List<Hashtag> hashTags, List<Ingredient> ingredients)
        {
            if (this.Details != null)
                return RecipeCardDetailErrors.CantCreateRecipeCardWithExistringRecipeInfo();

            var details = new RecipeCardDetails(this, cookingProccess, hashTags, ingredients);
            Details = details;

            return details;
        }
    }
}
