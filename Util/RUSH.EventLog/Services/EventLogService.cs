using RUSH.EventBus.Events;
using RUSH.EventLog.Entries;
using RUSH.EventLog.Enums;
using RUSH.EventLog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RUSH.EventLog.Services
{
    public class EventLogService : IEventLogService
    {
        private readonly EventLogContext _integrationEventLogContext;
        private readonly DbConnection _dbConnection;
        private readonly List<Type> _eventTypes;

        public EventLogService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _integrationEventLogContext = new EventLogContext(
                new DbContextOptionsBuilder<EventLogContext>()
                    .UseSqlServer(_dbConnection).Options);

            _eventTypes = Assembly.Load(Assembly.GetEntryAssembly().FullName)
                .GetTypes()
                .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
                .ToList();
        }

        public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsAsync()
        {
            return await _integrationEventLogContext.IntegrationEventLog
                .OrderBy(o => o.CreationTime)
                .ToListAsync();
        }

        public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

            _integrationEventLogContext.Database.UseTransaction(transaction.GetDbTransaction());
            _integrationEventLogContext.IntegrationEventLog.Add(eventLogEntry);

            return _integrationEventLogContext.SaveChangesAsync();
        }
    }
}
