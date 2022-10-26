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

        public static bool operator ==(Entity? first, Entity? second)
        {
            if (first is null && second is null)
                return true;

            if (first is null && second is not null)
                return false;

            if (first is not null && second is null)
                return false;

            return first.Equals(second);
        }

        public static bool operator !=(Entity first, Entity second)
        {
            return !(first == second);
        }
    }
}
