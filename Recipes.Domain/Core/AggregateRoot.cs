namespace Recipes.Domain.Core
{
    public abstract class AggregateRoot : Entity
    {
        private List<IDomainEvent> _events;
        public IReadOnlyCollection<IDomainEvent> Events => _events;

        public AggregateRoot()
        {
            _events = new List<IDomainEvent>();
        }

        protected void RaiseEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }
    }
}
