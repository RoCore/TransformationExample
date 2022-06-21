using System.Collections;

namespace Infrastructure.Files.Serialization;

/// <summary>
/// Simple definition of deserialization
/// </summary>
public interface IDeserializer
{
    /// <summary>
    /// Deserialize a single line
    /// </summary>
    /// <param name="file">Single line that contains object data</param>
    /// <param name="cancellationToken">can cancel operation</param>
    /// <returns>return a deserialized objects</returns>
    IEnumerable<IStructLayout> Read(Stream file, CancellationToken cancellationToken);
}