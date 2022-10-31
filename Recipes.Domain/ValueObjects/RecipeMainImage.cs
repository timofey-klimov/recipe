using Recipes.Domain.Core;

namespace Recipes.Domain.ValueObjects
{
    public class RecipeMainImage : ValueObject
    {
        private RecipeMainImage() { }

        internal RecipeMainImage(byte[] content, string contentType, long size, string? fileName = null)
        {
            Content = content;
            ContentType = contentType;
            Size = size;
            FileName = fileName;
        }

        public byte[] Content { get; private set; }

        public string ContentType { get; private set; }

        public string? FileName { get; private set; }

        public long Size { get; private set; }

        protected override IEnumerable<object> GetInternalValues()
        {
            yield return Content;
            yield return ContentType;
            yield return Size;
        }
    }
}
