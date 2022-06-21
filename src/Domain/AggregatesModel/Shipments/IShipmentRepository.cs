using Domain.SeedWork;

namespace Domain.AggregatesModel.Shipments;

public interface IShipmentRepository : IRepository<ShipmentDetails>
{
}