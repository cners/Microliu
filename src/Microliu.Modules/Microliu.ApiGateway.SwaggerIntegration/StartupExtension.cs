using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.ApiGateway.SwaggerIntegration
{
    public static class StartupExtension
    {
        public static IApplicationBuilder UseMicroliuSwagger(this IApplicationBuilder app, string version = "v1")
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Microliu API {version}");
            });
            app.UseSwagger();
            return app;
        }

        public static IServiceCollection UseMicroliuSwagger(this IServiceCollection services, MicroliuSwaggerOptions options)
        {
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<Nullable>();
                c.SwaggerDoc(options.Version, new Info { Title = options.Title, Version = options.Version });
            });
            return services;
        }
    }
}
