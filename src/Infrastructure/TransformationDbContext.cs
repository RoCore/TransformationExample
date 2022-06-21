using Domain.AggregatesModel.Shipments;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class TransformationDbContext : DbContext
{
    public TransformationDbContext(DbContextOptions<TransformationDbContext> options) : base(options) { }

    public DbSet<ShipmentDetails> ShipmentDetails { get; set; }
    public DbSet<ShipmentDetailsExtension> ShipmentDetailsExtensions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ShipmentDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new ShipmentDetailsExtensionConfiguration());
    }
}