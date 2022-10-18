using RUSH.App.API.Infrastructure.Validators;
using RUSH.App.Domain.Model;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RUSH.App.API.Core.DependencyInjection
{
    public static class ModelValidationServiceCollectionExtensions
    {
        public static IServiceCollection AddModelValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<ToDo>, ToDoValidator>();
            return services;
        }
    }
}
