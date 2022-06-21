namespace Domain.SeedWork;

/// <summary>
/// Default Repository definition
/// </summary>
/// <typeparam name="T">Root object type</typeparam>
public interface IRepository<T> where T : IAggregateRoot
{
    void Save(IEnumerable<T> data);
}