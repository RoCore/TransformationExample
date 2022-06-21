using Domain.AggregatesModel.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Infrastructure.S59LayoutDefinition;
namespace Infrastructure.Database;

internal class ShipmentDetailsExtensionConfiguration : IEntityTypeConfiguration<ShipmentDetailsExtension>
{
    public void Configure(EntityTypeBuilder<ShipmentDetailsExtension> builder)
    {
        // properties
        builder.Property(a => a.Id).ValueGeneratedOnAdd();
        builder.Property(a => a.Number1).HasMaxLength(Number1Size);
        builder.Property(a => a.S50Id).HasMaxLength(S50IdSize);

        //keys
        builder.HasKey(a => a.Id);

        //dependencies
        builder.HasOne(a => a.Details).WithOne(a => a.ShipmentDetailsExtension).HasPrincipalKey<ShipmentDetails>(a => a.ExternalId);
    }
}