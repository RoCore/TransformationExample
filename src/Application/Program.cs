// See https://aka.ms/new-console-template for more information

using Application.Configuration;
using Application.Services;
using Domain.AggregatesModel.Shipments;
using Domain.Options;
using Domain.SeedWork;
using Infrastructure;
using Infrastructure.Files.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");
var host = Host.CreateDefaultBuilder();
host
    .ConfigureServices(services =>
        services
            .AddHostedService<HostedService>()
            .ConfigureOptionsWithValidation<FileWatcherOptions>(OptionsConfiguration.Validate)
            .ConfigureOptionsWithValidation<DeserializationOptions>(OptionsConfiguration.Validate)
            .AddTransient<ILoadDataService, LoadDataService>()
            .AddTransient<IDeserializer, Deserializer>()
            .AddTransient<IInteropSerializer, InteropSerializer>()
            .AddTransient<ILoadDataService, LoadDataService>()
            .AddTransient<ILoadDataService, LoadDataService>()
            .AddTransient<IRepository<ShipmentDetails>, ShipmentDetailRepository>()
            .AddDbContext<TransformationDbContext>(options => options.UseInMemoryDatabase("Test"))
    )
    .ConfigureLogging(loggingBuilder =>
    {
        loggingBuilder.AddConsole();
    });


var app = host.Build();
var cancellationTokenSource = new CancellationTokenSource();
await app.Services.GetRequiredService<TransformationDbContext>().Database.EnsureCreatedAsync(cancellationTokenSource.Token);

try
{
    await app.StartAsync(cancellationTokenSource.Token);
}
finally
{
    await app.StopAsync(cancellationTokenSource.Token);
}
