namespace Recipes.Domain.Core
{
    public abstract class Entity
    {
        public int Id { get; private set; }

        public override bool Equals(object? obj)
        {
            if (obj is Entity e && e.GetType() == GetType())
            {
                return Id == e.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity first, Entity second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Entity first, Entity second)
        {
            return !(first == second);
        }
    }
}
