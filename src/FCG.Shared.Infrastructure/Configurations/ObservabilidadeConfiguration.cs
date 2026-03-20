using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FCG.Shared.Infrastructure.Configurations;

public static class ObservabilidadeConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddObservabilidade()
        {
            var nomeServico = builder.Configuration["OTEL_SERVICE_NAME"];
            var endpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];

            if (string.IsNullOrWhiteSpace(nomeServico) || string.IsNullOrWhiteSpace(endpoint))
                return builder;

            var recurso = ResourceBuilder.CreateDefault().AddService(nomeServico);

            builder.Services.AddOpenTelemetry()
                .WithTracing(tracing => tracing
                    .SetResourceBuilder(recurso)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(o => o.Endpoint = new Uri(endpoint)))
                .WithMetrics(metrics => metrics
                    .SetResourceBuilder(recurso)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(o => o.Endpoint = new Uri(endpoint)));

            builder.Logging.AddOpenTelemetry(logs =>
            {
                logs.SetResourceBuilder(recurso);
                logs.IncludeFormattedMessage = true;
                logs.IncludeScopes = true;
                logs.AddOtlpExporter(o => o.Endpoint = new Uri(endpoint));
            });

            return builder;
        }
    }
}
