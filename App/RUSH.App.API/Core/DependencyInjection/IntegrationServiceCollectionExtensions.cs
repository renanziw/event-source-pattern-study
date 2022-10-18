using RUSH.App.Infrastructure.Services.Integration;
using RUSH.App.Infrastructure.Services.Integration.Interfaces;
using RUSH.EventLog.Services;
using RUSH.EventLog.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;

namespace RUSH.App.API.Core.DependencyInjection
{
    public static class IntegrationServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            services.BuildServiceProvider();

            services.AddTransient<IIntegrationEventService, IntegrationEventService>();

            services.AddTransient<Func<DbConnection, IEventLogService>>(
                    sp => (DbConnection connection) => new EventLogService(connection));

            return services;
        }
    }
}
