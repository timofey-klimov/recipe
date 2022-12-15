using Recipes.Domain.Core;

namespace Recipes.Domain.Entities
{
    public class Permission : AggregateRoot<int>
    {
        public string Name { get; private set; }

        public Permission(int id, string name)
            : base(id)
        {
            Name = name;
        }

        private Permission() { }
    }
}
