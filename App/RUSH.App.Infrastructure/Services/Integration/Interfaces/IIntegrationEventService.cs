using RUSH.EventBus.Events;
using RUSH.EventLog.Entries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RUSH.App.Infrastructure.Services.Integration.Interfaces
{
    public interface IIntegrationEventService
    {
        Task AddAndSaveEventAsync(IntegrationEvent @event);
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsAsync();
    }
}
