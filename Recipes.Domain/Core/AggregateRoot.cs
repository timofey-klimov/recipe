using Recipes.Domain.Core.Events;

namespace Recipes.Domain.Core
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot
        where T : IEquatable<T>
    {
        protected AggregateRoot() { }
        protected AggregateRoot(T id)
            : base(id)
        {
        }

        private List<IDomainEvent> _events = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> Events => _events.ToList();


        protected void RaiseEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();
        
    }
}
