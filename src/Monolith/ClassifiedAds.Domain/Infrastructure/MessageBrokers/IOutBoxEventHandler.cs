using ClassifiedAds.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ClassifiedAds.Domain.Infrastructure.MessageBrokers;

public interface IOutBoxEventHandler
{
    static abstract string[] CanHandleEventTypes();

    Task HandleAsync(OutboxEvent outboxEvent, CancellationToken cancellationToken = default);
}
