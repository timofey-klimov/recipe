namespace Recipes.Domain.Core
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetInternalValues();

        public override bool Equals(object? obj)
        {
            if (obj is ValueObject vo && this.GetType() == vo.GetType())
            {
                return Enumerable.SequenceEqual(GetInternalValues(), vo.GetInternalValues());
            }

            return false;
        }

        public override int GetHashCode()
        {
            return GetInternalValues().Aggregate(1, (hash, current) => hash * current.GetHashCode());
        }

        public static bool operator ==(ValueObject first, ValueObject second) => first.Equals(second);

        public static bool operator !=(ValueObject first, ValueObject second) => !(first == second);
    }
}
