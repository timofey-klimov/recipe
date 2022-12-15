using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Recipes.Domain;
using Recipes.Domain.Core.Events;
using Recipes.Persistance.Outbox;

namespace Recipes.Persistance.Interceptors
{
    public class CreateOutboxMessageInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var events = new List<IDomainEvent>();

            context.ChangeTracker
                .Entries<IAggregateRoot>()
                .Select(entry => entry.Entity)
                .Select(entity =>
                {
                    events.AddRange(entity.Events);
                    entity.ClearEvents();
                    return entity;
                })
                .ToList();


                var entries = events.Select(@event => new OutboxMessage
                {
                    OccuredOn = DateTime.UtcNow,
                    Type = @event.GetType().Name,
                    Content = JsonConvert.SerializeObject(@event,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        })
                })
                .ToList();

            context.Set<OutboxMessage>().AddRange(entries);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
