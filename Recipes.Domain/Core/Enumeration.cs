using System.Reflection;

namespace Recipes.Domain.Core
{
    public abstract class Enumeration<TEnum>
        where TEnum : Enumeration<TEnum>
    {
        private static readonly Dictionary<byte, TEnum> _enumerations = CreateEnumerations();

        public byte Value { get; }

        public string Name { get; }

        protected Enumeration(byte value, string name)
        {
            Value = value;
            Name = name;
        }

        public static TEnum? FromValue(byte value)
        {
            return _enumerations.TryGetValue(value, out var enumValue) ? enumValue : default;
        }

        private static Dictionary<byte, TEnum> CreateEnumerations()
        {
            var enumType = typeof(TEnum);

            return enumType.GetFields(
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(x => enumType.IsAssignableFrom(x.FieldType))
                .Select(x => (TEnum)x.GetValue(default)!)
                .ToDictionary(x => x.Value)!;
        }
    }
}
