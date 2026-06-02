using MythosSoftware.EventSource.Domain.Entities;
using MythosSoftware.EventSource.Domain.Events;

namespace MythosSoftware.EventSource.Application.Interfaces.Repositories;

public interface IGenericEventRepository<T> where T : EntityBase
{
    Task<IReadOnlyList<IDomainEvent>> LoadDomainEventsAsync(string id, CancellationToken ct);
    
    Task<T> LoadAsync(string id, CancellationToken ct);
    
    Task SaveAsync(T account, CancellationToken ct);
}