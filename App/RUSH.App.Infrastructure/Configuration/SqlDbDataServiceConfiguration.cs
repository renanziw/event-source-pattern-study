using RUSH.App.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace RUSH.App.Infrastructure.Configuration
{
    public class SqlDbDataServiceConfiguration : ISqlDbDataServiceConfiguration
    {
        public string ConnectionString { get; set; }
    }

    public class SqlDbDataServiceConfigurationValidation : IValidateOptions<SqlDbDataServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, SqlDbDataServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} for azure is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
