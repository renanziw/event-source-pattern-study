using Microsoft.AspNetCore.Builder;

namespace RUSH.App.API.Core.DependencyInjection
{
    public static class AppBuilderExtensions
    {
        public static void UseSwaggerServices(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RUSH Test API v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
