using Recipes.Domain.Shared;

namespace Recipes.Domain.Core.Errors
{
    public static class FavouriteRecipeErrors
    {
        public static Error RecipeWasRemovedFromFavourites() =>
            new Error("FavouriteRecipe.IsInDislikeStatus", "Рецепт уже удален из избранного");
    }
}
