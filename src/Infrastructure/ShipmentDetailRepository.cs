using Domain.AggregatesModel.Shipments;
using Domain.SeedWork;

namespace Infrastructure;

public class ShipmentDetailRepository : IRepository<ShipmentDetails>
{
    private readonly TransformationDbContext _context;

    public ShipmentDetailRepository(TransformationDbContext context)
    {
        _context = context;
    }

    public void Save(IEnumerable<ShipmentDetails> data)
    {
        _context.ShipmentDetails.AddRange(data);
        _context.SaveChanges();
    }
}