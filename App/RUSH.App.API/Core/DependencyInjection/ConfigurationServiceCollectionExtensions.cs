using RUSH.App.Infrastructure.Configuration;
using RUSH.App.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace RUSH.App.API.Core.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SqlDbDataServiceConfiguration>(config.GetSection("SqlDbSettings"));
            services.AddSingleton<IValidateOptions<SqlDbDataServiceConfiguration>, SqlDbDataServiceConfigurationValidation>();
            var sqlDbDataServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<SqlDbDataServiceConfiguration>>().Value;
            services.AddSingleton<ISqlDbDataServiceConfiguration>(sqlDbDataServiceConfiguration);

            return services;
        }
    }
}
