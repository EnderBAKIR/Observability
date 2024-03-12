// See https://aka.ms/new-console-template for more information
using Observability.ConsoleApp;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

Console.WriteLine("Hello, World!");

var traceProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource(OpenTelemetryConstants.ActiviySourceName)//buradaki isimle oluşturduğumuz sınıf içerisinde ki isim eşleşerek bunu buluyor.
    .ConfigureResource(configure =>
    {
        configure
        .AddService(OpenTelemetryConstants.ServiceName, OpenTelemetryConstants.ServiceVersion)
        .AddAttributes(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("host.machineName", Environment.MachineName),
                    new KeyValuePair<string, object>("host.environment", "dev")
                    
                });
    }).AddConsoleExporter().Build();


var serviceHelper = new ServiceHelper();

serviceHelper.Work1();