using Recipes.Domain.Core;

namespace Recipes.Domain.Entities
{
    public class FavouriteRecipe : Entity<int>
    {
        public static string EntityName => nameof(FavouriteRecipe);
        public int RecipeId { get; private set; }
        public int LikedBy { get; private set; }

        public DateTime LikeDate { get; private set; }

        public bool Dislike { get; private set; }

        private FavouriteRecipe() { }

        internal FavouriteRecipe(RecipeCard recipe, User user)
        {
            RecipeId = recipe.Id;
            LikedBy = user.Id;
            Dislike = false;
        }

        public void AddToFavourites() => Dislike = false;

        public void RemoveFromFavourites() => Dislike = true;
    }
}
