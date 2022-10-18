using RUSH.EventBus.Events;
using RUSH.EventLog.Entries;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RUSH.EventLog.Services.Interfaces
{
    public interface IEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsAsync();
        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
    }
}
