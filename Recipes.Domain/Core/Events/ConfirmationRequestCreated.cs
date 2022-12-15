namespace Recipes.Domain.Core.Events
{
    public class ConfirmationRequestCreated : IDomainEvent
    {
        public Guid ConfirmationRequestId { get; private set; }

        public ConfirmationRequestCreated(Guid confirmationRequestId)
        {
            ConfirmationRequestId = confirmationRequestId;
        }
    }
}
