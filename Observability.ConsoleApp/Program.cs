// See https://aka.ms/new-console-template for more information
using Observability.ConsoleApp;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

Console.WriteLine("Hello, World!");

using var traceProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource(OpenTelemetryConstants.ActiviySourceName)//buradaki isimle oluşturduğumuz sınıf içerisinde ki isim eşleşerek bunu buluyor.
    .ConfigureResource(configure =>
    {
        configure
        .AddService(OpenTelemetryConstants.ServiceName, serviceVersion: OpenTelemetryConstants.ServiceVersion)
        .AddAttributes(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("host.machineName", Environment.MachineName),
                    new KeyValuePair<string, object>("host.environment", "dev")
                    
                });
    }).AddConsoleExporter().AddOtlpExporter().AddZipkinExporter(zipkinopt =>
    {
        zipkinopt.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
    }).Build();


var serviceHelper = new ServiceHelper();

await serviceHelper.Work1();