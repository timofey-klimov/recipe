using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using Recipes.Domain.Core.Events;
using Recipes.Persistance;
using Recipes.Persistance.Outbox;

namespace Recipes.Infrustructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProccessOutboxMessagesJob : IJob
    {
        private readonly IPublisher _publisher;
        private readonly ApplicationDbContext _appDbContext;

        public ProccessOutboxMessagesJob(
            IPublisher publisher,
            ApplicationDbContext applicationDbContext)
        {
            _publisher = publisher;
            _appDbContext = applicationDbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _appDbContext.Set<OutboxMessage>()
                .Where(x => x.ProccededOn == null)
                .OrderBy(x => x.Id)
                .Take(20)
                .ToListAsync(context.CancellationToken);

            foreach (var message in messages)
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    message.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                if (domainEvent is null)
                    continue;
                try
                {
                    await _publisher.Publish(domainEvent, cancellationToken: context.CancellationToken);
                    message.ProccededOn = DateTime.Now;
                }
                catch(Exception ex)
                {
                    message.Error = ex.Message;
                    message.ProccededOn = DateTime.Now;
                }

            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}
