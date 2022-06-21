using Domain.AggregatesModel.Shipments;
using Infrastructure.Files;
using Infrastructure.Files.Serialization;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Domain.SeedWork;

namespace Application.Services;

public class LoadDataService : ILoadDataService
{
    private readonly IDeserializer _deserializer;
    private readonly ILogger<LoadDataService> _logger;
    private readonly IRepository<ShipmentDetails> _repository;

    public LoadDataService(IDeserializer deserializer, ILogger<LoadDataService> logger, IRepository<ShipmentDetails> repository)
    {
        _deserializer = deserializer;
        _logger = logger;
        _repository = repository;
    }

    public IEnumerable<ShipmentDetails> LoadFromFile(string path, CancellationToken cancellationToken)
    {
        using var reader = File.OpenRead(path);
        var rawData = _deserializer.Read(reader, cancellationToken).ToList();
        var shipments = Map(rawData);
        _repository.Save(shipments);

        return shipments;
    }

    private ICollection<ShipmentDetails> Map(ICollection<IStructLayout> rawData)
    {
        var shipmentDetails = rawData.OfType<S50>().Select(Map).ToArray();
        var deliveryInformation = rawData.OfType<S59>().Select(Map).ToArray();

        foreach (var delivery in deliveryInformation)
        {
            var detailsExists = false;
            foreach (var shipmentDetail in shipmentDetails)
            {
                if (shipmentDetail.ExternalId == delivery.S50Id)
                {
                    shipmentDetail.ShipmentDetailsExtension = delivery;
                    detailsExists = true;
                    break;
                }
            }

            if (!detailsExists)
            {
                _logger.LogCritical($"No shipment details provided for {delivery.S50Id}");
            }
        }
        return shipmentDetails;
    }

    private ShipmentDetails Map(S50 raw)
    {
        return new ShipmentDetails
        {
            City = raw.City?.Trim(),
            Country = raw.Country?.Trim(),
            ExternalId = raw.Id?.Trim(),
            FirstName = raw.FirstName?.Trim(),
            LastName = raw.LastName?.Trim(),
            Number2 = raw.Number2?.Trim(),
            Number3 = raw.Number3?.Trim(),
            Street = raw.Street?.Trim(),
            Zip = raw.Zip?.Trim(),
            Number5 = raw.Number5?.Trim(),
            Number6 = raw.Number6?.Trim(),
            Number7 = raw.Number7?.Trim(),
            OrderDate = TryParseDateTime(raw.OrderDate, raw.Id),
            ShipmentPrice = TryParseToDecimal(raw.ShipmentPrice, raw.Id),
            ShipmentPriceCurrency = raw.ShipmentPriceCurrency?.Trim(),
            ShipmentTax = TryParseToDecimal(raw.ShipmentTax, raw.Id),
            ShipmentTaxCurrency = raw.ShipmentTaxCurrency?.Trim(),
            UndefinedSpace = raw.UndefinedSpace,
            Number8 = raw.Number8?.Trim()
        };
    }

    private ShipmentDetailsExtension Map(S59 raw)
    {
        return new ShipmentDetailsExtension
        {
            Number1 = raw.Number1?.Trim(),
            S50Id = raw.S50Id?.Trim(), 
        };
    }

    private decimal TryParseToDecimal(string? data, string? entryIdentifier)
    {
        if (string.IsNullOrEmpty(data))
        {
            _logger.LogWarning($"{data} in {entryIdentifier} is not recognized as datetime");
            return default;
        }

        try
        {
            return decimal.Parse(data);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, $"{data} in {entryIdentifier} is not recognized as datetime");
            return default;
        }
    }

    private DateTime TryParseDateTime(string? data, string? entryIdentifier)
    {
        if (data == null || data.Length < 21)
        {
            _logger.LogWarning($"{data} in {entryIdentifier} is not recognized as datetime");
            return default;
        }

        try
        {
            return DateTime.ParseExact(data[..21], "yyyyMMddHHmmssFFFFFFF", CultureInfo.InvariantCulture);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, $"{data} in {entryIdentifier} is not recognized as datetime");
            return default;
        }
    }
}