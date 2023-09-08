using ClassifiedAds.Application.FileEntries.DTOs;
using ClassifiedAds.Domain.Constants;
using ClassifiedAds.Domain.Entities;
using ClassifiedAds.Domain.Infrastructure.MessageBrokers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ClassifiedAds.BackgroundServer.OutBoxEventHandlers;

public class FileEntryOutBoxEventHandler : IOutBoxEventHandler
{
    private readonly IMessageBus _messageBus;

    public static string[] CanHandleEventTypes()
    {
        return new string[] { EventTypeConstants.FileEntryCreated, EventTypeConstants.FileEntryDeleted };
    }

    public FileEntryOutBoxEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task HandleAsync(OutboxEvent outboxEvent, CancellationToken cancellationToken = default)
    {
        if (outboxEvent.EventType == EventTypeConstants.FileEntryCreated)
        {
            await _messageBus.SendAsync(new FileUploadedEvent { FileEntry = JsonSerializer.Deserialize<FileEntry>(outboxEvent.Message) }, cancellationToken: cancellationToken);
        }
        else if (outboxEvent.EventType == EventTypeConstants.FileEntryDeleted)
        {
            await _messageBus.SendAsync(new FileDeletedEvent { FileEntry = JsonSerializer.Deserialize<FileEntry>(outboxEvent.Message) }, cancellationToken: cancellationToken);
        }
    }
}
