namespace Recipes.Domain.Core
{
    public abstract class Entity<T>
        where T : IEquatable<T>
    {
        protected Entity() { }

        protected Entity(T id)
        {
            Id = id;
        }

        public T Id { get; private set; }

        public override bool Equals(object? obj)
        {
            if (obj is Entity<T> e && e.GetType() == GetType())
            {
                return Id.Equals(e.Id);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<T>? first, Entity<T>? second)
        {
            if (first is null && second is null)
                return true;

            if (first is null && second is not null)
                return false;

            if (first is not null && second is null)
                return false;

            return first.Equals(second);
        }

        public static bool operator !=(Entity<T>? first, Entity<T>? second)
        {
            return !(first == second);
        }
    }
}
