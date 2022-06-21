namespace Domain.Options;

/// <summary>
/// File watcher options
/// </summary>
public class FileWatcherOptions : IConfigurableOptions
{
    /// <summary>
    /// Path for file watcher
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// File watcher filter
    /// </summary>
    public string? Filter { get; set; }
}