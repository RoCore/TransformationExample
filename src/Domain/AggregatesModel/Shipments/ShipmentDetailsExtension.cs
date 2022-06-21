namespace Domain.AggregatesModel.Shipments;


public record ShipmentDetailsExtension
{
    public int Id { get; set; }
    public string? Number1 { get; set; }

    public string? S50Id { get; set; }

    public ShipmentDetails Details { get; set; }
}