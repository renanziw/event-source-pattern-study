using RUSH.App.Domain.Repositories.Interfaces;
using RUSH.App.Infrastructure.Configuration.Interfaces;
using RUSH.App.Infrastructure.Repositories;
using RUSH.EventLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace RUSH.App.API.Core.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var sqlDbConfiguration = serviceProvider.GetRequiredService<ISqlDbDataServiceConfiguration>();

            services.AddDbContext<ToDoCatalogDbContext>(options =>
            {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(ToDoCatalogDbContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                     });
            });


            services.AddDbContext<EventLogContext>(options =>
            {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(ToDoCatalogDbContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                     });
            });

            services.AddScoped<IToDosRepository, ToDoRepository>();

            return services;
        }
    }
}
