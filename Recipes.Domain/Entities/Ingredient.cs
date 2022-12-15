using Recipes.Domain.Core;
using Recipes.Domain.Shared;

namespace Recipes.Domain.Entities
{
    public class Ingredient : AggregateRoot<int>
    {
        public string Name { get; private set; }

        public string Quantity { get; private set; }

        public int RecipeCardId { get; private set; }

        private Ingredient() { }
        protected Ingredient(string name, string quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public static Result<Ingredient> Create(string name, string quantity) =>
            new Ingredient(name, quantity);
    }
}
