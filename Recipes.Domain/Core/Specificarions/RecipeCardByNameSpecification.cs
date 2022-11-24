using Recipes.Domain.Entities;
using System.Linq.Expressions;

namespace Recipes.Domain.Core.Specifications
{
    public class RecipeCardByNameSpecification : Specification<RecipeCard>
    {
        private readonly string? _name;
        public RecipeCardByNameSpecification(string? name)
        {
            _name = name;
        }
        public override Expression<Func<RecipeCard, bool>> Criteria()
        {
            if (string.IsNullOrEmpty(_name))
                return Empty();
            else
                return x => x.Title.Contains(_name);
        }
    }
}
