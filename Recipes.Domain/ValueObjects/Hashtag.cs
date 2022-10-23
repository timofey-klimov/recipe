using Recipes.Domain.Core;

namespace Recipes.Domain.ValueObjects
{
    public class Hashtag : ValueObject
    {
        public string Title { get; private set; }

        private Hashtag() { }

        public Hashtag(string title) => Title = title;

        protected override IEnumerable<object> GetInternalValues()
        {
            yield return Title;
        }
    }
}
