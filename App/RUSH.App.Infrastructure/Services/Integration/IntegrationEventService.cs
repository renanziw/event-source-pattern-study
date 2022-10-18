using RUSH.App.Infrastructure.Repositories;
using RUSH.App.Infrastructure.Services.Integration.Interfaces;
using RUSH.EventBus.Events;
using RUSH.EventLog;
using RUSH.EventLog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using RUSH.EventLog.Entries;

namespace RUSH.App.Infrastructure.Services.Integration
{
    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly ToDoCatalogDbContext _toDoCatalogDbContext;
        private readonly IEventLogService _eventLogService;
        private readonly Func<DbConnection, IEventLogService> _integrationEventLogServiceFactory;

        public IntegrationEventService(ToDoCatalogDbContext toDoCatalogDbContext, Func<DbConnection, IEventLogService> integrationEventLogServiceFactory,
                                     ILogger<IntegrationEventService> logger)
        {
            _toDoCatalogDbContext = toDoCatalogDbContext ?? throw new ArgumentNullException(nameof(toDoCatalogDbContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventLogService = _integrationEventLogServiceFactory(_toDoCatalogDbContext.Database.GetDbConnection());
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent @event)
        {
            await ResilientTransaction.CreateNew(_toDoCatalogDbContext).ExecuteAsync(async () =>
            {
                await _toDoCatalogDbContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(@event, _toDoCatalogDbContext.Database.CurrentTransaction);
            });
        }

        public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsAsync()
        {
            return await _eventLogService.RetrieveEventLogsAsync();
        }
    }
}
