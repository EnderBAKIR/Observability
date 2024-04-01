using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OpenTelemetry.Shared
{
    public static class OpenTelemetryExtensions
    {
        public static void AddOpenTelemetryExt(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<OpenTelemetryConstants>(configuration.GetSection("OpenTelemetry"));
            var openTelemetryConstants = (configuration.GetSection("OpenTelemetry").Get<OpenTelemetryConstants>())!;

            ActivitySourceProvider.Source = new System.Diagnostics.ActivitySource(openTelemetryConstants.ActiviySourceName);

            services.AddOpenTelemetry().WithTracing(options =>
            {
                options.AddSource(openTelemetryConstants.ActiviySourceName)
                .ConfigureResource(resource =>
                {
                    resource.AddService(openTelemetryConstants.ServiceName, serviceVersion: openTelemetryConstants.ServiceVersion);
                });

                options.AddAspNetCoreInstrumentation(aspnetcoreoptions =>
                {
                    aspnetcoreoptions.Filter = (context) =>
                    {
                        if (!string.IsNullOrEmpty(context.Request.Path.Value))
                        {
                            return context.Request.Path.Value.Contains("api", StringComparison.InvariantCulture);
                        }
                        return false;
                    };
                    aspnetcoreoptions.RecordException = true;
                });
                options.AddConsoleExporter(); //Console
                options.AddOtlpExporter(); // Jaeger/UI
            });
        }
    }
}

