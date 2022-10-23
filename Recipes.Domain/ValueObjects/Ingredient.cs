using Recipes.Domain.Core;
using Recipes.Domain.Shared;

namespace Recipes.Domain.ValueObjects
{
    public class Ingredient : ValueObject
    {
        private Ingredient() { }

        public Ingredient(string name, string quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string Name { get; private set; }

        public string Quantity { get; private set; }

        protected override IEnumerable<object> GetInternalValues()
        {
            yield return Name;
            yield return Quantity;
        }
    }
}
