namespace Recipes.Shared
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str, true);
        }
    }
}
