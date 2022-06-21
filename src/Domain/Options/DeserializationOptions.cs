namespace Domain.Options;

/// <summary>
/// Options for the deserialization
/// </summary>
public class DeserializationOptions : IConfigurableOptions
{
    /// <summary>
    /// Identifier for the SA50 data
    /// </summary>
    public string? S50FileIdentifier { get; set; }

    /// <summary>
    /// SA59 Data identifier
    /// </summary>
    public string? S59FileIdentifier { get; set; }
}