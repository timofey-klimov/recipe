using Recipes.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain.Entities
{
    public class FavouriteRecipe : Entity
    {
        public int RecipeId { get; private set; }
        public RecipeCard Recipe { get; private set; }

        public int LikedBy { get; private set; }

        public DateTime LikeDate { get; private set; }

        private FavouriteRecipe() { }

        internal FavouriteRecipe(RecipeCard recipe, User user)
        {
            RecipeId = recipe.Id;
            LikedBy = user.Id;
            Recipe = recipe;
        }
    }
}
