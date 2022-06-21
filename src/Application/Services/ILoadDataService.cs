using Domain.AggregatesModel.Shipments;

namespace Application.Services;

/// <summary>
/// Application service
/// </summary>
public interface ILoadDataService
{
    /// <summary>
    /// Load data from file
    /// </summary>
    /// <param name="path">full path of the file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All shipments</returns>
    IEnumerable<ShipmentDetails> LoadFromFile(string path, CancellationToken cancellationToken);
}