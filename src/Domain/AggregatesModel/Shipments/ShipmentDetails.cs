using Domain.SeedWork;

namespace Domain.AggregatesModel.Shipments;

public class ShipmentDetails : IAggregateRoot
{
    /// <summary>
    /// db id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// seems to be an external id
    /// </summary>
    public string? ExternalId { get; set; }
    public string? Number2 { get; set; }
    public string? Number3 { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Street { get; set; }
    public string? Country { get; set; }
    public string? Zip { get; set; }
    public string? City { get; set; }
    public string? Number5 { get; set; }
    public DateTime OrderDate { get; set; }

    public string? Number6 { get; set; }

    public string? Number7 { get; set; }
    public string? Number8 { get; set; }

    public decimal ShipmentPrice { get; set; }

    public string? ShipmentPriceCurrency { get; set; }

    public decimal ShipmentTax { get; set; }

    public string? ShipmentTaxCurrency { get; set; }

    public string? UndefinedSpace { get; set; }

    public ShipmentDetailsExtension ShipmentDetailsExtension { get; set; }
}