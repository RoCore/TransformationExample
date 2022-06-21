using Domain.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Application.Services;

internal class HostedService : IHostedService, IDisposable
{
    private readonly FileSystemWatcher _fileWatcher;
    private readonly ILogger<HostedService> _logger;
    private readonly ILoadDataService _service;
    private CancellationToken _cancellationToken;
    public HostedService(IOptions<FileWatcherOptions> options, ILogger<HostedService> logger, ILoadDataService service)
    {
        _logger = logger;
        _service = service;
        CheckDirectory(options.Value.Path!);
        _fileWatcher = new FileSystemWatcher(options.Value.Path!, options.Value.Filter!);
    }

    private static void CheckDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        _fileWatcher.Created += Created;
        var directoryInfo = new DirectoryInfo(_fileWatcher.Path);
        var files = directoryInfo.EnumerateFiles(_fileWatcher.Filter);

        Parallel.ForEach(files, a => Created(default!, new FileSystemEventArgs(WatcherChangeTypes.Created, a.DirectoryName!, a.Name)));

        await ExitCondition(cancellationToken);
    }

    private static async Task ExitCondition(CancellationToken cancellationToken)
    {
        Console.WriteLine("Press 'Esc' to exit");
        while (!cancellationToken.IsCancellationRequested)
        {
            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
            {
                break;
            }
        }

        await Task.CompletedTask;
    }

    private void Created(object _, FileSystemEventArgs e)
    {
        _logger.LogInformation($"New file detected {e.Name}, trying to read data.");
        var data = _service.LoadFromFile(e.FullPath, _cancellationToken);
        _logger.LogTrace(JsonSerializer.Serialize(data, new JsonSerializerOptions{ WriteIndented = true, ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve }));
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _fileWatcher.Created -= Created;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public void Dispose() => _fileWatcher.Dispose();
}