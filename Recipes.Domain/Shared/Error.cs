namespace Recipes.Domain.Shared
{
    public struct Error
    {
        public string Code { get; }

        public string Description { get; }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
