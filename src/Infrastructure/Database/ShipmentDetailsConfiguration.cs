using Domain.AggregatesModel.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Infrastructure.S50LayoutDefinition;

namespace Infrastructure.Database;

internal class ShipmentDetailsConfiguration : IEntityTypeConfiguration<ShipmentDetails>
{
    public void Configure(EntityTypeBuilder<ShipmentDetails> builder)
    {
        // properties
        builder.Property(a => a.Id).ValueGeneratedOnAdd();
        builder.Property(a => a.ExternalId).HasMaxLength(ExternalIdSize);
        builder.Property(a => a.Number2).HasMaxLength(Number2Size);
        builder.Property(a => a.Number3).HasMaxLength(Number3Size);
        builder.Property(a => a.LastName).HasMaxLength(LastNameSize);
        builder.Property(a => a.FirstName).HasMaxLength(FirstNameSize);
        builder.Property(a => a.Street).HasMaxLength(StreetSize);
        builder.Property(a => a.Country).HasMaxLength(CountrySize);
        builder.Property(a => a.Zip).HasMaxLength(ZipSize);
        builder.Property(a => a.City).HasMaxLength(CitySize);
        builder.Property(a => a.Number5).HasMaxLength(Number5Size);
        builder.Property(a => a.OrderDate);
        builder.Property(a => a.Number6).HasMaxLength(Number6Size);
        builder.Property(a => a.Number7).HasMaxLength(Number7Size);
        builder.Property(a => a.Number8).HasMaxLength(Number8Size);
        builder.Property(a => a.ShipmentPrice).HasPrecision(2).HasMaxLength(ShipmentPriceSize);
        builder.Property(a => a.ShipmentPriceCurrency).HasMaxLength(ShipmentPriceCurrencySize);
        builder.Property(a => a.ShipmentTax).HasPrecision(2).HasMaxLength(ShipmentTaxSize);
        builder.Property(a => a.ShipmentTaxCurrency).HasMaxLength(ShipmentTaxCurrencySize);
        builder.Property(a => a.UndefinedSpace).HasMaxLength(UndefinedSpaceSize);

        //keys
        builder.HasKey(a => a.Id);

        //dependencies
        builder.HasOne(a => a.ShipmentDetailsExtension).WithOne(a => a.Details).HasPrincipalKey<ShipmentDetailsExtension>(a => a.S50Id);
    }
}