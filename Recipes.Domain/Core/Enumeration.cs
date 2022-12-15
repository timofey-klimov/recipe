using System.Reflection;

namespace Recipes.Domain.Core
{
    public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
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

        public override bool Equals(object? obj)
        {
            if (obj is Enumeration<TEnum> en)
            {
                return Equals(en);
            }
            return false;
        }

        public bool Equals(Enumeration<TEnum>? other)
        {
            if (this.GetType() == other?.GetType())
            {
                return this.Value ==other.Value;
            }

            return false;
        }

        public static bool operator == (Enumeration<TEnum>? first, Enumeration<TEnum>? second)
        {
            if (first is null && second is null)
                return true;

            if (first is not null && second is null)
                return false;

            if (first is null && second is not null)
                return false;

            return first.Equals(second);
        }

        public static bool operator != (Enumeration<TEnum>? first, Enumeration<TEnum>? second)
        {
            return !(first == second);
        }
    }
}
