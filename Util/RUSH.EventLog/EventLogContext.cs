using RUSH.EventLog.Entries;
using Microsoft.EntityFrameworkCore;

namespace RUSH.EventLog
{
    public class EventLogContext : DbContext
    {
        public EventLogContext(DbContextOptions<EventLogContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventLogEntry> IntegrationEventLog { get; set; }
    }
}
